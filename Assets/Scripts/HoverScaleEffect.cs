using UnityEngine;
using UnityEngine.EventSystems;

public class HoverScaleEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
    private Vector3 originalScale;
    public float tweenTime = 0.2f;

    void Start() {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        LeanTween.scale(gameObject, hoverScale, tweenTime).setEaseOutBack();
    }

    public void OnPointerExit(PointerEventData eventData) {
        LeanTween.scale(gameObject, originalScale, tweenTime).setEaseOutBack();
    }
}