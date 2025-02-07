using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Скорость движения
    public float jumpForce = 10f; // Сила прыжка
    private Rigidbody2D rb; // Компонент Rigidbody2D
    private bool isGrounded; // Находится ли персонаж на земле

    private AudioSource audioSource; // Для звуков шагов
    public AudioClip stepSound; // Клип со звуком шагов
    private bool isWalking = false; // Чтобы не проигрывать звук постоянно

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Движение влево-вправо
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Звук шагов
        if (moveInput != 0 && isGrounded)
        {
            if (!isWalking)
            {
                isWalking = true;
                InvokeRepeating("PlayStepSound", 0f, 0.5f); // Звук шагов каждые 0.5 сек
            }
        }
        else
        {
            isWalking = false;
            CancelInvoke("PlayStepSound");
        }

        // Прыжок
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    void PlayStepSound()
    {
        if (audioSource != null && stepSound != null)
        {
            audioSource.PlayOneShot(stepSound);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
