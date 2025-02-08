using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    public GameObject dialogueBox; // Окно диалога
    public Text dialogueText; // Текст NPC (Legacy UI)
    public Button nextButton; // Кнопка "Далее"
    public Text buttonText; // Текст на кнопке
    public Button closeButton; // Кнопка "Закрыть" (❌)
    
    public GameObject hintBox; // Подсказка
    public Text hintText; // Текст подсказки
    public Transform player; // Ссылка на игрока

    public string npcTag = "NPC"; // Тег NPC
    private bool isDialogueActive = false; // Флаг активности диалога
    private bool hasTalked = false; // Был ли уже разговор
    private int currentDialogueIndex = 0; // Индекс текущей реплики

    private string[] npcDialogues = // Реплики NPC
    {
        "Смотрительница: Франк... милый, ты ведь знаешь, что не должен быть здесь. Ты ведь знаешь, что двери запирают не просто так, верно?",
        "Смотрительница: Некоторые воспоминания лучше оставлять за закрытыми дверями.",
        "Смотрительница: Желтый. Какой ужасно ядовитый цвет. Напоминает яд, которым мы травим проворных, вечно шныряющих мотыльков.",
        "Смотрительница: Знаешь, Франк, иногда дети тоже бывают слишком любопытными... и тогда приходится их…. Возвращайся к себе, мой маленький мотылёк."
    };

    private string[] playerResponses = // Варианты ответа на кнопке
    {
        "Меня мучают кошмары.",
        "Мой шарфик,тот кто подарил мне его …",
        "Но что мне делать дальше?...",
        "Хорошо.До свидания..."
    };

    public AudioClip hintDisappearSound; // Звук исчезновения подсказки
    public AudioClip buttonClickSound; // Звук при нажатии на кнопку
    private AudioSource audioSource; // Источник звука

    void Start()
    {
        // Убедимся, что на объекте есть AudioSource. Если нет — добавим его.
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        dialogueBox.SetActive(false); // Скрываем диалог при старте
        hintBox.SetActive(true); // Показываем подсказку при старте
        hintText.text = "Поговорить со Смотрительницей"; // Устанавливаем текст
        nextButton.onClick.AddListener(NextDialogue); // Кнопка "Далее"
        closeButton.onClick.AddListener(EndDialogue); // Кнопка "Закрыть"
    }

    void Update()
    {
        // Подсказка следует за игроком, если диалог не активен и еще не был разговор
        if (!isDialogueActive && !hasTalked)
        {
            Vector3 hintPosition = new Vector3(player.position.x, player.position.y + 5.5f, player.position.z);
            hintBox.transform.position = Camera.main.WorldToScreenPoint(hintPosition);
        }

        // Запуск диалога при клике на NPC
        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag(npcTag))
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueBox.SetActive(true);
        hintBox.SetActive(false); // Скрываем подсказку при начале диалога
        hasTalked = true; // Помечаем, что разговор был
        currentDialogueIndex = 0;
        ShowDialogue();

        // Проигрываем звук исчезновения подсказки
        if (audioSource != null && hintDisappearSound != null)
        {
            audioSource.PlayOneShot(hintDisappearSound);
        }
    }

    void ShowDialogue()
    {
        if (currentDialogueIndex < npcDialogues.Length)
        {
            dialogueText.text = npcDialogues[currentDialogueIndex]; // Меняем текст NPC
            buttonText.text = playerResponses[currentDialogueIndex]; // Меняем текст на кнопке
        }
        else
        {
            EndDialogue();
        }
    }

    void NextDialogue()
    {
        // Проигрываем звук при нажатии на кнопку "Далее"
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }

        currentDialogueIndex++;
        ShowDialogue();
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueBox.SetActive(false);
        
        // Подсказка пропадает навсегда после первого разговора
        if (!hasTalked)
        {
            hintBox.SetActive(true);
        }
        else
        {
            // Проигрываем звук, когда подсказка исчезает
            if (audioSource != null && hintDisappearSound != null)
            {
                audioSource.PlayOneShot(hintDisappearSound);
            }
        }
    }
}
