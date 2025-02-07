using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LockpickController : MonoBehaviour
{
    public float moveDistance = 500f;  // Расстояние для перемещения при одном нажатии пробела
    public float moveSpeed = 5f;       // Скорость перемещения
    public PegController[] pegs;       // Ссылка на массив колышков
    public RectTransform resetPosition; // Ссылка на объект ResetPosition
    public float collisionThreshold = 10f;  // Допустимая дистанция для проверки столкновений

    public AudioClip winSound;
    public AudioSource successSound;

    private RectTransform rectTransform;
    private int currentPegIndex = 0;   // Индекс текущего колышка для проверки столкновений
    private bool isMoving = false;     // Флаг, который показывает, что объект в движении
    private float targetPositionX;     // Целевая позиция по оси X для отмычки
    private float startTime;           // Время начала движения

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            if (CheckCollision())
            {
                RestartGame(); // Перезапуск игры при столкновении
            }
            else
            {
                StartMoving(); // Запуск движения
            }
        }

        // Двигаем отмычку, если она в движении
        if (isMoving)
        {
            MoveLockpick();
        }
    }

    private void StartMoving()
    {
        // Определяем целевую позицию и начинаем движение
        targetPositionX = rectTransform.anchoredPosition.x + moveDistance;
        startTime = Time.time;
        isMoving = true;
    }

    private void MoveLockpick()
    {
        // Рассчитываем, сколько времени прошло с начала движения
        float journeyLength = Mathf.Abs(targetPositionX - rectTransform.anchoredPosition.x);
        float distanceCovered = (Time.time - startTime) * moveSpeed;

        // Рассчитываем, насколько далеко отмычка должна переместиться
        float fractionOfJourney = distanceCovered / journeyLength;

        // Перемещаем объект
        rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, targetPositionX, fractionOfJourney), rectTransform.anchoredPosition.y);

        // Если отмычка достигла целевой позиции, останавливаем движение
        if (fractionOfJourney >= 1f)
        {
            isMoving = false;
        }
    }

    private bool CheckCollision()
    {
        if (currentPegIndex >= pegs.Length) return false;

        // Получаем текущий колышек
        PegController currentPeg = pegs[currentPegIndex];

        // Проверяем, поднят ли колышек на достаточную высоту
        if (currentPeg.IsAboveThreshold())
        {
            Debug.Log($"Колышек #{currentPegIndex} успешно пройден.");
            currentPegIndex++; // Переходим к следующему колышку

            if (currentPegIndex >= pegs.Length)
            {
                Debug.Log("Замок взломан! Переход на следующую сцену.");
                if (successSound != null)
                {
                    successSound.PlayOneShot(winSound); // Проигрываем звук взлома
                }
                Invoke("LoadNextLevel", 1f); // Переход с задержкой
            }

            return false; // Всё в порядке
        }
        else
        {
            Debug.Log($"Столкновение с колышком #{currentPegIndex}! Перезапуск игры.");
            return true; // Проигрыш
        }
    }

    private void RestartGame()
    {
        Debug.Log("Проигрыш! Возврат отмычки в начальное положение...");

        // Находим объект ResetPosition и устанавливаем его позицию как начальную
        GameObject resetPosition = GameObject.Find("ResetPosition");
        if (resetPosition != null)
        {
            Vector2 resetPos = resetPosition.GetComponent<RectTransform>().anchoredPosition;
            rectTransform.anchoredPosition = resetPos;
        }
        else
        {
            Debug.LogWarning("Объект ResetPosition не найден! Убедитесь, что он есть в сцене.");
        }

        // Сбрасываем индекс текущего колышка
        currentPegIndex = 0;

        // Дополнительные действия (например, визуальная индикация перезапуска)
        // ...
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("Все уровни пройдены!");
        }
    }

}
