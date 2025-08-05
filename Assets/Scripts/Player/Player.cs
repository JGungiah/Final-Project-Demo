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

            Vector3 Movement = new Vector3(horizontalInput, verticalVelocity, verticalInput ).normalized;

            controller.Move(Movement * speed * Time.deltaTime);

        }

        void Animations()
        {
            if (verticalInput > 0)
            {
                playerAnim.SetBool("isRunningNorth", true);
            }
            else
            {
                playerAnim.SetBool("isRunningNorth", false);
            }
        }
       
    }

