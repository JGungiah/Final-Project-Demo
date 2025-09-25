using System.Collections;
using UnityEngine;

public class RuneExit : MonoBehaviour
{
    public int exitIndex;
    private TeleportRune runeScript;
    public GameObject VFX;
    void Start()
    {
        runeScript = GameObject.FindGameObjectWithTag("RuneManager").GetComponent<TeleportRune>();

        if (runeScript == null)
        {
            print(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(runeScript.TeleportCharge(false, exitIndex));
        }
    }

    private void Update()
    {
        if (runeScript.runeCooldowns[exitIndex])
        {
            VFX.SetActive(false);
        }
        else
        {
            VFX.SetActive(true);
        }
    }
}
