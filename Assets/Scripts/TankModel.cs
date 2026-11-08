using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeeed;
    public TankTypes type;
    public Material color;

    private float maxHealth;
    private float currentHealth;
    private bool isDead;

    public float CurrentHealth => currentHealth;
    public float MaxHealth => maxHealth;
    public bool IsDead => isDead;


    public TankModel(float _movementSpeed, float rotationSpeed, TankTypes _type, Material _color, float _health)
    {
        movementSpeed = _movementSpeed;
        rotationSpeeed = rotationSpeed;
        type = _type;
        color = _color;
        maxHealth = _health;
        currentHealth = _health;
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
