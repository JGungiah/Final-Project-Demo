using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RuneExit : MonoBehaviour
{
    public int exitIndex;
    private TeleportRune runeScript;
    public GameObject VFX;
    public Image chargeRing;

    public AudioSource chargeSound;
    void Start()
    {
        runeScript = GameObject.FindGameObjectWithTag("RuneManager").GetComponent<TeleportRune>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            runeScript.canTeleport = true;


            if (runeScript.activeChargeCoroutines[exitIndex] != null)
            {
                StopCoroutine(runeScript.activeChargeCoroutines[exitIndex]);
                runeScript.activeChargeCoroutines[exitIndex] = null;
            }


            runeScript.activeChargeCoroutines[exitIndex] = StartCoroutine(runeScript.TeleportCharge(false, exitIndex));

            chargeSound.Play();
        }


    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            runeScript.canTeleport = false;


            if (runeScript.activeChargeCoroutines[exitIndex] != null)
            {
                StopCoroutine(runeScript.activeChargeCoroutines[exitIndex]);
                runeScript.activeChargeCoroutines[exitIndex] = null;
            }

            runeScript.chargeTimers[exitIndex] = 0f;

            chargeSound.Stop();
        }
    }


    private void Update()
    {
        chargeRing.fillAmount = runeScript.chargeTimers[exitIndex] / runeScript.teleportChargeTime;
        if (runeScript.runeCooldowns[exitIndex])
        {
            VFX.SetActive(false);
            chargeSound.Stop();
        }
        else
        {
            VFX.SetActive(true);
        }
    }
}
