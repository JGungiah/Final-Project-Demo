using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class playerInteract : MonoBehaviour
{
    public List<string> sceneNames;
    public List<string> YggdrasilScenes;
   
    public GameObject interactImage;
    private bool hasBeenPressed;
    private Scene currentScene;
    public TextMeshProUGUI waveText;
    public TextMeshProUGUI roomNumberText;
    public TextMeshProUGUI roomNumber;
    private float numberOfRoomsCompleted = 0;
   
    public bool NormRooms  = false;
    public bool YggdrasilRooms = false;
    public Image uiArrow;
    private GameObject gate;
    private Quaternion rotation;
    private int count = 0;

    private GameObject boonCanvas;
    public GameObject canvas;

    private GameObject gameManager;
    private RandomizeBoons boonScript;

    private bool isChangingScene = false;

    private Health healthScript;
    private Player movementScript;
    private Attack attackScript;
    private GameObject loadanim;
    
    private SceneLoadManager sceneLoadManager;

    private GameObject runeManager;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        gate = GameObject.FindGameObjectWithTag("InteractDoor");
        boonCanvas = GameObject.FindWithTag("Boon UI");
        gameManager = GameObject.FindWithTag("GameManager");
        runeManager = GameObject.FindWithTag("RuneManager");
        boonScript = gameManager.GetComponent<RandomizeBoons>();    
        sceneLoadManager = runeManager.GetComponent<SceneLoadManager>();
        healthScript = GetComponent<Health>();
        movementScript = GetComponent<Player>();
        attackScript = GetComponent<Attack>();

        NormRooms = true;
    }

  
    void Update()
    {
        if (count == 4)
        {
            NormRooms = false;
            YggdrasilRooms = true;
        }
      
        loadanim = GameObject.FindWithTag("Load");
        
        if (boonScript.isActive && !isChangingScene && NormRooms)
        {
            StartCoroutine(SceneChangeDelay());
        }
        else if (boonScript.isActive && !isChangingScene && YggdrasilRooms) 
        {
            StartCoroutine (SceneChangeDelayYggdrasil());
        }
        if (gate == null)
        {
            gate = GameObject.FindGameObjectWithTag("InteractDoor");
        }

        if (gate != null && gate.activeInHierarchy)
        {
            if (!uiArrow.gameObject.activeSelf)
                uiArrow.gameObject.SetActive(true);

            RotateArrow();
        }
        else
        {
            if (uiArrow.gameObject.activeSelf)
                uiArrow.gameObject.SetActive(false);
        }

        if (!sceneLoadManager.isLoading)
        {
            canvas.SetActive(true);
        }

        else if (sceneLoadManager.isLoading)
        {
            canvas.SetActive(false);
        }



        waveText.text = " Wave "  +  enemySpawner.numberOfWavesCompleted;
        roomNumber.text = numberOfRoomsCompleted.ToString();

        if (hasBeenPressed)
        {
            interactImage.gameObject.SetActive(false);
        }

        CheckScene();
        
        if (currentScene.name == "LobbyRoom")
        {
            waveText.gameObject.SetActive(false);
            roomNumberText.gameObject.SetActive(false);
            roomNumber.gameObject.SetActive(false);
            canvas.SetActive(false);

            enemySpawner.numberOfWavesCompleted = 0;
            numberOfRoomsCompleted = 0;
            count = 0;
            //healthScript.ClearHealthBoons();
            //movementScript.ClearPlayerBoons();
            //attackScript.ClearAttackBoons();
        }
        else
        {
            waveText.gameObject.SetActive(true);
            roomNumberText.gameObject.SetActive(true);
            roomNumber.gameObject.SetActive(true);

        }

        
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindGateInScene();
    }
    void FindGateInScene()
    {
        gate = GameObject.FindGameObjectWithTag("InteractDoor");
     
    }

    void RotateArrow()
    {
        Vector3 gatePosition = gate.transform.position;
        Vector3 playerPosition = Camera.main.transform.position;
   
        Vector3 direction = gatePosition - playerPosition;
        direction.y = 0; 
     
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
  
        float angle = Vector3.SignedAngle(forward, direction, Vector3.up);
  
        uiArrow.rectTransform.localEulerAngles = new Vector3(0, 0, -angle );
    }
    public void LobbyScene()
    {
        SceneManager.LoadScene("LobbyRoom");
    }
    private void CheckScene()
    {
         currentScene = SceneManager.GetActiveScene();

    }
    public void SceneGenerator() 
    {

        int RandIndex = Random.Range(0, sceneNames.Count);
        
        string scenetoload = sceneNames[RandIndex];

        
        SceneManager.LoadScene(scenetoload);
        boonScript.RandomizeStatBoons();
        boonScript.AssignUIValues();
        numberOfRoomsCompleted ++;
    }
    public void SceneGeneratorYggdrasil()
    {

        int RandIndex = Random.Range(0, YggdrasilScenes.Count);

        string scenetoload = YggdrasilScenes[RandIndex];


        SceneManager.LoadScene(scenetoload);
        boonScript.RandomizeStatBoons();
        boonScript.AssignUIValues();
        numberOfRoomsCompleted++;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "InteractDoor")
        {
            interactImage.gameObject.SetActive(true);

            if (Input.GetKey(KeyCode.E))
            {
                interactImage.gameObject.SetActive(true);
              
                for (int i = 0; i < boonCanvas.transform.childCount; i++)
                {
                    Transform child = boonCanvas.transform.GetChild(i);
                    child.gameObject.SetActive(true);
 
                }
               
               
                hasBeenPressed = true;
            }
        }

       
    }

    private IEnumerator SceneChangeDelay()
    {

        loadanim.GetComponent<Animator>().SetBool("FadeIn", true);
        isChangingScene = true; 
      
        yield return new WaitForSeconds(4f);
       
        SceneGenerator();
        count++;
        isChangingScene = false;
        boonScript.isActive = false;
        print(count);
    }
    private IEnumerator SceneChangeDelayYggdrasil()
    {
        loadanim.GetComponent<Animator>().SetBool("FadeIn", true);
        isChangingScene = true;

        yield return new WaitForSeconds(4f);

        SceneGeneratorYggdrasil();

        isChangingScene = false;
        boonScript.isActive = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InteractDoor")
        {
            interactImage.gameObject.SetActive(false);
        }
    }
}
