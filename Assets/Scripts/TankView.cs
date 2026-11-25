using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer[] childs;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private TankHealthUI healthUI;

    [SerializeField] private Transform fireTransform;
    [SerializeField] private AudioSource shootingAudio;
    [SerializeField] private AudioSource engineAudio;
    [SerializeField] private AudioClip drivingClip;
    [SerializeField] private AudioClip idleClip;
    [SerializeField] private AudioClip chargingClip;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private ShellController shellPrefab;
    [SerializeField] private float destroyDelete;

    private ParticleSystem explosionParticles;
    private AudioSource explosionAudio;

    private GameObject cam;

    public TankHealthUI HealthUI => healthUI;

    void Awake()
    {
        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.gameObject.SetActive(false);
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public void ChangeColor(Material color)
    {
        foreach (MeshRenderer child in childs)
        {
            child.material = color;
        }
    }

    public void PlayMovementAudio()
    {
        if (rb.velocity != Vector3.zero && engineAudio.clip != drivingClip)
        {
            engineAudio.clip = drivingClip;
            engineAudio.Play();
        }
        else if (rb.velocity == Vector3.zero && engineAudio.clip != idleClip)
        {
            engineAudio.clip = idleClip;
            engineAudio.Play();
        }
    }


    public void PlayChargingClip()
    {
        if (!chargingClip) return;
        shootingAudio.clip = chargingClip;
        shootingAudio.Play();
    }

    public void PlayFireClip()
    {
        if (!fireClip) return;
        shootingAudio.clip = fireClip;
        shootingAudio.Play();
    }

    public void PlayeExplosion()
    {
        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive (true);

        explosionParticles.Play();
        explosionAudio.Play();
    }

    public void Fire(float velocity, ShellType shellType)
    {
        ShellController shellInstance = Instantiate(shellPrefab);
        shellInstance.SetShellProperties(fireTransform, velocity, shellType);
    }

    public void DisableTank()
    {
        Transform firstChild = transform.GetChild(0); 

        GameObject firstChildObj = firstChild.gameObject;
        firstChildObj.SetActive (false);

        Destroy(gameObject, destroyDelete);
    }

    public void SetCamera(GameObject cameraObject)
    {
        cam = cameraObject;
    }

    public void DetachCam()
    {
        if(cam != null)
            cam.transform.parent = null;
    }

    public Rigidbody GetRigidbody() => rb;

    public TankController GetTankController() => tankController;
}
