using UnityEngine;
using System.Collections;

public class RuneEnter : MonoBehaviour
{
    public int enterIndex;
    private TeleportRune runeScript;
    public GameObject VFX;

    void Start()
    {
        runeScript = GameObject.FindGameObjectWithTag("RuneManager").GetComponent<TeleportRune>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(runeScript.TeleportCharge(true, enterIndex));
        }
        
    }
    private void Update()
    {
        if (runeScript.runeCooldowns[enterIndex])
        {
            VFX.SetActive(false);
        }
        else
        {
            VFX.SetActive(true);
        }
    }
}

