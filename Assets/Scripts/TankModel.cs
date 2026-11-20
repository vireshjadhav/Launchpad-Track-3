using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public string tankName;
    public float movementSpeed;
    public float rotationSpeed;
    public TankType  type;
    public ShellType shellType;
    public Material color;
    public Vector3 scale;

    private float maxHealth;
    private float currentHealth;
    private bool isDead;

    private float minShellVelocity = 15.0f;
    private float maxShellVelocity = 30.0f;
    private float maxChargeTime = 0.75f;

    private float currentLaunchVelocity;


    public float CurrentLaunchVelocity => currentLaunchVelocity;
    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public float MinShellVelocity => minShellVelocity;
    public float MaxShellVelocity => maxShellVelocity;
    public float MaxChargeTime => maxChargeTime;
    public bool IsDead => isDead;

    public TankModel(TankScriptableObject _tank)
    {
        tankName = _tank.tankName;
        movementSpeed = _tank.movementSpeed;
        rotationSpeed = _tank.rotationSpeed;
        type = _tank.tankTypes;
        shellType = _tank.shellType;
        color = _tank.color;
        maxHealth = _tank.maxHealth;
        currentHealth = _tank.maxHealth;
        scale = _tank.scale;
        isDead = false;
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) 
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (currentHealth <= 0)
            isDead = true;
    }

    public void SetLaunchVelocity(float velocity)
    {
        currentLaunchVelocity = Mathf.Clamp(velocity, minShellVelocity, maxShellVelocity);
    }

    public void ResetLaunchVelocity()
    {
        currentLaunchVelocity = minShellVelocity;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public ShellType GetShellType() => shellType;
}
