using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 7f; // Скорость движения персонажа
    private Rigidbody2D rb; // Rigidbody2D персонажа

    public AudioClip walkSound; // Звук шагов
    private AudioSource audioSource; // Компонент AudioSource

    private bool isMoving = false; // Флаг, двигается ли персонаж
    private bool facingRight = true; // Направление персонажа

    // Координаты области движения
    private Vector2 minBounds = new Vector2(-16f, -2.5f);
    private Vector2 maxBounds = new Vector2(16f, -0.5f);

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Убираем влияние гравитации

        // Инициализируем AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            // Если компонента AudioSource нет, добавляем его
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.clip = walkSound; // Привязываем звук шагов
        audioSource.loop = true; // Включаем зацикливание
    }

    void Update()
    {
        // Получаем ввод от пользователя
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Двигаем персонажа
        Vector2 movement = new Vector2(moveX, moveY).normalized * speed;
        rb.linearVelocity = movement; // Устанавливаем скорость

        // Ограничиваем движение персонажа в пределах области
        rb.position = new Vector2(
            Mathf.Clamp(rb.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(rb.position.y, minBounds.y, maxBounds.y)
        );

        // Проверяем, движется ли персонаж
        isMoving = moveX != 0 || moveY != 0;

        // Воспроизводим или останавливаем звук шагов
        if (isMoving && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        else if (!isMoving && audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        // Проверяем направление движения и поворачиваем персонажа
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Инвертируем направление
        transform.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Останавливаем движение при столкновении со стеной
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
