using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public KeyController keyController; // ������ �� ������ �����
    public AudioClip openSound; // ���� �������� �����
    private AudioSource audioSource; // �������� �����

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // �������� ��������� AudioSource
    }

    private void OnMouseDown()
    {
        if (keyController.IsPickedUp()) // ��������, �������� �� ����
        {
            // ����������� ����
            audioSource.PlayOneShot(openSound);

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
        else
        {
            Debug.Log("���� �� ��������, ����� �� �������.");
        }
    }
}
