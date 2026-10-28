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
        CreateTank();
    }

    private void CreateTank()
    {
        TankModel tankModel= new TankModel(tank[3].movementSpeed, tank[3].rotationSpeed, tank[3].type, tank[3].color);
        TankController tankController = new TankController(tankModel, tankView);
    }
}
