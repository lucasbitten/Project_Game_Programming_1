using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject mainMenu;

    public GameObject loadingScreen;
    public Slider slider;
    public void SettingsButton(){

        mainMenu.SetActive(false);
        settings.SetActive(true);

    }

    public void BackButton(){

        settings.SetActive(false);
        mainMenu.SetActive(true);

    }


    public void LoadLevel(int sceneIndex){

        StartCoroutine(LoadAsynchronously(sceneIndex));

    }

    IEnumerator LoadAsynchronously (int sceneIndex){

        AsyncOperation opetarion = SceneManager.LoadSceneAsync(sceneIndex);
       
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        while (!opetarion.isDone){
            float progress = Mathf.Clamp01(opetarion.progress/0.9f);
            slider.value = progress;
            yield return null;

        }

    }

}
