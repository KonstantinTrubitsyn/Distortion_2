using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Синглтон для глобального доступа
    public static InventorySystem instance;

    // Хранилище предметов
    private List<string> inventory = new List<string>();

    private void Awake()
    {
        // Настраиваем синглтон
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Добавление предмета в инвентарь
    public void AddItem(string itemName)
    {
        inventory.Add(itemName);
        Debug.Log($"Добавлено в инвентарь: {itemName}");
    }

    // Проверка наличия предмета в инвентаре
    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    // Для отладки: вывести содержимое инвентаря
    public void ShowInventory()
    {
        Debug.Log("Инвентарь содержит:");
        foreach (string item in inventory)
        {
            Debug.Log(item);
        }
    }
}
