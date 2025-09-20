using UnityEngine;
using UnityEngine.VFX;

public class PortalVFx : MonoBehaviour
{
    public VisualEffect vfx;
    public Transform target;

    void Update()
    {    
            vfx.SetVector3("TargetPosition", target.position);
        
    }
}
