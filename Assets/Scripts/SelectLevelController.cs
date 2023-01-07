using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectLevelController : MonoBehaviour
{
    public Sprite levelUnlocked;

    public GameObject[] levels = new GameObject[0];
    public Button[] btnLevels = new Button[0];

    private string filePath = "./Score.txt";
    private string[] scores;

    private void Start()
    {
        ReadToFile(filePath, out scores);
        for (int i = 0; i < scores.Length; i++)
        {
            int score = int.Parse(scores[i]);
            if (score != -1)
            {
                btnLevels[i].image.sprite = levelUnlocked;
                for (int j = 1; j <= 3; j++)
                {
                    if (j <= score)
                        levels[i].transform.GetChild(j).gameObject.SetActive(true);
                }
            }
            if (score == -1 && (i == 0 || int.Parse(scores[i - 1]) > 0))
            {
                btnLevels[i].image.sprite = levelUnlocked;
                break;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SelectLevel(int sceneLevel)
    {
        if (sceneLevel == 3 || int.Parse(scores[sceneLevel - 4]) > 0)
            SceneManager.LoadScene(sceneLevel);
    }

    void ReadToFile(string path, out string[] scores)
    {
        scores = System.IO.File.ReadAllLines(path);
    }
}
