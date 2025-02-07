using UnityEngine;
using UnityEngine.UI;
using TMPro; // Для TextMeshPro
using System.Collections;

public class EndGameEffect : MonoBehaviour
{
    public Image darkScreen; // Экран для затемнения
    public TextMeshProUGUI endText; // Текст "To Be Continued..."
    public float fadeDuration = 2f; // Время для затемнения экрана
    public float textFadeDelay = 1f; // Задержка перед показом текста
    public float textFadeDuration = 2f; // Время для появления текста

    public void TriggerEndEffect()
    {
        if (darkScreen == null || endText == null)
        {
            Debug.LogError("DarkScreen или EndText не привязаны в инспекторе!");
            return;
        }

        // Активируем DarkScreen перед началом эффекта
        darkScreen.gameObject.SetActive(true);
        endText.gameObject.SetActive(false); // Текст остаётся скрытым до появления

        StartCoroutine(FadeToBlackAndShowText());
    }

    private IEnumerator FadeToBlackAndShowText()
    {
        // 1. Плавное затемнение экрана
        Color screenColor = darkScreen.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            darkScreen.color = new Color(screenColor.r, screenColor.g, screenColor.b, alpha); // Устанавливаем альфа-канал
            yield return null; // Ждём следующий кадр
        }

        darkScreen.color = new Color(screenColor.r, screenColor.g, screenColor.b, 1f); // Устанавливаем полный чёрный цвет

        // 2. Задержка перед появлением текста
        yield return new WaitForSeconds(textFadeDelay);

        // 3. Плавное появление текста
        endText.gameObject.SetActive(true);
        Color textColor = endText.color;
        textColor.a = 0f; // Начинаем с полной прозрачности
        endText.color = textColor;

        elapsedTime = 0f;

        while (elapsedTime < textFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / textFadeDuration);
            endText.color = new Color(textColor.r, textColor.g, textColor.b, alpha);
            yield return null; // Ждём следующий кадр
        }

        endText.color = new Color(textColor.r, textColor.g, textColor.b, 1f); // Устанавливаем полный белый цвет
    }
}
