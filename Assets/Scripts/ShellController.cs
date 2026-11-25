using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private Transform trail;
    [SerializeField] private MeshRenderer shellMesh;
    [SerializeField] private Rigidbody shellBody;
    [SerializeField] private Collider shellCollider;

    [SerializeField] private AudioSource explosionAudio;
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip fireClip;

    public ShellScriptableObjectList shellList;

    private bool exploded = false;

    private float explosionRadius;
    private float explosionForce;
    private float maxDamage;
    private float maxLifeTime;

    private bool GetShellProperties(ShellType shellType)
    {
        ShellScriptableObject shell = shellList.shells.Find(t => t.shellTypes == shellType);
        if (shell == null)
        {
            Debug.LogError($"No ShellScriptableObject for {shellType}");
            return false;
        }

        transform.localScale = shell.size;

        explosionRadius = shell.explosionRadius;
        explosionForce = shell.explosionForce;
        maxDamage = shell.maxDamage;
        maxLifeTime = shell.maxLifeTime;

        return true;
    }

    private void OnCollisionEnter(Collision collision) => Explode();

    public void SetShellProperties(Transform exitPoint, float launchVelocity, ShellType shellType)
    {
        if(!GetShellProperties(shellType))
        {
            Destroy(gameObject);
            return;
        }
        SetShootingAudio();
        SetShellActive(true);
        transform.SetPositionAndRotation(exitPoint.position, exitPoint.rotation);
        
        exploded = false;

        shellBody.isKinematic = false;
        shellBody.velocity = transform.forward * launchVelocity;
      
        Invoke(nameof(Explode), maxLifeTime);
    }

    private void SetShootingAudio()
    {
        if (fireClip)
        {
            explosionAudio.clip = fireClip;
            explosionAudio.Play();
        }
    }

    private void Explode()
    {
        if (exploded) return;
        exploded = true;

        shellBody.velocity =Vector3.zero;
        shellBody.isKinematic = true;

        CheckInRadius();
        SetOffExplosion();
        SetShellActive(false);
        
        CancelInvoke(nameof(Explode));
        Destroy(gameObject);
    }

    private void CheckInRadius()
    {
        foreach (var col in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            TankView targetTank = col.GetComponent<TankView>();

            if (targetTank == null) continue;

            targetTank.GetRigidbody().AddExplosionForce(explosionForce, transform.position, explosionRadius);
            
            Vector3 closestPoint = col.ClosestPoint(transform.position);
            float damage = CalculateDamage(closestPoint);
                
            //Apply Damage to the Tank in Radius
            targetTank.GetTankController().TakeDamage(damage);
        }
    }

    private void SetShellActive(bool active)
    {
        shellMesh.enabled = active;
        shellCollider.enabled = active;
        trail.gameObject.SetActive(active);
    }

    private void SetOffExplosion()
    {
        explosionParticles.transform.SetPositionAndRotation(transform.position, Quaternion.Euler(-90.0f, 0.0f, 0.0f));

        explosionParticles.transform.parent = null;
        explosionParticles.Play();

        if(explosionClip)
        { 
        explosionAudio.clip = explosionClip;
        explosionAudio.Play();
        }

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
    }

    private float CalculateDamage(Vector3 targetPosion)
    {
        if(explosionRadius <= 0.0f) 
            return 0.0f;

        Vector3 explosionToTarget = targetPosion - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        
        return Mathf.Max(0.0f, damage);
    }

    void Update()
    {
        transform.forward = shellBody.velocity;
    }
}
