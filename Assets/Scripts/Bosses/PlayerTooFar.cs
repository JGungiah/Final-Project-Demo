using UnityEngine;

public class PlayerTooFar : MonoBehaviour
{
    public JormungandrAttack FireProj;
    public GameObject Jorm;

    private void Start()
    {
        FireProj = Jorm.GetComponent<JormungandrAttack>();   
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            FireProj.RangedAttack();
        }
    }
}
