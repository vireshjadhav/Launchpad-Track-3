using UnityEngine;

public class PlayerTankInput : MonoBehaviour
{
    private float movement;
    private float rotation;
    private float chargingVelocity;

    private TankController tankController;
    private TankView tankView;
    private TankModel tankModel;
    private GameObject cam;

    private bool fired = false;


    private void Awake()
    {
        cam = GameObject.Find("Main Camera");

        if (cam == null) 
            return;

        cam.transform.SetParent(transform);
        cam.transform.position = new Vector3(-12.0f, 15.0f, -10.0f);
        cam.transform.rotation = Quaternion.Euler(37.0f, 50.0f, 0.0f);


        tankView = GetComponent<TankView>();
        tankView.SetCamera(cam);
    }

    // Start is called before the first frame update
    void Start()
    {
        tankController = tankView.GetTankController();
        tankModel = tankController.GetTankModel();

        SetInitialLaunchVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();

        tankView.PlayMovementAudio();
        SetLaunchVelocity();
    }

    private void FixedUpdate()
    {
        tankController.Move(movement);
        tankController.Rotate(rotation);
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
        if (tankModel.MaxShellVelocity <= tankModel.MinShellVelocity)
        {
            Debug.LogError("Maximum Shell Velocity must be greater than Minimum Shell Velocity.");
            return;
        }
        chargingVelocity = (tankModel.MaxShellVelocity - tankModel.MinShellVelocity) / tankModel.MaxChargeTime;
    }

    private void SetLaunchVelocity()
    {
        if (tankModel.CurrentLaunchVelocity >= tankModel.MaxShellVelocity && !fired)
        {
            fired = true;
            tankController.Fire();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            fired = false;
            tankModel.SetLaunchVelocity(tankModel.MinShellVelocity);
            tankView.PlayChargingClip();
        }
        else if (Input.GetMouseButton(0) && !fired)
        {
            float velocity = Mathf.Min(tankModel.CurrentLaunchVelocity + chargingVelocity * Time.deltaTime, tankModel.MaxShellVelocity);
            tankModel.SetLaunchVelocity(velocity);
            tankView.UpdateArrowSlider(velocity);
        }
        else if (Input.GetMouseButtonUp(0) && !fired)
        {
            fired = true;
            tankController.Fire();
        }
    }

    public void DetachCam()
    {
        cam.transform.parent = null;
    }
}
