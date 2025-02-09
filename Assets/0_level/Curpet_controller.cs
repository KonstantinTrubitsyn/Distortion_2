using UnityEngine;

public class CarpetController : MonoBehaviour
{
    public Sprite rolledCarpet; // ������ ����������� �����
    public Sprite unrolledCarpet; // ������ �� ����������� �����
    private SpriteRenderer spriteRenderer;
    private bool isRolled = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unrolledCarpet; // ��������� ���������
    }

    private void OnMouseDown()
    {
        ToggleCarpet();
    }

    private void ToggleCarpet()
    {
        isRolled = !isRolled;
        spriteRenderer.sprite = isRolled ? rolledCarpet : unrolledCarpet;
    }

    public bool IsRolled()
    {
        return isRolled;
    }
}

