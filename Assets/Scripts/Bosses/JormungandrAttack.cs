using UnityEngine;

public class JormungandrAttack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Transform player;

    public void FireAtPlayer()
    {
        if (player == null) return;

        // Spawn projectile
        GameObject proj = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Aim projectile toward the player
        proj.GetComponent<ProjectileJorm>().Init(player.position);
    }
}
