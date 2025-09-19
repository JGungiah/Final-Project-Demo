using UnityEngine;
using System.Collections;

public class TeleportRune : MonoBehaviour
{
    [SerializeField] private GameObject[] runeEnter;
    [SerializeField] private GameObject[] runeExit;
    [SerializeField] private float teleportChargeTime = 1f;
    [SerializeField] private float teleportCooldown = 0.5f; 

    private GameObject player;
    private CharacterController controller;

    private bool isTeleporting = false;
    private bool isOnCooldown = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();
    }

    public IEnumerator TeleportCharge(bool fromEnter, int index)
    {
        if (isTeleporting || isOnCooldown) yield break;

        yield return new WaitForSeconds(teleportChargeTime);

        if (isTeleporting || isOnCooldown) yield break;

        isTeleporting = true;

 
        controller.enabled = false;

        if (fromEnter)
            
            player.transform.position = runeExit[index].transform.position;

        else

            player.transform.position = runeEnter[index].transform.position;

        controller.enabled = true;


        StartCoroutine(TeleportCooldown());

        isTeleporting = false;
    }

    private IEnumerator TeleportCooldown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(teleportCooldown);
        isOnCooldown = false;
    }
}
