using System.Collections;
using UnityEngine;

public class DashDetection : MonoBehaviour
{
    private GameObject player;
    private Player playerScript;
    private Collider collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent < Player >();
        collider = GetComponent< Collider >();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.isDashing)
        {
            StartCoroutine(DisableCollider());
        }
    }

    IEnumerator DisableCollider()
    {
        collider.enabled = false;
        yield return new WaitForSeconds(playerScript.dashDuration);
        collider.enabled = true;
    }
}
