using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private enum EnemyState 
    { 
        Attacking, 
        Chasing, 
        Dead 
    }


    private Transform target;
    private NavMeshAgent agent;
    private TankModel tankModel;
    private TankController tankController;
    private PlayerTankInput playerTankInput;
    private EnemyState currentState;
    private float rotationSpeed;
    private bool scoreAwarded = false;

    [SerializeField] private LayerMask playerMask;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackInterval = 3.0f;

    private float nextAttackTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent == null )
        {
            Debug.LogError("NavMeshAgent is Missing");
            return;
        }

        target = GameObject.FindGameObjectWithTag("Player")?.transform;
        if(target != null)
        {
            playerTankInput = target.GetComponent<PlayerTankInput>();
        }


        TankView tankView = GetComponent<TankView>();
        if (tankView == null)
        {
            Debug.LogError("Tank View is not assigned");
            return;
        }

        tankController = tankView.GetTankController();
        if (tankController == null)
        {
            Debug.LogError("Tank Controller is not assigned");
            return;
        }

        tankModel = tankController.GetTankModel();
        if (tankModel == null)
        {
            Debug.LogError("Tank Model is not assigned");
            return;
        }

        rotationSpeed = tankModel.RotationSpeed;
        agent.speed = tankModel.MovementSpeed;

        currentState = EnemyState.Chasing;
        agent.updateRotation = false;
    }

    void Update()
    {
        if (target == null)
        {
            Debug.LogError("Target Transform not found.");
            return;
        }

        if (playerTankInput != null && playerTankInput.IsDead()) 
        {
            agent.isStopped = true;
            return;
        }

        switch (currentState)
        {
            case EnemyState.Chasing:
                if (CheckDeathState()) return;

                agent.updateRotation = true;

                if (CheckPlayerInRange())
                {
                    currentState = EnemyState.Attacking;
                    agent.updateRotation = false;
                    StopMovingEnemy();
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case EnemyState.Attacking:
                if (CheckDeathState()) return;

                StopMovingEnemy();
                FacePlayer();

                if (!CheckPlayerInRange())
                {
                    currentState = EnemyState.Chasing;
                    break;
                }

                HandleAttackCooldown();
                break;

            case EnemyState.Dead:
                AddPointsToPlayer();
                break;
        }
    }

    private bool CheckDeathState()
    {
        if (tankModel == null) return false;
        if (!tankModel.IsDead) return false;

        currentState = EnemyState.Dead;
        agent.isStopped = true;

        return true;
    }

    private void StopMovingEnemy()
    {
        agent.isStopped = true;
    }

    private bool CheckPlayerInRange()
    {
        return Physics.CheckSphere(transform.position, attackRange, playerMask);
    }

    private void HandleAttackCooldown()
    {
        if (!CheckPlayerInRange()) return;
        if (Time.time < nextAttackTime) return;

        nextAttackTime = Time.time + attackInterval;
        Fire(); 
    }

    private void FacePlayer()
    {
        Vector3 dir = target.position - transform.position;
        dir.y = 0.0f;
        if (dir.sqrMagnitude < 0.001f) return;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(dir), rotationSpeed * Time.deltaTime);
    }

    private void AddPointsToPlayer()
    {
        if (scoreAwarded) return;
        if (playerTankInput == null) return;

        playerTankInput.UpdateScore(tankModel.TankPoints);
        scoreAwarded = true;

        Debug.Log("Updated Score by: " + tankModel.TankPoints);
    }

    private void Fire()
    {
        tankController.Fire(tankModel.LaunchVelocity);
    }

    private void ChasePlayer()
    {
        agent.isStopped = false;
        agent.destination = target.position;
    }
}
