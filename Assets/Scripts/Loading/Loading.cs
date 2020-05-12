using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    public Slider sliderLoading;
    public Text processLoading;

    void Start()
    {
        StartCoroutine(LoadingInGame("InGame"));
    }

    IEnumerator LoadingInGame(string ingame)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(ingame);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress/ 0.9f);
            sliderLoading.value = progress;
            processLoading.text = progress * 100f + "%";

            yield return null;
        }
    }

}
