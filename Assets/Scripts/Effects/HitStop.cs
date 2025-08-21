using System.Collections;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    private bool Waiting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyHitStop(float duration)
    {
        if (Waiting)
        {
            return;
        }
        Time.timeScale = 0;
        StartCoroutine(hitStopTime(duration));
    }

    IEnumerator hitStopTime(float duration)
    {
        Waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1;
        Waiting = false;
    }
}
