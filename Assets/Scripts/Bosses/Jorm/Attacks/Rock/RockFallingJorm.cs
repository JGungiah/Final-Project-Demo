
using UnityEngine;

public class RockFallingJorm : MonoBehaviour
{
    public GameObject debrisPrefab;
    private GameObject mainCam;
    private CameraFollow followScript;

    private float fallSpeed = 7;
    private float gravityMultiplier = 2f;
    private float rotationSpeed = 200f;

    private float currentSpeed;
    public float rayDistance = 100f;           
    public LayerMask Ground;              
    public GameObject animationPrefab;
    public bool groundhit;
   

    private void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
        followScript = mainCam.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        currentSpeed += gravityMultiplier * Time.deltaTime;
        transform.Translate(Vector3.down * currentSpeed * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayDistance, Ground) && !groundhit)
        {
            Instantiate(animationPrefab, hit.point, Quaternion.Euler(90,0,0));
            groundhit = true;

        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground")) 
        {
            followScript.shakeStrength = 3f;
            followScript.shakeDuration = 0.5f;
            followScript.Shake();
            Instantiate(debrisPrefab , this.transform.position, Quaternion.identity);
            Destroy(this.gameObject );
        }
    }
}
