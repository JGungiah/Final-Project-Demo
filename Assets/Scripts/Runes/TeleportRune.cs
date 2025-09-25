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

    [SerializeField] float teleportTime;
    private GameObject player;
    private CharacterController controller;

    public bool isTeleporting = false;

    private SpriteRenderer spriteRenderer;
    public bool[] runeCooldowns;
    private Player playerScript;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        controller = player.GetComponent<CharacterController>();
        spriteRenderer = player.GetComponent<SpriteRenderer>();

        playerScript = player.GetComponent<Player>();
        runeCooldowns = new bool[runeEnter.Length];
    }

    public IEnumerator TeleportCharge(bool fromEnter, int index)
    {
        if (isTeleporting || runeCooldowns[index]) yield break;

        yield return new WaitForSeconds(teleportChargeTime);

        if (isTeleporting || runeCooldowns[index]) yield break;

        isTeleporting = true;

 
        controller.enabled = false;

        float elapsedTime = 0;
        float playerY = player.transform.position.y;
        Vector3  startPos = player.transform.position;
       
        if (fromEnter)
        {
            while (elapsedTime < teleportTime)
            {
                playerScript.arrowUI.SetActive(false);
                spriteRenderer.enabled = false;
                elapsedTime += Time.deltaTime;
                Vector3 teleportedPos = player.transform.position = Vector3.Lerp(startPos, runeExit[index].transform.position, elapsedTime / teleportTime);
                teleportedPos.y = playerY;
                player.transform.position = teleportedPos;



                yield return null;
            }
        }

        else
        {
            Vector3 startPos2 = player.transform.position;
            while (elapsedTime < teleportTime)
            {
                playerScript.arrowUI.SetActive(false);
                spriteRenderer.enabled = false;
                elapsedTime += Time.deltaTime;
                Vector3 teleportedPos2 = Vector3.Lerp(startPos2, runeEnter[index].transform.position, elapsedTime / teleportTime);
                teleportedPos2.y = playerY;
                player.transform.position = teleportedPos2;



                yield return null;
            }
        }
           
       

        controller.enabled = true;
        isTeleporting = false;

        StartCoroutine(TeleportCooldown(index));
       

    }

    private IEnumerator TeleportCooldown(int index)
    {
        playerScript.arrowUI.SetActive(true);
        spriteRenderer.enabled = true;
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
