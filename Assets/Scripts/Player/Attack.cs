using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour
{
    [SerializeField] private Camera cam;

    private Transform player;
    private Animator anim;

    private Vector3 mouseWorldPos;
    private Vector3 attackDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            AttackAnim();
        }
    }

    void AttackAnim()
    {
        mouseWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        attackDir = mouseWorldPos - player.transform.position;
        attackDir.y = 0;
        attackDir.Normalize();

        anim.SetFloat("attackVertical", attackDir.z);
        anim.SetFloat("attackHorizontal", attackDir.x);
        anim.SetTrigger("attack");
    }
}
