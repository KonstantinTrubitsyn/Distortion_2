using UnityEngine;

public class NoteClickHandler : MonoBehaviour
{
    public GameObject note1;            // ������ �������
    public GameObject note2;            // ������ �������
    public GameObject darkOverlay;      // ����������� ���

    private bool isNote1Open = false;   // ��������� ������ �������
    private bool isNote2Open = false;   // ��������� ������ �������

    // ���� ����� ����� ���������� ��� ����� �� ������ ����� ����
    void OnMouseDown()
    {
        if (IsMouseOverNote(note1))  // ��������, �� ����� ������� ��� ����
        {
            if (isNote1Open)
            {
                CloseNote1();  // ���� ������� 1 �������, ��������� �
            }
            else
            {
                OpenNote1();   // ���� ������� 1 �������, ��������� �
            }
        }
        else if (IsMouseOverNote(note2)) // �������� ��� ������ �������
        {
            if (isNote2Open)
            {
                CloseNote2();  // ���� ������� 2 �������, ��������� �
            }
            else
            {
                OpenNote2();   // ���� ������� 2 �������, ��������� �
            }
        }
    }

    // ��������, ��������� �� ������ ���� ��� ��������
    bool IsMouseOverNote(GameObject note)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return note.GetComponent<Collider2D>().bounds.Contains(mousePos);
    }

    // �������� ������ �������
    void OpenNote1()
    {
        note1.SetActive(true);          // ���������� ������ �������
        darkOverlay.SetActive(true);    // ��������� ���
        isNote1Open = true;             // ������������� ���������, ��� ������� 1 �������
    }

    // �������� ������ �������
    void CloseNote1()
    {
        note1.SetActive(false);         // �������� ������ �������
        darkOverlay.SetActive(false);   // ������� ����������
        isNote1Open = false;            // ������������� ���������, ��� ������� 1 �������
    }

    // �������� ������ �������
    void OpenNote2()
    {
        note2.SetActive(true);          // ���������� ������ �������
        darkOverlay.SetActive(true);    // ��������� ���
        isNote2Open = true;             // ������������� ���������, ��� ������� 2 �������
    }

    // �������� ������ �������
    void CloseNote2()
    {
        note2.SetActive(false);         // �������� ������ �������
        darkOverlay.SetActive(false);   // ������� ����������
        isNote2Open = false;            // ������������� ���������, ��� ������� 2 �������
    }
}
