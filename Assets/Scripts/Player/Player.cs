using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class Player : MonoBehaviour
{
    [Header("Movement")]

    private CharacterController controller;
   
    public float speed;
    public float originalSpeed;
   
    public Vector3 Movement;
    public Vector3 lastMovement;


    [Header("Player Input")]

    public float verticalInput;
    public float horizontalInput;


    [Header("Dash Controls")]
    
    [SerializeField] private float dashForce;
    [SerializeField] private float dashCoolDown;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashDistance;

    private bool isDashing = false;


    [Header("Gravity")]
   
    [SerializeField] private float gravity = -9.81f;
    
    private float verticalVelocity;


    [Header("Animations")]
    
    private Animator playerAnim;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSpeed = speed;
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        print(controller.velocity);
        if (controller != null)
        {
            if (!controller.isGrounded)
            {
                ApplyGravity();
            }
            else
            {
                RemoveGravity();
            }
            movement();
        }

        if (playerAnim != null)
        {
            Animations();
        }

        if (Input.GetKey(KeyCode.Space) && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }


    void ApplyGravity()
    {
        verticalVelocity += gravity * Time.deltaTime;
    }

    void RemoveGravity()
    {
        verticalVelocity = -2f;
    }

    void movement()
    {

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 horizontalMovement = new Vector3(horizontalInput, 0f, verticalInput);

        Vector3 localMovement = transform.TransformDirection(horizontalMovement);

        if ((horizontalInput != 0 || verticalInput != 0) && Movement.x != 0 || Movement.z != 0)
        {
            lastMovement = Movement;
        }

        
        Movement = localMovement.normalized;
        Movement.y = verticalVelocity;
        

       

        controller.Move(Movement * speed * Time.deltaTime);
    }

    void Animations()
    {

        Vector3 flatMovement = new Vector3(Movement.x, 0f, Movement.z);

        playerAnim.SetFloat("verticalMovement", flatMovement.z);
        playerAnim.SetFloat("horizontalMovement", flatMovement.x);
        playerAnim.SetFloat("animMoveMagnitude", flatMovement.magnitude);
        playerAnim.SetFloat("lastVertical", lastMovement.z);
        playerAnim.SetFloat("lastHorizontal", lastMovement.x);
    }


    IEnumerator Dash()
    {
        isDashing = true;

        dashForce = dashDistance / dashDuration;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            controller.Move (lastMovement * dashForce * Time.deltaTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(dashCoolDown);
        
        isDashing = false;
    }
}

