using UnityEngine;

public class InstrumentManager : MonoBehaviour
{
    public GameObject[] instruments; // Массив инструментов в правильном порядке
    public AudioSource[] audioSources; // Аудио источники для каждого инструмента
    public AudioClip successClip; // Успешная мелодия
    public AudioClip failClip; // Звук при неправильной последовательности
    public Color highlightColor = Color.yellow; // Цвет подсветки
    public Color defaultColor = Color.white; // Цвет по умолчанию

    private int currentStep = 0; // Текущий шаг в последовательности
    private bool isLevelComplete = false; // Флаг завершения уровня
    private AudioSource failAudioSource; // Источник для звука ошибки
    private AudioSource successAudioSource; // Источник для финальной мелодии

    void Start()
    {
        ResetSequence();

        // Создаём AudioSource для финальной мелодии
        if (successAudioSource == null)
        {
            successAudioSource = gameObject.AddComponent<AudioSource>();
            successAudioSource.clip = successClip;
            successAudioSource.playOnAwake = false; // Отключаем автозапуск
        }

        // Создаём AudioSource для звука ошибки
        if (failAudioSource == null)
        {
            failAudioSource = gameObject.AddComponent<AudioSource>();
            failAudioSource.clip = failClip;
            failAudioSource.playOnAwake = false; // Отключаем автозапуск
        }
    }

    public void OnInstrumentClicked(GameObject instrument)
    {
        if (isLevelComplete)
            return; // Если уровень завершён, ничего не делаем

        // Найти индекс нажатого инструмента
        int instrumentIndex = System.Array.IndexOf(instruments, instrument);

        if (instrumentIndex == -1)
        {
            Debug.Log("Этот инструмент не найден в списке.");
            return;
        }

        // Проверяем правильность текущего шага
        if (instrumentIndex == currentStep)
        {
            // Подсветить инструмент
            var renderer = instrument.GetComponent<SpriteRenderer>();
            if (renderer != null)
            {
                renderer.color = highlightColor;
                Debug.Log($"Инструмент {instrument.name} подсвечен в цвет {highlightColor}.");
            }

            // Воспроизвести звук инструмента
            audioSources[instrumentIndex].Play();

            // Переход к следующему шагу
            currentStep++;

            // Если все инструменты нажаты в правильном порядке
            if (currentStep == instruments.Length)
            {
                Debug.Log("Уровень пройден!");
                ResetColors(); // Сбрасываем цвета
                PlaySuccessMelody(); // Играем мелодию
                isLevelComplete = true;
            }
        }
        else
        {
            Debug.Log("Неправильный порядок, сброс. Проигрываем звук ошибки.");
            PlayFailSound(); // Проиграть звук ошибки
        }
    }

    public void ResetSequence()
    {
        Debug.Log("Сброс последовательности.");

        // Сброс текущего шага
        currentStep = 0;

        // Останавливаем всю музыку
        foreach (var source in audioSources)
        {
            if (source != null)
                source.Stop();
        }

        // Сбрасываем подсветку всех инструментов
        ResetColors();

        // Останавливаем звуки ошибок и финальной мелодии
        if (failAudioSource != null && failAudioSource.isPlaying)
            failAudioSource.Stop();

        if (successAudioSource != null && successAudioSource.isPlaying)
            successAudioSource.Stop();

        isLevelComplete = false;
    }

    private void ResetColors()
    {
        // Сбрасываем цвет всех инструментов
        foreach (var instrument in instruments)
        {
            if (instrument != null)
            {
                var renderer = instrument.GetComponent<SpriteRenderer>();
                if (renderer != null)
                {
                    renderer.color = defaultColor;
                }
            }
        }
    }

    private void PlaySuccessMelody()
    {
        // Останавливаем все текущие звуки
        foreach (var source in audioSources)
        {
            if (source != null)
                source.Stop();
        }

        // Воспроизводим успешную мелодию
        if (successAudioSource != null && successClip != null)
        {
            successAudioSource.Play();
            Debug.Log("Играется финальная мелодия.");
        }
        else
        {
            Debug.LogWarning("Успешная мелодия не задана или AudioSource отсутствует.");
        }
    }

    private void PlayFailSound()
    {
        Debug.Log("Вызван метод PlayFailSound.");

        if (failAudioSource == null)
        {
            Debug.LogError("failAudioSource равен null. Проверьте создание AudioSource.");
            return;
        }

        if (failClip == null)
        {
            Debug.LogError("failClip не задан. Добавьте звуковой файл в поле Fail Clip.");
            return;
        }

        failAudioSource.clip = failClip;
        failAudioSource.Play();
        Debug.Log("Играется звук ошибки.");

        // Сбрасываем последовательность через время, равное длине клипа
        Invoke("ResetSequence", failClip.length);
    }
}
