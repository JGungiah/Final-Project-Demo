using UnityEngine;
using TMPro;
using System.Collections;
public class TutorialText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private Animator textAnim;
    [SerializeField] private float reappearTime;

    private bool hasMoved = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tutorialText.text = "Press WASD to Move";
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || !hasMoved)
        {
            textAnim.SetTrigger("Dissapear");
            hasMoved = true;
            tutorialText.text = "Press Left Click to Attack";
            StartCoroutine(TextReappear());
            StartCoroutine(IdleText());

            if (Input.GetKey(KeyCode.Mouse0))
            {

            }
        }
    }


    private IEnumerator TextReappear()
    {
        yield return null;
        textAnim.SetTrigger("Reappear");
    }


    private IEnumerator IdleText()
    {
        yield return new WaitForSeconds(reappearTime);
        textAnim.SetTrigger("Idle");
    }
}


