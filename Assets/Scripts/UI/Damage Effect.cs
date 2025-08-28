using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    private Health healthScript;

    public Image damageImage;

    [SerializeField] private float damageFlashDuration = 0.5f;
    [SerializeField] private float flashBoost = 0.3f;
    [SerializeField] private float fadeSpeed = 5f;

    private float baseOpacity;
    private float currentOpacity;
    private Coroutine flashRoutine;

    void Start()
    {
        healthScript = GetComponent<Health>();
    }

    void Update()
    {
        baseOpacity = 1f - (healthScript.currentHealth / healthScript.maxHealth);

        currentOpacity = Mathf.Lerp(currentOpacity, baseOpacity, Time.deltaTime * fadeSpeed);

        if (healthScript.hasBeenAttacked)
        {
            TriggerDamageFlash();
        }
        Color c = damageImage.color;
        c.a = Mathf.Clamp01(currentOpacity);
        damageImage.color = c;
    }

    public void TriggerDamageFlash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(DamageFlashRoutine());
    }

    private IEnumerator DamageFlashRoutine()
    {
        currentOpacity = Mathf.Clamp01(baseOpacity + flashBoost);

        float elapsed = 0f;

        while (elapsed < damageFlashDuration)
        {
            elapsed += Time.deltaTime;
            currentOpacity = Mathf.Lerp(currentOpacity, baseOpacity, elapsed / damageFlashDuration);

            Color c = damageImage.color;
            c.a = Mathf.Clamp01(currentOpacity);
            damageImage.color = c;

            yield return null;
        }

        currentOpacity = baseOpacity;
    }
}
