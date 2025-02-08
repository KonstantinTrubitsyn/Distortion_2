using UnityEngine;

public class PickItem : MonoBehaviour
{
    public string itemName; // Название предмета

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventorySystem.instance.AddItem(itemName); // Добавляем предмет в инвентарь
            Destroy(gameObject); // Удаляем объект с карты
        }
    }
}
