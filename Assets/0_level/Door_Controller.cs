using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorController : MonoBehaviour
{
    public KeyController keyController; // Ссылка на скрипт ключа
    public AudioClip openSound; // Звук открытия двери
    private AudioSource audioSource; // Источник звука

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Получаем компонент AudioSource
    }

    private void OnMouseDown()
    {
        if (keyController.IsPickedUp()) // Проверка, подобран ли ключ
        {
            // Проигрываем звук
            audioSource.PlayOneShot(openSound);

            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                Debug.Log("Все уровни пройдены!");
            }
        }
        else
        {
            Debug.Log("Ключ не подобран, дверь не открыта.");
        }
    }
}
