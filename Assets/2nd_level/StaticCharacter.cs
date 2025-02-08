using UnityEngine;

public class StaticCharacter : MonoBehaviour
{
    // Ссылка на объект диалогового окна, который будет показываться при клике
    public GameObject dialogueWindow;

    // Метод, который будет вызываться при клике на объект
    void OnMouseDown()
    {
        // Переключение видимости окна: если оно активно - скрыть, если скрыто - показать
        dialogueWindow.SetActive(!dialogueWindow.activeSelf);
    }
}
