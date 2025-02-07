using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LockpickController : MonoBehaviour
{
    public float moveDistance = 500f;  // ���������� ��� ����������� ��� ����� ������� �������
    public float moveSpeed = 5f;       // �������� �����������
    public PegController[] pegs;       // ������ �� ������ ��������
    public RectTransform resetPosition; // ������ �� ������ ResetPosition
    public float collisionThreshold = 10f;  // ���������� ��������� ��� �������� ������������

    public AudioClip winSound;
    public AudioSource successSound;

    private RectTransform rectTransform;
    private int currentPegIndex = 0;   // ������ �������� ������� ��� �������� ������������
    private bool isMoving = false;     // ����, ������� ����������, ��� ������ � ��������
    private float targetPositionX;     // ������� ������� �� ��� X ��� �������
    private float startTime;           // ����� ������ ��������

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isMoving)
        {
            if (CheckCollision())
            {
                RestartGame(); // ���������� ���� ��� ������������
            }
            else
            {
                StartMoving(); // ������ ��������
            }
        }

        // ������� �������, ���� ��� � ��������
        if (isMoving)
        {
            MoveLockpick();
        }
    }

    private void StartMoving()
    {
        // ���������� ������� ������� � �������� ��������
        targetPositionX = rectTransform.anchoredPosition.x + moveDistance;
        startTime = Time.time;
        isMoving = true;
    }

    private void MoveLockpick()
    {
        // ������������, ������� ������� ������ � ������ ��������
        float journeyLength = Mathf.Abs(targetPositionX - rectTransform.anchoredPosition.x);
        float distanceCovered = (Time.time - startTime) * moveSpeed;

        // ������������, ��������� ������ ������� ������ �������������
        float fractionOfJourney = distanceCovered / journeyLength;

        // ���������� ������
        rectTransform.anchoredPosition = new Vector2(Mathf.Lerp(rectTransform.anchoredPosition.x, targetPositionX, fractionOfJourney), rectTransform.anchoredPosition.y);

        // ���� ������� �������� ������� �������, ������������� ��������
        if (fractionOfJourney >= 1f)
        {
            isMoving = false;
        }
    }

    private bool CheckCollision()
    {
        if (currentPegIndex >= pegs.Length) return false;

        // �������� ������� �������
        PegController currentPeg = pegs[currentPegIndex];

        // ���������, ������ �� ������� �� ����������� ������
        if (currentPeg.IsAboveThreshold())
        {
            Debug.Log($"������� #{currentPegIndex} ������� �������.");
            currentPegIndex++; // ��������� � ���������� �������

            if (currentPegIndex >= pegs.Length)
            {
                Debug.Log("����� �������! ������� �� ��������� �����.");
                if (successSound != null)
                {
                    successSound.PlayOneShot(winSound); // ����������� ���� ������
                }
                Invoke("LoadNextLevel", 1f); // ������� � ���������
            }

            return false; // �� � �������
        }
        else
        {
            Debug.Log($"������������ � �������� #{currentPegIndex}! ���������� ����.");
            return true; // ��������
        }
    }

    private void RestartGame()
    {
        Debug.Log("��������! ������� ������� � ��������� ���������...");

        // ������� ������ ResetPosition � ������������� ��� ������� ��� ���������
        GameObject resetPosition = GameObject.Find("ResetPosition");
        if (resetPosition != null)
        {
            Vector2 resetPos = resetPosition.GetComponent<RectTransform>().anchoredPosition;
            rectTransform.anchoredPosition = resetPos;
        }
        else
        {
            Debug.LogWarning("������ ResetPosition �� ������! ���������, ��� �� ���� � �����.");
        }

        // ���������� ������ �������� �������
        currentPegIndex = 0;

        // �������������� �������� (��������, ���������� ��������� �����������)
        // ...
    }

    private void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("��� ������ ��������!");
        }
    }

}
