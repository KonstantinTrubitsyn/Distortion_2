using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool isPickedUp = false; // ����, ������������, �������� �� ����
    private Collider2D keyCollider; // ������ �� ���������
    public CarpetController carpetController; // ������ �� ������ �����
    public AudioClip openSound;
    private AudioSource audioSource;

    private void Start()
    {
        keyCollider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        DisableKey(); // ��������� ��������� � ���� ����������
    }

    private void OnMouseDown()
    {
        if (carpetController.IsRolled()) // ���� ���� �������
        {
            if (!isPickedUp)
            {
                audioSource.PlayOneShot(openSound);
                isPickedUp = true;
                // ��������� ��������� � ������, ����� ������ ���� "���������"
                keyCollider.enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                Debug.Log("���� ��������!");
            }
        }
        else
        {
            Debug.Log("���������� ��������� ����, ���� �������!");
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
