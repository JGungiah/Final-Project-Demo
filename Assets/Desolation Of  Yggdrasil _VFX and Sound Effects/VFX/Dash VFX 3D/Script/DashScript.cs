using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DashScript : MonoBehaviour
{
    // === DASH SETTINGS ===
    [Header("Dash Settings")]
    public float activeTime = 2f;
    public bool isTrailActive;

    // === TRAIL MESH SETTINGS ===
    [Header("Mesh Settings")]
    public float meshRefreshRate = 0.1f;
    public float meshDestroyDelay = 3f;

    // === SHADER SETTINGS ===
    [Header("Shader Settings")]
    public Material mat;
    public string shaderVarRef = "_FadeAmount";
    public float shaderVarRate = 0.1f;
    public float shaderVarRefreshRate = 0.05f;

    // === LIGHTNING FX SETTINGS ===
    [Header("Lightning FX Settings")]
    public GameObject lightningFXPrefab;
    public Transform lightningAnchor;                 // Drag in your player root or an empty child
    public Vector3 lightningLocalOffset = new Vector3(0f, 0.906f, 0f);
    public float lightningRefreshRate = 0.1f;
    public float lightningLifetime = 1f;

    // === INTERNAL CACHES ===
    private SkinnedMeshRenderer[] skinnedMeshRenderers;
    private Queue<GameObject> meshPool = new Queue<GameObject>();

    void Start()
    {
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTrailActive)
        {
            isTrailActive = true;
            StartCoroutine(ActiveTrail(activeTime));
            StartCoroutine(SpawnLightningFX(activeTime));
        }
    }

    IEnumerator ActiveTrail(float timeActive)
    {
        float timer = 0f;
        while (timer < timeActive)
        {
            SpawnTrailMeshes();
            timer += meshRefreshRate;
            yield return new WaitForSeconds(meshRefreshRate);
        }
        isTrailActive = false;
    }

    void SpawnTrailMeshes()
    {
        foreach (var smr in skinnedMeshRenderers)
        {
            GameObject obj = GetMeshObject();
            obj.transform.position = smr.transform.position;
            obj.transform.rotation = smr.transform.rotation;

            Mesh mesh = new Mesh();
            smr.BakeMesh(mesh);

            MeshFilter mf = obj.GetComponent<MeshFilter>();
            MeshRenderer mr = obj.GetComponent<MeshRenderer>();
            mf.mesh = mesh;

            Material instanceMat = new Material(mat);
            mr.material = instanceMat;

            StartCoroutine(AnimateMaterialFloat(instanceMat, 0f, shaderVarRate, shaderVarRefreshRate));
            StartCoroutine(DeactivateAfterDelay(obj, meshDestroyDelay));
        }
    }

    IEnumerator SpawnLightningFX(float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            SpawnLightningFromAnchor();
            timer += lightningRefreshRate;
            yield return new WaitForSeconds(lightningRefreshRate);
        }
    }

    void SpawnLightningFromAnchor()
    {
        if (lightningFXPrefab == null || lightningAnchor == null)
            return;

        Vector3 spawnPos = lightningAnchor.TransformPoint(lightningLocalOffset);
        Quaternion spawnRot = lightningAnchor.rotation;

        GameObject fx = Instantiate(lightningFXPrefab, spawnPos, spawnRot);
        Destroy(fx, lightningLifetime);
    }

    GameObject GetMeshObject()
    {
        if (meshPool.Count > 0)
        {
            var pooledObj = meshPool.Dequeue();
            pooledObj.SetActive(true);
            return pooledObj;
        }

        var newObj = new GameObject("TrailMesh");
        newObj.AddComponent<MeshFilter>();
        newObj.AddComponent<MeshRenderer>();
        return newObj;
    }

    void ReturnMeshObject(GameObject obj)
    {
        obj.SetActive(false);
        meshPool.Enqueue(obj);
    }

    IEnumerator DeactivateAfterDelay(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnMeshObject(obj);
    }

    IEnumerator AnimateMaterialFloat(Material matInstance, float goal, float rate, float refreshRate)
    {
        if (!matInstance.HasProperty(shaderVarRef))
            yield break;

        float value = matInstance.GetFloat(shaderVarRef);
        while (value > goal)
        {
            value -= rate;
            matInstance.SetFloat(shaderVarRef, value);
            yield return new WaitForSeconds(refreshRate);
        }
    }
}