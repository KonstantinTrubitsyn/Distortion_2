using UnityEngine;

public class CarpetController : MonoBehaviour
{
    public Sprite rolledCarpet; // ������ ����������� �����
    public Sprite unrolledCarpet; // ������ �� ����������� �����
    private SpriteRenderer spriteRenderer;
    private Collider2D carpetCollider; // ������ �� ��������� �����
    private bool isRolled = false;
    public KeyController keyController; // ������ �� ���������� �����
    public AudioClip openSound;
    private AudioSource audioSource;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        carpetCollider = GetComponent<Collider2D>(); // �������� ��������� �����
        audioSource = GetComponent<AudioSource>();
        spriteRenderer.sprite = unrolledCarpet; // ��������� ���������
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

        // ���� ���� �������, ��������� ��������� ����� � �������� ����
        if (isRolled)
        {
            carpetCollider.enabled = false; // ��������� ��������� �����
            keyController.EnableKey(); // �������� ����
        }
        else
        {
            carpetCollider.enabled = true; // �������� ��������� �����
            keyController.DisableKey(); // ��������� ����
        }
    }

    public bool IsRolled()
    {
        return isRolled;
    }
}


