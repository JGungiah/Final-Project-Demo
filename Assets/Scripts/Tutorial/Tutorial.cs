using System.Collections;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{

    public TextMeshProUGUI WASD;
    public TextMeshProUGUI Combo;
    public TextMeshProUGUI Dash;
    public TextMeshProUGUI Parry;
    public TextMeshProUGUI Teleport;
    public TextMeshProUGUI Enemy;
    public TextMeshProUGUI Leave;
    public int count;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
          StartCoroutine(DisplayTutorial());
    }

    // Update is called once per frame
    void Update()
    {
       if (count == 7) 
        {
            StartCoroutine(DisplayTutorial());
            count = 0;
        }
    }
    IEnumerator DisplayTutorial() 
    {
       WASD.enabled = true;
        count++;
       yield return new WaitForSecondsRealtime(5f);
       Combo.enabled = true;
       WASD.enabled = false;
        count++;    
       yield return new WaitForSecondsRealtime(5f);
       Combo.enabled = false;
       Dash.enabled = true;
        count++;
        yield return new WaitForSecondsRealtime(5f);
       Dash.enabled = false;
       Parry.enabled = true;
        count++;
        yield return new WaitForSecondsRealtime(5f);
        Teleport.enabled = true;
        Parry.enabled = false;
        count++;
        yield return new WaitForSecondsRealtime(5f);
        Enemy.enabled = true;
        Teleport.enabled= false;
        count++;
        yield return new WaitForSecondsRealtime(5f);
        Leave.enabled = true;
        Enemy.enabled = false;
        yield return new WaitForSecondsRealtime(5f);
        Leave.enabled = false;
        count++;
        yield return null;

    }
}
