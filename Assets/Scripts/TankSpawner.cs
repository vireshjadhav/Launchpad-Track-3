using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{
    [System.Serializable]
    public class Tank
    {
        public float movementSpeed;
        public float rotationSpeed;
        public TankTypes type;
        public Material color;
    }

    [SerializeField]
    private TankView tankView;

    [SerializeField]
    private List<Tank> tank;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void CreateTank(TankTypes tankType)
    {
        int currentTank = 0;

        if (tankType == TankTypes.GreenTank)
            currentTank = 0;
        if(tankType == TankTypes.RedTank)
            currentTank = 1;
        if (tankType == TankTypes.BlueTank)
            currentTank = 2;
        if(tankType == TankTypes.YellowTank)
            currentTank = 3;

        TankModel tankModel = new TankModel(tank[currentTank].movementSpeed, tank[currentTank].rotationSpeed, tank[currentTank].type, tank[currentTank].color);
        TankController tankController = new TankController(tankModel, tankView);
    }
}
