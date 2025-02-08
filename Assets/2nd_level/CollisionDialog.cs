using UnityEngine;

public class CollisionDialog : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, если объект столкнулся с нужным объектом
        if (collision.gameObject.CompareTag("SecondObject"))
        {
            // Показываем сообщение
            Debug.Log("Объекты столкнулись!");
        }
    }
}
