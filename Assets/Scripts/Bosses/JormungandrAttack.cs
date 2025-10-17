using UnityEngine;

public class JormungandrAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    private GameObject player;
    [SerializeField] private float projectileSpeed;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        
    }
    public void RangedAttack()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        Vector3 direction = (player.transform.position - firePoint.position).normalized;
        projectile.transform.forward = direction;

        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        Vector3 force = direction * projectileSpeed;
        rb.AddForce(force,ForceMode.Impulse);
    }
}
