using UnityEngine;
using UnityEngine.UI;

public class StartUIAnimation : MonoBehaviour
{
    public RectTransform startUI;
    public float moveSpeed = 200f;

    private bool shouldMove = false;

    public void HideStartUI()
    {
        shouldMove = true;
    }

    void Update()
    {
        if (shouldMove)
        {
            startUI.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime;

            if (startUI.anchoredPosition.y > 2000f)
            {
                gameObject.SetActive(false); // ou startUI.gameObject.SetActive(false);
            }
        }
    }
}
