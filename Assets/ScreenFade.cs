using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 0.3f;  // Durée du fondu
    public float fadeIntensity = 0.9f; // Opacité du noir

    private void Awake()
    {
        SetAlpha(0f); // Commence transparent
    }

    public void PlayFade()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutRoutine());
    }

    IEnumerator FadeOutRoutine()
    {
        // Démarre à fadeIntensity et diminue vers 0
        float timer = 0f;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(fadeIntensity, 0f, timer / fadeDuration);
            SetAlpha(alpha);
            timer += Time.deltaTime;
            yield return null;
        }
        SetAlpha(0f); // Assure que c’est bien transparent à la fin
    }

    private void SetAlpha(float alpha)
    {
        if (fadeImage != null)
        {
            Color c = fadeImage.color;
            c.a = alpha;
            fadeImage.color = c;
        }
    }
}