using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoaderWithLogo : MonoBehaviour
{

    public Image loadingImage;
    public Image loadingImageBW;
    public float currentLoad;
    public float waitTime;
    public int loadIndex;

    public AudioSource startEffect;

    AsyncOperation operation;
    bool loadingScene = false;
    bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {

        loadingImageBW.enabled = false;

        loadingImage.fillAmount = 0;

        StartCoroutine(Wait(waitTime));

    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        loadingImageBW.enabled = true;
        startEffect.Play();
        StartCoroutine(LoadAsynchronously(loadIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        yield return new WaitForSecondsRealtime(1.0f);
        operation = SceneManager.LoadSceneAsync(sceneIndex);
        operation.allowSceneActivation = false;
        loadingScene = true;


        while (!operation.isDone && currentLoad < 1)
        {
            currentLoad = Mathf.Clamp01(operation.progress / 0.9f);

            loadingImage.fillAmount = currentLoad;
            Debug.Log(currentLoad);

            if(currentLoad >= 0.99f)
            {
                StartCoroutine(LoadActivation(waitTime));
            }


            yield return null;
        }
    }

    IEnumerator LoadActivation(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        operation.allowSceneActivation = true;
    }


}
