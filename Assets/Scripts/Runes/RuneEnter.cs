using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class RuneEnter : MonoBehaviour
{
    public int enterIndex;
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


            if (runeScript.activeChargeCoroutines[enterIndex] != null)
            {
                StopCoroutine(runeScript.activeChargeCoroutines[enterIndex]);
                runeScript.activeChargeCoroutines[enterIndex] = null;
            }

           
            runeScript.activeChargeCoroutines[enterIndex] = StartCoroutine(runeScript.TeleportCharge(true, enterIndex));

            chargeSound.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            runeScript.canTeleport = false;


            if (runeScript.activeChargeCoroutines[enterIndex] != null)
            {
                StopCoroutine(runeScript.activeChargeCoroutines[enterIndex]);
                runeScript.activeChargeCoroutines[enterIndex] = null;
            }

            runeScript.chargeTimers[enterIndex] = 0f;

            chargeSound.Stop();
        }

        
    }

    private void Update()
    {
        chargeRing.fillAmount = runeScript.chargeTimers[enterIndex]  / runeScript.teleportChargeTime;
        if (runeScript.runeCooldowns[enterIndex])
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

