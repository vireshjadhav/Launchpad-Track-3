public class TankModel
{
    private TankController tankController;

    public float movementSpeed;
    public float rotationSpeeed;

    public TankModel(float _movementSpeed, float rotationSpeed)
    {
        movementSpeed = _movementSpeed;
        rotationSpeeed = rotationSpeed;
    }

    public void SetTankController(TankController _tankController)
    {
        tankController = _tankController;
    }
}
