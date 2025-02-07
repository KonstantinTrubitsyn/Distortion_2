using UnityEngine;

public class PegController : MonoBehaviour
{
    public float jumpHeight = 50f; // ������ ������
    public float jumpSpeed = 2f;  // �������� ������ (��������� �����-����)
    public AudioClip hitSound;    // ���� ����� �� �������� �����
    public AudioSource audioSource; // �������� �����

    private RectTransform rectTransform;
    private float initialY;       // ��������� ������ �������
    public float threshold;
    private bool hasHitBottom = false; // ���� ��� ����� �����
    private float timeOffset;     // �������� ������� ��� ���������

    void Start()
    {
        // �������� RectTransform ����������
        rectTransform = GetComponent<RectTransform>();

        // ������������� ��������� ������� ������������ ��������� Canvas
        initialY = rectTransform.anchoredPosition.y;
        Debug.Log($"Initial anchored Y position set to: {initialY}"); // �������

        // ������������� �������� ������� ��� ����������� ������ ���������
        timeOffset = 0f; // ��������� �������� �� ����� initialY

        // �������� �� ������� AudioSource
        if (audioSource == null)
        {
            Debug.LogError("AudioSource �� ���������!");
        }
    }

    void Update()
    {
        // ����� ��� ������� ���������
        float time = Time.time * jumpSpeed + timeOffset;

        // ������� �������� �� ������
        float offsetY = Mathf.Sin(time) * jumpHeight / 2f + jumpHeight / 2f;

        // ����� ������� �� ��� Y ������������ anchors
        float newY = initialY + offsetY;

        // �������� �� ���������� ������ ����� (�������� �������)
        if (Mathf.Abs(offsetY) < 0.1f) // ����� � initialY
        {
            if (!hasHitBottom) // ���� ��� �� ���� �����
            {
                PlayHitSound(); // ��������� ����
                hasHitBottom = true; // ������������� ����
                Debug.Log($"Hit bottom at Y: {newY}");
            }
        }
        else
        {
            hasHitBottom = false; // ���������� ����, ����� ������ �������� �����
        }

        // ��������� ������� ������� ������������ anchors
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, newY);
        Debug.Log($"New anchored Y position: {newY}");
    }

    private void PlayHitSound()
    {
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        else
        {
            Debug.LogWarning("���� �� ������������. ������� ��������� AudioSource � AudioClip.");
        }
    }

    // ��������, ���������� �� ������� ������
    public bool IsAboveThreshold()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float currentY = rectTransform.anchoredPosition.y;

        // ���������, ������ �� ������� ���� ��������� ������ �� �����
        return currentY >= initialY + threshold;
    }
}
