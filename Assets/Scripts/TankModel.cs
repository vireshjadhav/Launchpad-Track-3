using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public string tankName;
    public float movementSpeed;
    public float rotationSpeed;
    public TankType  type;
    public Material color;
    public Vector3 scale;

    private float maxHealth;
    private float currentHealth;
    private bool isDead;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => isDead;

    public TankModel(TankScriptableObject _tank)
    {
        tankName = _tank.tankName;
        movementSpeed = _tank.movementSpeed;
        rotationSpeed = _tank.rotationSpeed;
        type = _tank.tankTypes;
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

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
}
