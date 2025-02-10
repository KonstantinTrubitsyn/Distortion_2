using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool isPickedUp = false; // Флаг, показывающий, подобран ли ключ
    private Collider2D keyCollider; // Ссылка на коллайдер
    public CarpetController carpetController; // Ссылка на скрипт ковра
    public AudioClip openSound;
    private AudioSource audioSource;

    private void Start()
    {
        keyCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        DisableKey(); // Начальное состояние — ключ недоступен
    }

    private void OnMouseDown()
    {
        if (carpetController.IsRolled()) // Если ковёр откатан
        {
            if (!isPickedUp)
            {
                audioSource.PlayOneShot(openSound);
                isPickedUp = true;
                // Отключаем коллайдер и спрайт, чтобы объект стал "невидимым"
                keyCollider.enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                Debug.Log("Ключ подобран!");
            }
        }
        else
        {
            Debug.Log("Невозможно подобрать ключ, ковёр закатан!");
        }
    }

    public void EnableKey()
    {
        keyCollider.enabled = true;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    public void DisableKey()
    {
        keyCollider.enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
