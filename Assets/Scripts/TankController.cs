using UnityEngine;

public class TankController
{
    private TankModel tankModel;
    private TankView tankView;

    private Rigidbody rb;
    private TankHealthUI healthUI;

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
    }

    public void TakeDamage(float damage)
    {
        if(tankModel.IsDead) return;

        tankModel.TakeDamage(damage);

        UpdateHealthUI();

        if(tankModel.IsDead)
        {
            tankView.DetachCam();
            tankView.PlayeExplosion();
            tankView.DisableTank();
        }
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
        rb.MoveRotation(rb.rotation*deltaRotation);
    }

    public void SetTankScale(Vector3 scale) => tankView.transform.localScale = scale;

    public TankModel GetTankModel() => tankModel;
}
