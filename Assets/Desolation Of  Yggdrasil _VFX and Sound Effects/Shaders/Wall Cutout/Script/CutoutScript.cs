using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class CutoutScript : MonoBehaviour
{
    [Header("Target & Wall Settings")]
    // The object we want to keep visible through walls (e.g., player)
    [SerializeField]
    private Transform targetObject;
    // Layer mask to define which objects are considered walls
    [SerializeField]
    private LayerMask wallMask;

    [Header("Cutout Shader Controls")]
    [SerializeField] [Range(0f, 1f)] private float cutoutSize = 0.1f;
    [SerializeField][Range(0f, 1f)] private float falloffSize = 0.05f;

    // Reference to the camera this script is attached to
    private Camera mainCam;

    private void Awake()
    {
        // Cache the camera component for viewport calculations
        mainCam = GetComponent<Camera>();
    }

    private void Update()
    {
        // Convert target's world position to viewport space (normalized 0–1)
        Vector2 cutoutPos = mainCam.WorldToViewportPoint(targetObject.position);

        // Optional aspect ratio correction (may be shader-dependent)
        cutoutPos.y /= (Screen.width / Screen.height);

        // Calculate direction and distance from camera to target
        Vector3 offSet = targetObject.position - transform.position ;

        // Raycast from camera to target to detect obstructing walls
        RaycastHit[] hitObjects = Physics.RaycastAll(transform.position, offSet, offSet.magnitude, wallMask);

        // Loop through all hit wall objects
        for (int i = 0; i < hitObjects.Length; i++)
        {
            // Get all materials on the hit object
            Material[] materials = hitObjects[i].transform.GetComponent<Renderer>().materials;

            // Apply cutout parameters to each material
            for (int m = 0; m < materials.Length; m++)
            {
                // Set the cutout center in viewport space
                materials[m].SetVector("_cutoutPosition", cutoutPos);

                // Set the size of the transparent hole
                materials[m].SetFloat("_cutoutSize", 0.08f);

                // Set the falloff radius for smooth blending
                materials[m].SetFloat("_falloffSize", 0.05f);
            }
        }
    }
}
