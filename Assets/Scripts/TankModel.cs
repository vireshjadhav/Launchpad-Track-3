using UnityEngine;

public class TankModel
{
    public string tankName;
    public TankType  type;
    public ShellType shellType;
    public Material color;
    public Vector3 scale;

    private float maxHealth;
    private float currentHealth;
    public float movementSpeed;
    public float rotationSpeed;

    private float maxChargeTime = 0.75f;

    private bool isDead;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
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

    public void TakeDamage(float damage)
    {
        if (isDead) 
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);

        if (currentHealth <= 0)
            isDead = true;
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public ShellType GetShellType() => shellType;
}
