using UnityEngine;
using System.Collections;

public class UIBanner : MonoBehaviour
{
    public float moveDistance = 300f;
    public float moveSpeed = 500f;
    public float displayTime = 5f;

    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private Coroutine currentAnim;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
    }

    public void ShowBanner()
    {
        if (currentAnim != null) StopCoroutine(currentAnim);
        currentAnim = StartCoroutine(AnimateBanner());
    }

    private IEnumerator AnimateBanner()
    {
        Vector2 targetDown = originalPosition - new Vector2(0f, moveDistance);
        Vector2 targetUp = originalPosition;

        // Descend
        while (Vector2.Distance(rectTransform.anchoredPosition, targetDown) > 1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetDown, moveSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        // Remonte
        while (Vector2.Distance(rectTransform.anchoredPosition, targetUp) > 1f)
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, targetUp, moveSpeed * Time.deltaTime);
            yield return null;
        }

        currentAnim = null;
    }
}