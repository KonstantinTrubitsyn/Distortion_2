using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public string requiredItem = "предмет"; // Предмет, необходимый для открытия двери

    private void OnMouseDown()
    {
        if (InventorySystem.instance.HasItem(requiredItem)) // Проверяем наличие предмета в инвентаре
        {
            Debug.Log("Дверь открыта!"); // Сообщение в консоли
            Destroy(gameObject); // Удаляем объект двери
        }
        else
        {
            Debug.Log("Дверь закрыта. Нужен: " + requiredItem); // Выводим сообщение, если предмета нет
        }
    }
}
