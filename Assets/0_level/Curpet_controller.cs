using UnityEngine;

public class CarpetController : MonoBehaviour
{
    public Sprite rolledCarpet; // Спрайт откатанного ковра
    public Sprite unrolledCarpet; // Спрайт не откатанного ковра
    private SpriteRenderer spriteRenderer;
    private Collider2D carpetCollider; // Ссылка на коллайдер ковра
    private bool isRolled = false;
    public KeyController keyController; // Ссылка на контроллер ключа
    public AudioClip openSound;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        carpetCollider = GetComponent<Collider2D>(); // Получаем коллайдер ковра
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = unrolledCarpet; // Начальное состояние
    }

    private void OnMouseDown()
    {
        ToggleCarpet();
    }

    private void ToggleCarpet()
    {
        audioSource.PlayOneShot(openSound);
        isRolled = !isRolled;
        spriteRenderer.sprite = isRolled ? rolledCarpet : unrolledCarpet;

        // Если ковёр откатан, отключаем коллайдер ковра и включаем ключ
        if (isRolled)
        {
            carpetCollider.enabled = false; // Отключаем коллайдер ковра
            keyController.EnableKey(); // Включаем ключ
        }
        else
        {
            carpetCollider.enabled = true; // Включаем коллайдер ковра
            keyController.DisableKey(); // Отключаем ключ
        }
    }

    public bool IsRolled()
    {
        return isRolled;
    }
}


