using UnityEngine;

public class CeilingInteraction : MonoBehaviour
{
    public GameObject blurBackground; // Панель замыливания
    public GameObject ceilingPopup;  // Окно с текстом

    private bool isPopupOpen = false; // Флаг для отслеживания состояния
    private bool isInteractionLocked = false; // Флаг для предотвращения двойного клика

    void Start()
    {
        // Убедиться, что UI скрыт
        blurBackground.SetActive(false);
        ceilingPopup.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (!isPopupOpen && !isInteractionLocked)
        {
            // Заблокировать взаимодействие, чтобы предотвратить двойное срабатывание
            isInteractionLocked = true;

            // Открыть замыливание и окно
            blurBackground.SetActive(true);
            ceilingPopup.SetActive(true);
            isPopupOpen = true;

            // Разблокировать через 0.1 секунды
            Invoke("UnlockInteraction", 0.1f);
        }
    }

    private void Update()
    {
        // Закрыть окно при клике
        if (isPopupOpen && Input.GetMouseButtonDown(0) && !isInteractionLocked)
        {
            // Заблокировать взаимодействие
            isInteractionLocked = true;

            // Закрыть замыливание и окно
            blurBackground.SetActive(false);
            ceilingPopup.SetActive(false);
            isPopupOpen = false;

            // Разблокировать через 0.1 секунды
            Invoke("UnlockInteraction", 0.1f);
        }
    }

    private void UnlockInteraction()
    {
        isInteractionLocked = false; // Разблокировать взаимодействие
    }
}
