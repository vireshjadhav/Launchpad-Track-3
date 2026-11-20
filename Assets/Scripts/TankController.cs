    using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;

    private Rigidbody rb;
    private TankHealthUI healthUI;

    private float chargingVelocity;

    private bool fired = false;

    public TankController(TankModel _tankModel, TankView _tankView)
    {
        tankModel = _tankModel;
        tankView = GameObject.Instantiate<TankView>(_tankView, _tankView.transform.position, Quaternion.identity);
        healthUI = tankView.HealthUI;

        rb = tankView.GetRigidbody();

        tankModel.SetTankController(this);
        tankView.SetTankController(this);

        tankView.ChangeColor(tankModel.color);
        SetTankScale(tankModel.scale);
        UpdateHealthUI();

        SetInitialLaunchVelocity();
        ResetLaunchVelocity();
    }

    public void TakeDamage(float damage)
    {
        if (tankModel.IsDead) return;

        tankModel.TakeDamage(damage);

        UpdateHealthUI();

        if (tankModel.IsDead)
        {
            tankView.DetachCam();
            tankView.PlayeExplosion();
            tankView.DisableTank();
        }
    }

    private void ResetLaunchVelocity()
    {
        tankModel.ResetLaunchVelocity();
        tankView.UpdateArrowSlider(tankModel.MinShellVelocity);
    }

    public void SetInitialLaunchVelocity()
    {
        if(tankModel.MaxChargeTime <= 0) 
        {
            Debug.LogError("Maximum Charge Time must be greater than zero.");
            return; 
        }   
        chargingVelocity = (tankModel.MaxShellVelocity - tankModel.MinShellVelocity) / tankModel.MaxChargeTime;
    }

    public void SetLaunchVelocity()
    {
        if (tankModel.CurrentLaunchVelocity >= tankModel.MaxShellVelocity && !fired)
        {
            tankModel.SetLaunchVelocity(tankModel.MaxShellVelocity);
            Fire();
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
            Fire();
        }
    }

    private void Fire()
    {
        fired = true;

        tankView.Fire(tankModel.CurrentLaunchVelocity);
        tankView.PlayFireClip();

        tankModel.ResetLaunchVelocity();
        tankView.UpdateArrowSlider(tankModel.MinShellVelocity);
    }

    private void UpdateHealthUI()
    {
        healthUI.UpdateHealth(tankModel.CurrentHealth, tankModel.MaxHealth);
    }

    public void Move(float movement, float movementSpeeed)
    {
        rb.velocity = tankView.transform.forward * movement * movementSpeeed;

    }

    public void Rotate(float rotation, float rotationSpeed)
    {
        Vector3 vector = new Vector3(0.0f, rotation * rotationSpeed, 0.0f);
        Quaternion deltaRotation = Quaternion.Euler(vector * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    public void SetTankScale(Vector3 scale) => tankView.transform.localScale = scale;

    public TankModel GetTankModel() => tankModel;
}
