using UnityEngine;

public class NoteInteraction : MonoBehaviour
{
    public GameObject blurBackground; // Панель замыливания
    public GameObject unfoldedNote;  // Объект развернутой записки

    private bool isNoteOpen = false; // Флаг для отслеживания состояния
    private bool isInteractionLocked = false; // Флаг для предотвращения двойного клика

    void Start()
    {
        // Убедиться, что все UI-объекты скрыты
        blurBackground.SetActive(false);
        unfoldedNote.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isNoteOpen && !isInteractionLocked)
        {
            // Заблокировать взаимодействие, чтобы предотвратить конфликт
            isInteractionLocked = true;

            // Открыть записку и замыливание
            blurBackground.SetActive(true);
            unfoldedNote.SetActive(true);
            isNoteOpen = true;

            // Разблокировать взаимодействие через 0.1 секунды
            Invoke("UnlockInteraction", 0.1f);

            Debug.Log("Note opened: " + unfoldedNote.name);
        }
    }

    public void CloseUI()
    {
        if (isNoteOpen && !isInteractionLocked)
        {
            // Закрыть записку и замыливание
            blurBackground.SetActive(false);
            unfoldedNote.SetActive(false);
            isNoteOpen = false;

            Debug.Log("Note closed.");
        }
    }

    private void Update()
    {
        if (isNoteOpen && Input.GetMouseButtonDown(0))
        {
            CloseUI();
        }
    }

    private void UnlockInteraction()
    {
        isInteractionLocked = false; // Разблокировать взаимодействие
    }
}
