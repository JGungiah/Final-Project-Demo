using UnityEngine;
using System.Collections;

public class RuneEnter : MonoBehaviour
{
    public int enterIndex;
    private TeleportRune runeScript;

    void Start()
    {
        runeScript = GameObject.FindGameObjectWithTag("GameManager").GetComponent<TeleportRune>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(runeScript.TeleportCharge(true, enterIndex));
        }
    }
}
