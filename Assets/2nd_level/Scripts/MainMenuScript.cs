using UnityEngine;
using UnityEngine.SceneManagement; // для загрузки сцен

public class MainMenuScript : MonoBehaviour
{
    // Этот метод будет вызываться при нажатии на кнопку
    public void StartGame()
    {
        SceneManager.LoadScene("PrologueScene"); // Загрузка сцены пролога
    }
}
