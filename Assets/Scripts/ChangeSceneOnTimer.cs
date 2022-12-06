using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class ChangeSceneOnTimer : MonoBehaviour
{
    public int sceneIndex;
    public float changeTime;

    // Update is called once per frame
    void Update()
    {
        changeTime -= Time.deltaTime;
        if (changeTime < 0)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
