using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneLoadManager : MonoBehaviour
{
    public GameObject loadingScreenUI;
    public Transform spawnPoint;
    public float maxCheckTime = 3f;

    private GameObject player;
    private CharacterController controller;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(HandleSceneLoad());
    }

    IEnumerator HandleSceneLoad()
    {
        Time.timeScale = 0f;
        loadingScreenUI.SetActive(true);

        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<CharacterController>();

        float elapsed = 0f;
        float minTime = 2f; // <-- always show for at least 2 seconds
        bool grounded = false;

        // Place slightly above spawn to allow fall
        player.transform.position = spawnPoint.position + Vector3.up * 0.5f;

        while (elapsed < maxCheckTime)
        {
            if (!controller.isGrounded)
            {
                // manually simulate gravity while frozen
                controller.Move(Vector3.down * 9.81f * Time.unscaledDeltaTime);
            }
            else
            {
                grounded = true;
            }

            elapsed += Time.unscaledDeltaTime;

            // If player is grounded AND we've passed minimum time → break early
            if (grounded && elapsed >= minTime)
                break;

            yield return null;
        }

        // Clean up
        loadingScreenUI.SetActive(false);
        Time.timeScale = 1f;
    }
}