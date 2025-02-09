using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool isPickedUp = false; // Флаг, показывающий, подобран ли ключ
    public CarpetController carpetController; // Ссылка на скрипт ковра

    private void OnMouseDown()
    {
        {
            // Проверяем, откатан ли ковёр перед тем как кликнуть по ключу
            if (carpetController.IsRolled()) // Если ковёр откатан
            {
                // Подбираем ключ, если он ещё не подобран
                if (!isPickedUp)
                {
                    isPickedUp = true;
                    // Отключаем коллайдер и спрайт, чтобы объект стал "невидимым"
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log("Ключ подобран!");
                }
            }
            else
            {
                Debug.Log("Невозможно подобрать ключ, ковёр закатан!");
            }
        }
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
