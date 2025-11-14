using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GrowYggdrasilVines : MonoBehaviour
{
    [Header("Visual Setup")]
    public List<MeshRenderer> growVinesMesh;     // Meshes with vine materials
    public float growTime = 5f;                  // Time to fully grow vines
    public float refreshRate = 0.05f;            // Update interval for growth
    [Range(0, 1)] public float minGrow = 0.2f;   // Starting shader value
    [Range(0, 1)] public float maxGrow = 1f;     // Ending shader value

    [Header("AOE Damage Settings")]
    public float aoeRadius = 5f;                 // Radius of damage
    public LayerMask damageLayer;                // Layers to check for targets
    public int damageAmount = 10;                // Damage per target
    public Transform aoeCenter;                  // Center of AOE
    public string targetTag = "Player";          // Tag to filter damage targets

    [Header("Trigger Settings")]
    public bool autoTriggerOnStart = false;      // Optional auto-fire on Start
    public bool useTimerTrigger = false;         // Enable timer-based activation
    public float attackInterval = 10f;           // Time between attacks

    private List<Material> growVineMaterials = new(); // Collected vine materials
    private bool fullyGrown = false;                   // Tracks growth direction
    private float timer = 0f;                          // Internal timer

    void Start()
    {
        // Collect vine materials with "_Vines_Grow" property
        foreach (var mesh in growVinesMesh)
        {
            foreach (var mat in mesh.materials)
            {
                if (mat.HasProperty("_Vines_Grow"))
                {
                    mat.SetFloat("_Vines_Grow", minGrow);
                    growVineMaterials.Add(mat);
                }
            }
        }

        if (autoTriggerOnStart)
        {
            TriggerAOE();
        }
    }

    void Update()
    {
        // Manual trigger for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TriggerAOE();
        }

        // Timer-based trigger
        if (useTimerTrigger)
        {
            timer += Time.deltaTime;
            if (timer >= attackInterval)
            {
                TriggerAOE();
                timer = 0f;
            }
        }
    }

    
    /// Triggers vine growth. Damage is applied only when vines reach full growth.
    
    public void TriggerAOE()
    {
        foreach (var mat in growVineMaterials)
        {
            StartCoroutine(GrowVines(mat));
        }
    }

 
    /// Coroutine to animate vine growth using time-based interpolation.
    
    IEnumerator GrowVines(Material mat)
    {
        float startValue = mat.GetFloat("_Vines_Grow");
        float endValue = fullyGrown ? minGrow : maxGrow;
        float elapsed = 0f;

        while (elapsed < growTime)
        {
            float t = elapsed / growTime;
            float growValue = Mathf.Lerp(startValue, endValue, t);
            mat.SetFloat("_Vines_Grow", growValue);
            elapsed += refreshRate;
            yield return new WaitForSeconds(refreshRate);
        }

        mat.SetFloat("_Vines_Grow", endValue);
        fullyGrown = endValue >= maxGrow;

        // Apply damage only when vines reach full growth
        if (fullyGrown)
        {
            ApplyAOEDamage();
        }
    }

   
    /// Applies damage to all targets within AOE radius that are tagged "Player".
    
    private void ApplyAOEDamage()
    {
        Collider[] hitColliders = Physics.OverlapSphere(aoeCenter.position, aoeRadius, damageLayer);
        foreach (var hit in hitColliders)
        {
            if (hit.CompareTag(targetTag) && hit.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damageAmount);
            }
        }
    }
}


/// Interface for damageable entities.

public interface IDamageable
{
    void TakeDamage(int amount);
}