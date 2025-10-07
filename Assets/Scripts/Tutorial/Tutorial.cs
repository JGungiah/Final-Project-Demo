using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public TextMeshProUGUI tutorialText;
   
    public int count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          StartCoroutine(DisplayTutorial());
    }

    // Update is called once per frame
  
    IEnumerator DisplayTutorial() 
    {
        tutorialText.text = "Press WASD to move";
        yield return StartCoroutine(WaitForMovementInput());

        tutorialText.text = "Dash with space";
        yield return StartCoroutine(WaitForDashInput());

        tutorialText.text = "Press Right Click to Parry";
        yield return StartCoroutine(WaitForParryInput());


    }
    IEnumerator WaitForMovementInput()
    {
       
        yield return new WaitUntil(() =>
            Input.GetKeyDown(KeyCode.W) ||
            Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.D)
        );
    }

    IEnumerator WaitForDashInput()
    {
    
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
    }
    IEnumerator WaitForParryInput()
    {

        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse1));
    }
}

