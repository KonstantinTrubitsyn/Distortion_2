using UnityEngine;

public class CarpetController : MonoBehaviour
{
    public Sprite rolledCarpet; // Спрайт откатанного ковра
    public Sprite unrolledCarpet; // Спрайт не откатанного ковра
    private SpriteRenderer spriteRenderer;
    private bool isRolled = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = unrolledCarpet; // Начальное состояние
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

