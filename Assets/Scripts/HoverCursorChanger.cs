using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class HoverCursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private Texture2D hoverCursorTexture;
    [SerializeField] private Texture2D defaultCursorTexture;
    [SerializeField] private Vector2 hotSpot = Vector2.zero;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursorTexture, hotSpot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(defaultCursorTexture, hotSpot, CursorMode.Auto);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SceneManager.LoadScene("Planete1_inside 1");
    }
}