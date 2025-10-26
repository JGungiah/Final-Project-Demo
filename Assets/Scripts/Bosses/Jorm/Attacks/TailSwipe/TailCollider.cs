using System.Collections;
using UnityEngine;

public class TailCollider : MonoBehaviour
{
    private GameObject player;
    private CharacterController characterController;
    private Health HealthScript;
    public bool isIncollider;
    public Jormungandr Jorm;
    public GameObject JormBody;

    private bool isMidhealth;
    private bool isLowhealth;

    [SerializeField] private float tailSwipeDamage;
    [SerializeField] private float knockbackDuration;
    [SerializeField] private float knockbackPower;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        HealthScript = player.GetComponent<Health>();
        JormBody = GameObject.FindWithTag("Jormungandr");
        Jorm = JormBody.GetComponent<Jormungandr>();
        characterController = player.GetComponent<CharacterController>();
    }

    private void Update()
    {
        Swipe();
    }
    public void Swipe() 
    {
        if(Jorm.currentHealth <= 700 && isIncollider && !isMidhealth)
        {
            StartCoroutine(SwipeAttack());
            isMidhealth = true;
        }
        if (Jorm.currentHealth <= 400 && isIncollider && !isLowhealth)
        {
            StartCoroutine(SwipeAttack());
            isLowhealth = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            isIncollider = true;
            
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isIncollider = false;
        }
        }
    IEnumerator SwipeAttack()
    {
        while (isIncollider)
        {
           
            yield return new WaitForSeconds(3f);
            if (!isIncollider) yield break;
            StartCoroutine(KnockBack());
            HealthScript.currentHealth -= tailSwipeDamage;
            
        }
    }

    private IEnumerator KnockBack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < knockbackDuration)
        {
            characterController.enabled = false;
            print(1);
            player.transform.position += new Vector3(-1, 0, -1) * knockbackPower * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        characterController.enabled = true;


    }
}
