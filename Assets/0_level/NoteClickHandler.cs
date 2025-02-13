using UnityEngine;

public class NoteClickHandler : MonoBehaviour
{
    public GameObject note1;            // Первая записка
    public GameObject note2;            // Вторая записка
    public GameObject darkOverlay;      // Затемняющий фон

    private bool isNote1Open = false;   // Состояние первой записки
    private bool isNote2Open = false;   // Состояние второй записки

    // Этот метод будет вызываться при клике на нужную часть фона
    void OnMouseDown()
    {
        if (IsMouseOverNote(note1))  // Проверка, на какой записке был клик
        {
            if (isNote1Open)
            {
                CloseNote1();  // Если записка 1 открыта, закрываем её
            }
            else
            {
                OpenNote1();   // Если записка 1 закрыта, открываем её
            }
        }
        else if (IsMouseOverNote(note2)) // Проверка для второй записки
        {
            if (isNote2Open)
            {
                CloseNote2();  // Если записка 2 открыта, закрываем её
            }
            else
            {
                OpenNote2();   // Если записка 2 закрыта, открываем её
            }
        }
    }

    // Проверка, находится ли курсор мыши над запиской
    bool IsMouseOverNote(GameObject note)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return note.GetComponent<Collider2D>().bounds.Contains(mousePos);
    }

    // Открытие первой записки
    void OpenNote1()
    {
        note1.SetActive(true);          // Показываем первую записку
        darkOverlay.SetActive(true);    // Затемняем фон
        isNote1Open = true;             // Устанавливаем состояние, что записка 1 открыта
    }

    // Закрытие первой записки
    void CloseNote1()
    {
        note1.SetActive(false);         // Скрываем первую записку
        darkOverlay.SetActive(false);   // Убираем затемнение
        isNote1Open = false;            // Устанавливаем состояние, что записка 1 закрыта
    }

    // Открытие второй записки
    void OpenNote2()
    {
        note2.SetActive(true);          // Показываем вторую записку
        darkOverlay.SetActive(true);    // Затемняем фон
        isNote2Open = true;             // Устанавливаем состояние, что записка 2 открыта
    }

    // Закрытие второй записки
    void CloseNote2()
    {
        note2.SetActive(false);         // Скрываем вторую записку
        darkOverlay.SetActive(false);   // Убираем затемнение
        isNote2Open = false;            // Устанавливаем состояние, что записка 2 закрыта
    }
}
