using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Player playerScript;

    private Animator anim;
    private Transform player;
    private bool isAttacking = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform;
        playerScript = GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isAttacking)
        {
            HandleAttack();
        }

        if (isAttacking)
        {
            playerScript.speed = 0;
        }
        else if (!isAttacking)
        {
            playerScript.speed = 5;
        }
    }

    void HandleAttack()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 targetPoint = hit.point;
            Vector3 dir = (targetPoint - player.position);
            dir.y = 0;
            dir.Normalize();

            Vector2 dir2D = new Vector2(dir.x, dir.z);
            float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;

            // Normalize to 0-360
            if (angle < 0) angle += 360;

            Vector2 animDir = Get8Direction(angle);
            anim.SetFloat("AttackHorizontal", animDir.x);
            anim.SetFloat("AttackVertical", animDir.y);
            anim.SetTrigger("attack");
            isAttacking = true;
            StartCoroutine(CanAttack());
        }
    }

    // Converts angle to nearest 8-direction vector
    Vector2 Get8Direction(float angle)
    {
        if (angle >= 337.5f || angle < 22.5f) return new Vector2(1, 0);      
        if (angle >= 22.5f && angle < 67.5f) return new Vector2(1, 1);        
        if (angle >= 67.5f && angle < 112.5f) return new Vector2(0, 1);       
        if (angle >= 112.5f && angle < 157.5f) return new Vector2(-1, 1);     
        if (angle >= 157.5f && angle < 202.5f) return new Vector2(-1, 0);     
        if (angle >= 202.5f && angle < 247.5f) return new Vector2(-1, -1);    
        if (angle >= 247.5f && angle < 292.5f) return new Vector2(0, -1);     
        if (angle >= 292.5f && angle < 337.5f) return new Vector2(1, -1);     

        return Vector2.zero;
    }

    IEnumerator CanAttack()
    {
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }
}
    

