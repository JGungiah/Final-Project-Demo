using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] private Vector3 offset = new Vector3(-5f, 5.5f, -5f);

    [SerializeField] private float shakeDuration = 0.5f;
    [SerializeField] private AnimationCurve shakeCurve;

    private float elapsedTime;
    private bool isShaking;

    private void LateUpdate()
    {
        if (!isShaking && player != null)
        {
            
            transform.position = player.position + offset;
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

            
            transform.position = player.position + offset + (Vector3)Random.insideUnitCircle * strength;

            yield return null;
        }

      
        transform.position = player.position + offset;
        isShaking = false;
    }

   
    public void Shake()
    {
        StartCoroutine(ApplyScreenShake());
    }
}
