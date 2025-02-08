using UnityEngine;
using UnityEngine.UI;

public class CharacterClick : MonoBehaviour
{
    public GameObject dialogueBox;  // Объект, в котором будет отображаться диалог
    public Text dialogueText;       // Текстовое поле для вывода текста диалога
    public string dialogue;         // Сам текст диалога

    void Update()
    {
        // Проверяем, был ли клик на персонажа
        if (Input.GetMouseButtonDown(0))  // 0 - это левая кнопка мыши
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)  // Проверяем, кликнули ли на этот персонаж
                {
                    ShowDialogue();
                }
            }
        }
    }

    // Функция для показа диалога
    void ShowDialogue()
    {
        dialogueBox.SetActive(true);  // Показываем окно диалога
        dialogueText.text = dialogue;  // Устанавливаем текст диалога
    }
}
