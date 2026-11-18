using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankView : MonoBehaviour
{
    private TankController tankController;

    [SerializeField] private Rigidbody rb;
    [SerializeField] private MeshRenderer[] childs;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private TankHealthUI healthUI;

    private float movement;
    private float rotation;
    private ParticleSystem explosionParticles;
    private AudioSource explosionAudio;

    private GameObject cam;

    public TankHealthUI HealthUI => healthUI;

    void Awake()
    {
        cam = GameObject.Find("Main Camera");
        cam.transform.SetParent(transform);
        cam.transform.position = new Vector3(-12.0f, 15.0f,-10.0f);
        cam.transform.rotation = Quaternion.Euler(37.0f, 50.0f, 0.0f);

        explosionParticles = Instantiate(explosionPrefab).GetComponent<ParticleSystem>();
        explosionAudio = explosionParticles.GetComponent<AudioSource>();
        explosionParticles.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        if(movement != 0)
            tankController.Move(movement, tankController.GetTankModel().movementSpeed);

        if (rotation != 0)
            tankController.Rotate(rotation, tankController.GetTankModel().rotationSpeed);
    }

    private  void Movement()
    {
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
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

    public void PlayeExplosion()
    {
        explosionParticles.transform.position = transform.position;
        explosionParticles.gameObject.SetActive (true);

        explosionParticles.Play();
        explosionAudio.Play();
    }

    public void DetachCam()
    {
        cam.transform.parent = null;
    }

    public void DisableTank()
    {
        gameObject.SetActive (false);
    }

    public Rigidbody GetRigidbody() => rb;

    public TankController GetTankController() => tankController;
}
