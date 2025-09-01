using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
public class Player : MonoBehaviour
{
    [Header("Movement")]

    private CharacterController controller;

    public float speed;
    public float originalSpeed;

    public Vector3 Movement;
    public Vector3 lastMovement;

    public bool canMove = true;

    [Header("Player Input")]

    public float verticalInput;
    public float horizontalInput;
    public AudioSource footStepsSound;
    [SerializeField] private float stepInterval = 0.5f; 
    private float stepTimer;


    [Header("Dash Controls")]

    [SerializeField] private float dashForce;
    [SerializeField] private float dashCoolDown;
    public float dashDuration;
    [SerializeField] private float dashDistance;

    private float[] distX = { 30f, 25f, 20f, 15f, 10f, 5f };

    private float NofRaycasts = 6;
    private float[][] dashDistanceCheck;

    private AudioSource dashSound;
    public GameObject[] dashVFX;
    private float allowedDashDistance;
    [SerializeField] private LayerMask wallCollisionMask;
    [SerializeField] private LayerMask groundCollisionMask;

    public bool hasDashed;

    public bool isDashing = false;
    private bool isMoving = false;

    private Attack attackScript;
    [Header("Gravity")]

    [SerializeField] private float gravity = -9.81f;

    private float verticalVelocity;


    [Header("Animations")]

    private Animator playerAnim;

    public float capsuleRadius = 0.5f;
    public float capsuleHeight = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalSpeed = speed;
        controller = GetComponent<CharacterController>();
        playerAnim = GetComponent<Animator>();
        dashSound = GetComponent<AudioSource>();
        stepTimer = stepInterval;
        attackScript = GetComponent<Attack>();

    }

    // Update is called once per frame
    void Update()
    {
        DashDistanceCheck();
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

        if (Input.GetKey(KeyCode.Space) && !isDashing && isMoving)
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
        if (!canMove)
        {
            Movement = Vector3.zero;
            controller.Move(Movement * Time.deltaTime);
            return;
        }

        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");

        Vector3 horizontalMovement = new Vector3(horizontalInput, 0f, verticalInput);

        Vector3 localMovement = transform.TransformDirection(horizontalMovement).normalized;




        Movement = localMovement.normalized;
        Movement.y = verticalVelocity;

        if ((horizontalInput != 0 || verticalInput != 0) && (Movement.x != 0 || Movement.z != 0))
        {
            isMoving = true;
            lastMovement = Movement;

            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                footStepsSound.pitch = Random.Range(1.0f, 1.4f);
              footStepsSound.PlayOneShot(footStepsSound.clip);
               stepTimer = stepInterval;
            }
        }
        else if (horizontalInput == 0 || verticalInput == 0 )
        {
           if (!footStepsSound.isPlaying)
            {
                footStepsSound.Stop();
            }
            
            isMoving = false;
            stepTimer = 0f;
        }

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


    private Vector3 DashDistanceCheck()
    {
        for (int i = 0; i < distX.Length; i++) {
            RaycastHit hitUp;

            if (!Physics.Raycast(transform.position , attackScript.animDir, out hitUp, distX[i] , wallCollisionMask))
            {
                RaycastHit hitDown;

                if (Physics.Raycast(hitUp.point, Vector3.down, out hitDown, distX[i], groundCollisionMask))
                {
                    Vector2 temp = attackScript.animDir.normalized * distX[i];
                    return temp;
                    //dashDistanceCheck
                }
            }

        }
        return transform.position;
    }




    IEnumerator Dash()
    {
        isDashing = true;
        hasDashed = true;
        Vector3 gotoposition = DashDistanceCheck();
        if (lastMovement.sqrMagnitude > 0.01f)
        {
            foreach(GameObject vfx in dashVFX)
            {
                vfx.SetActive(true);
            }
           
          

            Vector3 flatDirection = new Vector3(lastMovement.x, 0f, lastMovement.z);

            if (flatDirection.sqrMagnitude > 0.01f)
            {
                Quaternion dashRotation = Quaternion.LookRotation(-flatDirection, Vector3.up);

                foreach (GameObject vfx in dashVFX)
                {
                    vfx.transform.rotation = Quaternion.Euler(0f, dashRotation.eulerAngles.y, 0f);
                }
               
            }
            dashSound.pitch = Random.Range(2f, 3f);
            dashSound.Play();
            print(gotoposition);

          transform.position = Vector3.Lerp(transform.position, gotoposition, 2);

           
            hasDashed = false;

            foreach (GameObject vfx in dashVFX)
            {
                vfx.SetActive(false);
            }

            yield return new WaitForSeconds(dashCoolDown);
            isDashing = false;
           

        }


    }
}

