using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.81f;

    private float verticalVelocity;

    private Animator playerAnim;
    private CharacterController controller;

    public float verticalInput;
    public float horizontalInput;

    public Vector3 Movement;
    public Vector3 lastMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
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

        Vector3 horizontalMovement = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        if ((horizontalInput != 0 || verticalInput != 0) && Movement.x != 0 || Movement.z != 0)
        {
            lastMovement = Movement;
        }


        Movement = horizontalMovement;
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
}

