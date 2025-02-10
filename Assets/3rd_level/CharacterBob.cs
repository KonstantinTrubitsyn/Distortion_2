using UnityEngine;

public class CharacterBob : MonoBehaviour
{
    public float bobSpeed = 2f; // Скорость покачивания
    public float bobAmount = 0.1f; // Насколько вверх-вниз двигается

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // Запоминаем стартовую позицию
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
