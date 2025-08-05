using UnityEngine;
using System.Collections;
public class Player : MonoBehaviour
{
    [SerializeField] private float speed;

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
            movement();
        }

        void movement()
        {

            float verticalInput = Input.GetAxisRaw("Vertical");
            float horizontalInput = Input.GetAxisRaw("Horizontal");

            Vector3 Movement = new Vector3(verticalInput * Time.deltaTime * speed, 0, horizontalInput * Time.deltaTime * speed).normalized;

            controller.Move(Movement);

        }
    }
}
