using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    public Texture2D tex;
    public Vector2 size = new Vector2(64, 64);

    void OnGUI()
    {
        Cursor.visible = false;
        if (tex == null) return;

        Vector2 mousePos = Event.current.mousePosition;

        Rect rect = new Rect(
            mousePos.x - size.x / 2f,
            mousePos.y - size.y / 2f,
            size.x,
            size.y
        );

        GUI.DrawTexture(rect, tex);
    }
}
