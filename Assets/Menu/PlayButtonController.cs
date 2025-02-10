using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButtonController : MonoBehaviour
{
    public AudioClip openSound;
    private AudioSource audioSource;
    private void OnMouseDown()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            audioSource.PlayOneShot(openSound);
        }
        else
        {
            Debug.Log("Все уровни пройдены!");
        }
    }
}
