using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    [SerializeField] private Vector3 Offset = new Vector3 (-5f, 5.5f, -5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.position + Offset;
    }
}
