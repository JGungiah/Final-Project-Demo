using System.Collections;
using UnityEngine;

public class DashDetection : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private Collider objectCollider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent < Player >();
        objectCollider = GetComponent< Collider >();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.hasDashed)
        {
            StartCoroutine(DisableCollider());
        }
    }

    IEnumerator DisableCollider()
    {
        objectCollider.enabled = false;
        yield return new WaitForSeconds(playerScript.dashDuration);
        objectCollider.enabled = true;
    }
}
