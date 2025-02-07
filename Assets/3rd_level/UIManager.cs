using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject blurBackground;
    public GameObject instrumentPreview;

    public void CloseUI()
    {
        blurBackground.SetActive(false);
        instrumentPreview.SetActive(false);
    }
}
