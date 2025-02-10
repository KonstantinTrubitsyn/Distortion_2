using UnityEngine;

public class CharacterBob : MonoBehaviour
{
    public float bobSpeed = 2f; // �������� �����������
    public float bobAmount = 0.1f; // ��������� �����-���� ���������

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position; // ���������� ��������� �������
    }

    void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed) * bobAmount;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
