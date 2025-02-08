using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Ссылка на объект персонажа
    public float smoothSpeed = 0.125f; // Скорость сглаживания
    public Vector3 offset = new Vector3(0, 0, -20); // Смещение камеры относительно персонажа

    // Жёстко заданные границы движения камеры
    private float minX = -4f;
    private float minY = 0f;
    private float maxX = 4f;
    private float maxY = 0f;

    void LateUpdate()
    {
        // Целевая позиция камеры
        Vector3 desiredPosition = target.position + offset;

        // Ограничиваем позицию камеры
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);
        float clampedY = Mathf.Clamp(desiredPosition.y, minY, maxY);

        // Применяем ограничения
        Vector3 boundedPosition = new Vector3(clampedX, clampedY, desiredPosition.z);

        // Сглаженное движение камеры
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, boundedPosition, smoothSpeed);

        // Обновляем позицию камеры
        transform.position = smoothedPosition;
    }
}