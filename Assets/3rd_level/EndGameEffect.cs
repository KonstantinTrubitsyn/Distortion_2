using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndGameEffect : MonoBehaviour
{
    public Image endScreenImage; // Готовое изображение концовки
    public float fadeDuration = 2f; // Время затемнения экрана

    public void TriggerEndEffect()
    {
        StartCoroutine(ShowEndScreen());
    }

    private IEnumerator ShowEndScreen()
    {
        endScreenImage.gameObject.SetActive(true); // Активируем изображение

        Color imageColor = endScreenImage.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            endScreenImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, alpha);
            yield return null;
        }

        endScreenImage.color = new Color(imageColor.r, imageColor.g, imageColor.b, 1f);
    }
}
