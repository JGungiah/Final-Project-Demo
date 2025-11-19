using UnityEngine;
using UnityEngine.VFX;

public class FireVFX : MonoBehaviour
{
    public VisualEffect vfx;
    public Transform target;

    void Update()
    {
        vfx.SetVector3("Position", target.position);
        vfx.Reinit();

    }
}
