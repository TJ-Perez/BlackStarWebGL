using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor.VersionControl;


public class MainMenuController : MonoBehaviour
{
    public GameObject mainMenu;
    [SerializeField] RectTransform fader;
    public GameObject loadingScreen;
    public Slider fillLoadingBar;
    public int loadbarDelay = 1;
    public float loadSceneDelay = 10f;
    private AudioSource audioSource;
    public AudioClip completeSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void LoadPlayScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    // Credits to https://www.youtube.com/watch?v=YMj2qPq9CP8
    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;
        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / .9f);
            yield return new WaitForSecondsRealtime(loadbarDelay);
            fillLoadingBar.value = progressValue;

            if (operation.progress >= .9f)
            {
                yield return new WaitForSeconds(loadSceneDelay);
                operation.allowSceneActivation = true;

            }
        }
    }

    public void LoadNonPlayScene(string sceneName)
    {
        audioSource.PlayOneShot(completeSound);
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0f);
        LeanTween.scale(fader, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() =>
        {
            SceneManager.LoadScene(sceneName);
        });
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
