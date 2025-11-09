using UnityEngine;
using UnityEngine.Audio;

public class SFX : MonoBehaviour
{
  public AudioMixer AudioMixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
        // Update is called once per frame
        void Update()
        {

        }

    public void Master(float level)
    {
        AudioMixer.SetFloat("Master", level);   
    }

    public void Music(float level)
    {
        AudioMixer.SetFloat("Music", level);
    }

    public void SoundEffects(float level)
    {
        AudioMixer.SetFloat("SFX", level);
    }
}
