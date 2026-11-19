using UnityEngine;

[CreateAssetMenu(fileName = "TankScriptableObject", menuName = "ScriptableObject/NewTankScriptableObject")]
public class TankScriptableObject : ScriptableObject
{
    public string tankName;
    public TankType tankTypes;
    public ShellType shellType;
    public float movementSpeed;
    public float rotationSpeed;
    public float maxHealth;
    public float launchVelocity;
    public Material color;
    public Vector3 scale;
}

