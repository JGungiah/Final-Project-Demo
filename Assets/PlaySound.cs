using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource smashNoise;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        smashNoise = GetComponent<AudioSource>();
        smashNoise.pitch = Random.Range(0.8f, 1f);
        smashNoise.PlayOneShot(smashNoise.clip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
