using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankSpawner : MonoBehaviour
{

    [SerializeField] private TankView tankView;

    public TankScriptableObjectList tankList;
 
    public void CreateTank(TankType tankType)
    {
        TankScriptableObject tank = tankList.tanks.Find(t => t.tankTypes == tankType);

        if(tank == null)
        {
            Debug.LogError($"No TankScriptableObject for {tankType}");
            return;
        }

        TankModel tankModel = new TankModel(tank);

        TankController tankController = new TankController(tankModel, tankView);
    }
}
