using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Slider slider;
    public int sceneIndex;
    private void Start()
    {
        LoadLevel();
    }
    public void LoadLevel ()
    {
        StartCoroutine(LoadAsynchronously());
    }

    IEnumerator LoadAsynchronously ()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;
            yield return null;
        }
    }
}
