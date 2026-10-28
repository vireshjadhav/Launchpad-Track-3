using UnityEngine;

public class TankModel
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeeed;
    public TankTypes type;
    public Material color;

    public TankModel(float _movementSpeed, float rotationSpeed, TankTypes _type, Material _color)
    {
        movementSpeed = _movementSpeed;
        rotationSpeeed = rotationSpeed;
        type = _type;
        color = _color;
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
