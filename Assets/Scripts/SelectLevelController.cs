using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelController : MonoBehaviour
{
    public Sprite levelUnlocked;
    public Sprite levelLocked;
    
    public void SelectLevel(int sceneLevel)
    {
        SceneManager.LoadScene(sceneLevel);
    }
}
