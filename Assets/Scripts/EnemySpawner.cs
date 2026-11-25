using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private TankView tankView;

    public TankScriptableObjectList tankList;

    private bool PlayerSpawned = false;

    GameObject enemyObj;

    public void CreateEnemyTank(TankType tankType)
    {
        TankScriptableObject tank = tankList.tanks.Find(t => t.tankTypes == tankType);

        if (tank == null)
        {
            Debug.LogError($"No TankScriptableObject for {tankType}");
            return;
        }

        TankModel tankModel = new TankModel(tank);

        TankController tankController = new TankController(tankModel, tankView);
    }

    private void Update()
    {
        if (enemyObj != null) return;

        enemyObj = GameObject.FindGameObjectWithTag("Enemy");

        if (enemyObj == null && PlayerSpawned)
        {
            CreateEnemyTank(TankType.RedTank);
        }
    }

    public void SetPlayerSpwaned()
    {
        PlayerSpawned = true;
        CreateEnemyTank(TankType.RedTank);
    }
}