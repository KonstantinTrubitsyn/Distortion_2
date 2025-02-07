using UnityEngine;

public class TalkingObjectScript : MonoBehaviour
{
    public GameObject dialogueText; // UI объект для текста
    public string message = "Привет! Как дела?"; // Сообщение, которое будет отображаться

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Если игрок (или другой объект) входит в триггер
        if (other.CompareTag("Player")) // Убедитесь, что объект игрока имеет тег "Player"
        {
            dialogueText.SetActive(true); // Показываем текст
            dialogueText.GetComponent<TMPro.TextMeshProUGUI>().text = message; // Устанавливаем текст
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Если игрок уходит из триггера
        if (other.CompareTag("Player"))
        {
            dialogueText.SetActive(false); // Скрываем текст
        }
    }
}
