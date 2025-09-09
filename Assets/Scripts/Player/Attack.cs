using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    public Camera cam;
    [SerializeField] private float attackCooldown;
    private Player playerScript;
   

    private Animator anim;
    private Transform player;
    public bool isAttacking = false;
    public bool colliderActive = false;
   
    public Vector3 knockbackDirection; 
    public Quaternion rotation;

    public Vector2 animDir;

    [SerializeField] private Transform attackCollider;
    [SerializeField] private GameObject arrowUI;
    private Vector3 arrowDir;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = transform;
        playerScript = GetComponent<Player>();

       
    }

    void Update()
    {
        ArrowDirection();
        if (Input.GetMouseButtonDown(1))
        {
            CalculateParry();
        }

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
            playerScript.speed = playerScript.originalSpeed;
        }

        attackCollider.gameObject.SetActive(colliderActive);


    }

    void ArrowDirection()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 targetPoint = hit.point;
           arrowDir = (targetPoint - player.position);
           arrowDir.y = 0;
           arrowDir.Normalize();


        }
    }
    void CalculateParry()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            Vector3 targetPoint = hit.point;
            Vector3 dir = (targetPoint - player.position);
            dir.y = 0;
            dir.Normalize();

            knockbackDirection = dir.normalized;


            if (attackCollider != null)
            {


                attackCollider.rotation = Quaternion.LookRotation(dir, Vector3.up);

            }

            Vector2 dir2D = new Vector2(dir.x, dir.z);
            float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;
            angle -= 45f;
            if (angle < 0) angle += 360;

            animDir = GetDirection(angle);
            anim.SetFloat("AttackHorizontal", animDir.x);
            anim.SetFloat("AttackVertical", animDir.y);
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

            knockbackDirection = dir.normalized;


            if (attackCollider != null)
            {
              

                attackCollider.rotation = Quaternion.LookRotation(dir, Vector3.up);
               
            }

            Vector2 dir2D = new Vector2(dir.x, dir.z);
            float angle = Mathf.Atan2(dir2D.y, dir2D.x) * Mathf.Rad2Deg;
            angle -= 45f;       
            if (angle < 0) angle += 360;

            animDir = GetDirection(angle);
            anim.SetFloat("AttackHorizontal", animDir.x);
            anim.SetFloat("AttackVertical", animDir.y);
            anim.SetTrigger("attack");


            isAttacking = true;
            StartCoroutine(CanAttack());
        }
    }

    Vector2 GetDirection(float angle)
    {
      
        if (angle >= 337.5f || angle < 22.5f) return new Vector2(0,1);//North
        if (angle >= 22.5f && angle < 67.5f) return new Vector2(-1,1); //North West
        if (angle >= 67.5f && angle < 112.5f) return new Vector2(-1,0); //West
        if (angle >= 112.5f && angle < 157.5f) return new Vector2(-1, -1); //South West
        if (angle >= 157.5f && angle < 202.5f) return new Vector2(0, -1); //South
        if (angle >= 202.5f && angle < 247.5f) return new Vector2(1,-1); //South East
        if (angle >= 247.5f && angle < 292.5f) return new Vector2(1,0); //East
        if (angle >= 292.5f && angle < 337.5f) return new Vector2(1, 1); //North East

      

        return Vector2.zero;
    }

    IEnumerator CanAttack()
    {
        
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        //attackCollider.gameObject.SetActive(false);
    }



   

}


