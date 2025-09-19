using UnityEngine;
using System.Collections;

public class TeleportRune : MonoBehaviour
{
    [SerializeField] private GameObject[] runeEnter;
    [SerializeField] private GameObject[] runeExit;
    [SerializeField] private SpriteRenderer[] enterRenderers;
    [SerializeField] private SpriteRenderer[] exitRenderers;
    [SerializeField] private Color[] originalColors;
    [SerializeField] private float teleportChargeTime ;
    [SerializeField] private float teleportCooldown ; 

    private GameObject player;
    private CharacterController controller;

    private bool isTeleporting = false;


    private bool[] runeCooldowns;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();



        runeCooldowns = new bool[runeEnter.Length];
    }

    public IEnumerator TeleportCharge(bool fromEnter, int index)
    {
        if (isTeleporting || runeCooldowns[index]) yield break;

        yield return new WaitForSeconds(teleportChargeTime);

        if (isTeleporting || runeCooldowns[index]) yield break;

        isTeleporting = true;

 
        controller.enabled = false;

        if (fromEnter)
            
            player.transform.position = runeExit[index].transform.position;

        else

            player.transform.position = runeEnter[index].transform.position;

        controller.enabled = true;


        StartCoroutine(TeleportCooldown(index));

        isTeleporting = false;
    }

    private IEnumerator TeleportCooldown(int index)
    {
        runeCooldowns[index] = true;


        SetRuneOpacity(index, 0.05f);

        yield return new WaitForSeconds(teleportCooldown);

        exitRenderers[index].color = originalColors[index];
        enterRenderers[index].color = originalColors[index];

        runeCooldowns[index] = false;
    }

    private void SetRuneOpacity(int index, float alpha)
    {
        Color c = exitRenderers[index].color;
        Color c2 = enterRenderers[index].color;
        c.a = alpha;
        c2.a = alpha;
        enterRenderers[index].color = c2;
        exitRenderers[index].color = c;
    }
}
