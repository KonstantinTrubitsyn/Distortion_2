using UnityEngine;

public class PegController : MonoBehaviour
{
    public float jumpHeight = 50f; // Высота прыжка
    public float jumpSpeed = 2f;  // Скорость прыжка (колебания вверх-вниз)
    public AudioClip hitSound;    // Звук удара об исходную точку
    public AudioSource audioSource; // Источник звука

    private RectTransform rectTransform;
    private float initialY;       // Начальная высота объекта
    public float threshold;
    private bool hasHitBottom = false; // Флаг для звука удара
    private float timeOffset;     // Смещение времени для синусоиды

    void Start()
    {
        // Получаем RectTransform компонента
        rectTransform = GetComponent<RectTransform>();

        // Устанавливаем начальную позицию относительно координат Canvas
        initialY = rectTransform.anchoredPosition.y;
        Debug.Log($"Initial anchored Y position set to: {initialY}"); // Отладка

        // Устанавливаем смещение времени для корректного старта синусоиды
        timeOffset = 0f; // Синусоида стартует из точки initialY

        // Проверка на наличие AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource не подключён!");
        }
    }

    void Update()
    {
        // Время для расчёта синусоиды
        float time = Time.time * jumpSpeed + timeOffset;

        // Считаем смещение по высоте
        float offsetY = Mathf.Sin(time) * jumpHeight / 2f + jumpHeight / 2f;

        // Новая позиция по оси Y относительно anchors
        float newY = initialY + offsetY;

        // Проверка на достижение нижней точки (исходной позиции)
        if (Mathf.Abs(offsetY) < 0.1f) // Почти у initialY
        {
            if (!hasHitBottom) // Если ещё не было удара
            {
                PlayHitSound(); // Проиграть звук
                hasHitBottom = true; // Устанавливаем флаг
                Debug.Log($"Hit bottom at Y: {newY}");
            }
        }
        else
        {
            hasHitBottom = false; // Сбрасываем флаг, когда объект движется вверх
        }

        // Обновляем позицию объекта относительно anchors
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
        Debug.Log($"New anchored Y position: {newY}");
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogWarning("Звук не воспроизведён. Проверь настройки AudioSource и AudioClip.");
        }
    }

    // Проверка, достаточно ли колышек поднят
    public bool IsAboveThreshold()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float currentY = rectTransform.anchoredPosition.y;

        // Проверяем, поднят ли колышек выше начальной высоты на порог
        return currentY >= initialY + threshold;
    }
}
