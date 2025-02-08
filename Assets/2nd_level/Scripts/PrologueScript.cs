using UnityEngine;
using UnityEngine.UI; // Для работы с UI

public class PrologueScript : MonoBehaviour
{
    public Image imageDisplay; // Ссылка на объект Image
    public Sprite[] prologueImages; // Массив изображений для пролога
    private int currentImageIndex = 0; // Индекс текущей картинки

    void Start()
    {
        // Убедитесь, что картинки загружены
        if (prologueImages.Length > 0)
        {
            imageDisplay.sprite = prologueImages[currentImageIndex]; // Показываем первую картинку
        }
    }

    // Этот метод будет вызываться для перехода к следующей картинке
    public void NextImage()
    {
        currentImageIndex++;
        if (currentImageIndex >= prologueImages.Length)
        {
            // Если картинки закончились, можно, например, перейти на основную игру
            // SceneManager.LoadScene("MainGame"); // Разкомментируйте, если хотите завершить пролог
            return;
        }

        imageDisplay.sprite = prologueImages[currentImageIndex]; // Показываем следующую картинку
    }
}

