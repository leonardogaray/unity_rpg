using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroController : MonoBehaviour
{
    public Image displayImage;
    private string imagesFolder = "Intro"; // Folder within Resources
    private List<Sprite> images = new List<Sprite>();
    private int currentIndex = 0;
    private float delayTime = 3f;

    public Text uiText;  // The UI Text component
    private string fullText = "Hace mucho tiempo en una galaxia lejana";  // The full text to display
    private float textDelay = 0.1f;
    private string currentText = "";

    void Start()
    {
        LoadImages();
        ShowImage(currentIndex);

        StartCoroutine(ShowText());
    }

    void LoadImages()
    {
        Object[] loadedImages = Resources.LoadAll(imagesFolder, typeof(Sprite));
        foreach (Object img in loadedImages)
        {
            images.Add(img as Sprite);
        }
    }

    void ShowImage(int index)
    {
        if (images.Count > 0 && index >= 0 && index < images.Count)
        {
            displayImage.sprite = images[index];
        }

        if(currentIndex + 1 >= images.Count)
        {
            ChangeScene("Map");
        }
        else
        {
            Invoke("NextImage", delayTime);
        }
    }

    public void NextImage()
    {
        currentIndex = (currentIndex + 1) % images.Count;
        ShowImage(currentIndex);
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(textDelay);
        }
    }

    // Call this method to load a scene by name
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
