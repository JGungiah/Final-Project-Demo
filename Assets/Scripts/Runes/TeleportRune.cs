using UnityEngine;
using System.Collections;

public class TeleportRune : MonoBehaviour
{
    [SerializeField] private GameObject[] runeEnter;
    [SerializeField] private GameObject[] runeExit;
    [SerializeField] private int[] runeIndex;


    [SerializeField] private float teleportChargeTime;

    private bool isTeleporting = false;

    private GameObject player;
    private CharacterController controller;

    private RuneEnter RuneEnter;
    private RuneExit RuneExit;

    private bool canEnter = false;
    private bool canExit = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();

      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Teleport Enter"))
        {
            canEnter = true;
            StartCoroutine(TeleportCharge());
            
        } 
        else if (other.CompareTag("Teleport Exit"))
        {
            canExit = true;
            StartCoroutine(TeleportCharge());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Teleport Enter"))
        {
            canEnter = false;
            StopCoroutine(TeleportCharge());
        }
        else if (other.CompareTag("Teleport Exit"))
        {
            canExit= false;
            StopCoroutine(TeleportCharge());
        }
    }

    private IEnumerator TeleportCharge()
    {
        yield return new WaitForSeconds(teleportChargeTime);
        controller.enabled = false;
        isTeleporting = true;
       
        if (canEnter)
        {
            player.transform.position = runeExit[RuneExit.exitIndex].transform.position;
            print(1);
        }

        if (canExit)
        {
            player.transform.position = runeEnter[RuneEnter.enterIndex].transform.position;
            print(2);
        }

        controller.enabled = true;
        isTeleporting = false;

    }
}
