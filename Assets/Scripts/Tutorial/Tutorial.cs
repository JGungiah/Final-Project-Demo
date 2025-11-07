using System.Collections;
using TMPro;
using UnityEngine;


public class Tutorial : MonoBehaviour
{

    public TextMeshProUGUI tutorialText;
    public TeleportRune teleportRune;
    public EnemyTutorial enemyTutorial;
    public GameObject enemy;

    private GameObject player;
    private Attack attackScript;
    public int count;

    public AudioSource completeSound;
    public GameObject dashImage;
    public GameObject blockImage;
    public GameObject[] teleportImage;
    public GameObject arrow;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          StartCoroutine(DisplayTutorial());
          player = GameObject.FindWithTag("Player");
          attackScript = player.GetComponent<Attack>();
        
    }
    public void Update()
    {
        teleportRune = GetComponent<TeleportRune>();
        enemyTutorial = enemy.GetComponent<EnemyTutorial>();
      
    }

    // Update is called once per frame

    IEnumerator DisplayTutorial() 
    {
        tutorialText.text = "Press WASD to move";
        yield return StartCoroutine(WaitForMovementInput());
        completeSound.Play();

        tutorialText.text = "Dash with space";
        yield return StartCoroutine(WaitForDashInput());
        completeSound.Play();

        dashImage.SetActive(false);
        tutorialText.text = "Press and hold right click to block";
        yield return StartCoroutine(WaitForParryInput());
        completeSound.Play();

        blockImage.SetActive(false);
        tutorialText.text = "Use runes to teleport";
        yield return StartCoroutine(WaitForteleportInput());
        completeSound.Play();

        foreach (GameObject obj in teleportImage)
        {
            obj.SetActive(false);
        }

        tutorialText.text = "Press left click to attack";
        yield return StartCoroutine(WaitForAttackInput());
        completeSound.Play();

        tutorialText.text = "Press left click in quick succession to chain attacks";
        yield return StartCoroutine(WaitForComboInput());
        completeSound.Play();

        tutorialText.text = "Go attack the Enemy";
        yield return StartCoroutine(WaitForEnemyInput());
        completeSound.Play();

        arrow.SetActive(true);
        tutorialText.text = "Follow the arrow and press E on the gate to leave";
    }
    IEnumerator WaitForMovementInput()
    {
       
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||Input.GetKeyDown(KeyCode.D)
        );
    }

    IEnumerator WaitForDashInput()
    {
        dashImage.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }
    IEnumerator WaitForParryInput()
    {
        blockImage.SetActive(true);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse1));
    }
    IEnumerator WaitForteleportInput()
    {
        foreach (GameObject obj in teleportImage)
        {
            obj.SetActive(true);
        }

        yield return new WaitUntil(() => teleportRune.isTeleporting is true);
    }
    IEnumerator WaitForEnemyInput()
    {
       
        yield return new WaitUntil(() => enemyTutorial.isHit is true);
       
    }

    IEnumerator WaitForAttackInput()
    {

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));
    }

    IEnumerator WaitForComboInput()
    {

        yield return new WaitUntil(() => attackScript.Hit3 is true);
    }
}

