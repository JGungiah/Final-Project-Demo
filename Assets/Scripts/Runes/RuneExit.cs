using System.Collections;
using UnityEngine;

public class RuneExit : MonoBehaviour
{
    public int exitIndex;
    private TeleportRune runeScript;

    void Start()
    {
        runeScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TeleportRune>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(runeScript.TeleportCharge(false, exitIndex));
        }
    }
}
