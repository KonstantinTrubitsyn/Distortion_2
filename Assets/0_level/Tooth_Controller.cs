using UnityEngine;

public class KeyController : MonoBehaviour
{
    private bool isPickedUp = false; // ����, ������������, �������� �� ����
    public CarpetController carpetController; // ������ �� ������ �����

    private void OnMouseDown()
    {
        {
            // ���������, ������� �� ���� ����� ��� ��� �������� �� �����
            if (carpetController.IsRolled()) // ���� ���� �������
            {
                // ��������� ����, ���� �� ��� �� ��������
                if (!isPickedUp)
                {
                    isPickedUp = true;
                    // ��������� ��������� � ������, ����� ������ ���� "���������"
                    gameObject.GetComponent<Collider2D>().enabled = false;
                    gameObject.GetComponent<SpriteRenderer>().enabled = false;
                    Debug.Log("���� ��������!");
                }
            }
            else
            {
                Debug.Log("���������� ��������� ����, ���� �������!");
            }
        }
    }

    public bool IsPickedUp()
    {
        return isPickedUp;
    }
}
