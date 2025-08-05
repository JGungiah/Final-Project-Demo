using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float gravity = -9.81f;

    private float verticalVelocity;
    
    private CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
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

            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 Movement = new Vector3(horizontalInput, verticalVelocity, verticalInput ).normalized;

            controller.Move(Movement * speed * Time.deltaTime);

        }

       
    }
}
