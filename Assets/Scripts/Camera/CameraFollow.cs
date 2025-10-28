using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    private Player player;
    [SerializeField] private Vector3 offset = new Vector3(-5f, 5.5f, -5f);

    public float shakeDuration;
    [SerializeField] private AnimationCurve shakeCurve;

    public float shakeStrength;
    private float elapsedTime;
    private bool isShaking;

    private void Start()
    {
        player = FindAnyObjectByType<Player>();
    }
    private void LateUpdate()
    {
        if (!isShaking && player != null)
        {
            
            transform.position = player.transform.position + offset;
        }
    }

    private IEnumerator ApplyScreenShake()
    {
        isShaking = true;
        elapsedTime = 0f;

        Vector3 originalPos = transform.position;

        while (elapsedTime < shakeDuration)
        {
            elapsedTime += Time.deltaTime;
            float strength = shakeCurve.Evaluate(elapsedTime / shakeDuration);

            
            transform.position = player.transform.position + offset + (Vector3)Random.insideUnitCircle * strength * shakeStrength;

            yield return null;
        }

      
        transform.position = player.transform.position + offset;
        isShaking = false;
    }

   
    public void Shake()
    {
        StartCoroutine(ApplyScreenShake());
    }
}
