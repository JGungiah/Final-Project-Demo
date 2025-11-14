using UnityEngine;

public class FallingSphere : MonoBehaviour
{
    //public GameObject debrisPrefab;
    private GameObject mainCam;
    private CameraFollow followScript;

    private float fallSpeed = 7;
    private float gravityMultiplier = 2f;
    private float rotationSpeed = 500f;

    private float currentSpeed;


    private void Start()
    {
        mainCam = GameObject.FindWithTag("MainCamera");
        followScript = mainCam.GetComponent<CameraFollow>();
    }

    private void Update()
    {
        currentSpeed += gravityMultiplier * Time.deltaTime;
        transform.Translate(Vector3.down * currentSpeed * fallSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.down * rotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            followScript.shakeStrength = 3f;
            followScript.shakeDuration = 0.5f;
            followScript.Shake();
            //Instantiate(debrisPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
