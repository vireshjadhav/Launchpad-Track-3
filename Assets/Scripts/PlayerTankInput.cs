using UnityEngine;
using UnityEngine.UI;

public class PlayerTankInput : MonoBehaviour
{
    private float movement;
    private float rotation;
    private float chargingVelocity;
    private float currentLaunchVelocity;

    private TankController tankController;
    
    private TankModel tankModel;
    private GameObject cam;
    private TankView tankView;
    
    [SerializeField] private Slider aimSlider;
    [SerializeField] private float minShellVelocity = 15.0f;
    [SerializeField] private float maxShellVelocity = 30.0f;

    private bool fired = false;

    private void Awake()
    {
        cam = GameObject.Find("Main Camera");

        if (cam == null) 
            return;

        cam.transform.SetParent(transform);
        cam.transform.position = new Vector3(-12.0f, 15.0f, -10.0f);
        cam.transform.rotation = Quaternion.Euler(37.0f, 50.0f, 0.0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        tankView = GetComponent<TankView>();
        tankController = tankView.GetTankController();
        tankModel = tankController.GetTankModel();
        
        tankView.SetCamera(cam);

        SetInitialLaunchVelocity();
        ResetLaunchVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (tankModel.IsDead) return;
        Movement();

        tankView.PlayMovementAudio();
        SetLaunchVelocity();
    }

    private void FixedUpdate()
    {
        if(tankModel.IsDead) return;
        tankController.Move(movement);
        tankController.Rotate(rotation);
    }

    public void SetLaunchVelocity(float velocity)
    {
        currentLaunchVelocity = Mathf.Clamp(velocity, minShellVelocity, maxShellVelocity);
    }

    private void ResetLaunchVelocity()
    {
        currentLaunchVelocity = minShellVelocity;
        UpdateArrowSlider(minShellVelocity);
    }

    public void UpdateArrowSlider(float velocity)
    {
        if (aimSlider != null)
            aimSlider.value = velocity;
    }

    private void Movement()
    {
        movement = Input.GetAxis("Vertical");
        rotation = Input.GetAxis("Horizontal");
    }

    public void SetInitialLaunchVelocity()
    {
        if (tankModel.MaxChargeTime <= 0)
        {
            Debug.LogError("Maximum Charge Time must be greater than zero.");
            return;
        }
        if (maxShellVelocity <= minShellVelocity)
        {
            Debug.LogError("Maximum Shell Velocity must be greater than Minimum Shell Velocity.");
            return;
        }
        chargingVelocity = (maxShellVelocity - minShellVelocity) / tankModel.MaxChargeTime;
    }

    private void SetLaunchVelocity()
    {
        if (currentLaunchVelocity >= maxShellVelocity && !fired)
        {
            fired = true;
            Fire();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            fired = false;
            SetLaunchVelocity(minShellVelocity);
            tankView.PlayChargingClip();
        }
        else if (Input.GetMouseButton(0) && !fired)
        {
            float velocity = Mathf.Min(currentLaunchVelocity + chargingVelocity * Time.deltaTime, maxShellVelocity);
            SetLaunchVelocity(velocity);
            UpdateArrowSlider(velocity);
        }
        else if (Input.GetMouseButtonUp(0) && !fired)
        {
            fired = true;
            Fire();
        }
    }

    private void Fire()
    {
        tankController.Fire(currentLaunchVelocity);
        ResetLaunchVelocity();
    }

    public void DetachCam()
    {
        cam.transform.parent = null;
    }

    public bool IsDead() => tankModel.IsDead;

    public void UpdateScore(float tankPoints)
    {
        tankController.UpdateScore(tankPoints);
    }
}
