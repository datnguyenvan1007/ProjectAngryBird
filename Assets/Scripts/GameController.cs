using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    bool isPause = false;
    private float startScore;
    private float totalScore;
    private int totalStar;

    public Button btnPause;
    public Sprite spritePause;
    public Sprite spriteContinue;
    public Text txtEnemyAlive;
    public Text txtScore;
    public Text txtEndScore;

    public GameObject sceneRunning;
    public GameObject sceneEnd;
    public GameObject sceneFailed;
    public GameObject endLevel;
    public GameObject[] stars;
    public GameObject[] numberOfPlays;
    private int totalPlays;

    private AudioSource audioSource;
    public AudioClip backgroundClip;
    public AudioClip storyClip;
    public AudioClip birdLauchClip;
    public AudioClip dragBirdClip;
    public AudioClip woodCollisionClip;
    public AudioClip birdCollideEnemyClip;
    public AudioClip[] starClip;
    public AudioClip destroyWoodClip;
    public AudioClip birdDestroyClip;
    public AudioClip levelFailedClip;
    public AudioClip birdExplosionClip;

    public int totalEnemy;

    public int enemyDead { get; set; }

    public int sceneNextIndex;
    public int sceneCurrentIndex;

    private string filePath = "./Score.txt";
    private string[] scores;


    private void Start()
    {
        totalPlays = numberOfPlays.Length;
        audioSource = gameObject.GetComponent<AudioSource>();
        startScore = 0f;
        enemyDead = 0;
        totalStar = 0;
        txtEnemyAlive.text = enemyDead.ToString() + "/" + totalEnemy.ToString();
    }

    private void Update()
    {
        if (GameObject.Find("bird(Clone)") == null && GameObject.Find("Bomb(Clone)") == null && GameObject.Find("big_brother(Clone)") == null)
            DisplayEndLevel();
        if (startScore < totalScore)
        {
            startScore += 50f;
            txtScore.text = (startScore).ToString("#,###");
        }
        if (enemyDead >= totalEnemy)
            DisplayEndLevel();
    }

    public void DisplaySoundShoot()
    {
        audioSource.PlayOneShot(birdLauchClip);
    }

    public void DisplaySoundDragBird()
    {
        audioSource.PlayOneShot(dragBirdClip);
    }

    public void DisplaySoundBirdCollideEnemy()
    {
        audioSource.PlayOneShot(birdCollideEnemyClip);
    }

    public void DisplaySoundWoodCollision()
    {
        audioSource.PlayOneShot(woodCollisionClip);
    }

    public void DisplaySoundDestroyWood()
    {
        audioSource.PlayOneShot(destroyWoodClip);
    }

    public void DisplaySoundBirdDestroy()
    {
        audioSource.PlayOneShot(birdDestroyClip, 1);
    }

    public void DisplaySoundLevelFailed()
    {
        audioSource.clip = levelFailedClip;
        audioSource.Play();
    }

    public void DisplaySoundBirdExplosion()
    {
        audioSource.PlayOneShot(birdExplosionClip);
    }

    public void PauseGame()
    {
        if (isPause)
        {
            Time.timeScale = 1;
            isPause = false;
            btnPause.image.sprite = spritePause;
            audioSource.Play();
        }
        else
        {
            Time.timeScale = 0;
            btnPause.image.sprite = spriteContinue;
            isPause = true;
            audioSource.Pause();
        }
    }

    public void DisplayEndLevel()
    {
        endLevel.SetActive(true);
    }

    public void EndLevel()
    {
        numberOfPlays[0].SetActive(false);
        if (enemyDead >= totalEnemy)
            DisplaySceneScore();
        else
            DisplaySceneFailed();
    }

    public void DisplaySceneScore()
    {
        sceneEnd.SetActive(true);
        sceneRunning.SetActive(false);
        audioSource.clip = storyClip;
        audioSource.Play();
        StartCoroutine(DisplayStar());
        if (totalScore > 3000)
        {
            totalStar = 1;
        }
        if (totalScore > (totalEnemy / 2) * 3000)
        {
            totalStar = 2;
        }
        if (totalScore >= totalEnemy * 3000)
        {
            totalStar = 3;
        }
        ReadToFile(filePath, out scores);
        scores[sceneCurrentIndex - 3] = totalStar.ToString();
        WriteToFile(filePath, scores);
    }

    public void DisplaySceneFailed()
    {
        sceneRunning.SetActive(false);
        sceneFailed.SetActive(true);
        DisplaySoundLevelFailed();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(sceneCurrentIndex);
    }

    public void DisplayEnemyAlive()
    {
        txtEnemyAlive.text = enemyDead.ToString() + "/" + totalEnemy.ToString();
    }

    public void DisplayScore()
    {
        totalScore = enemyDead * 3000;
        txtEndScore.text = (totalScore).ToString("#,###");
    }

    private IEnumerator DisplayStar()
    {
        yield return new WaitForSeconds(1);
        if (totalScore > 3000)
        {
            stars[0].SetActive(true);
            audioSource.PlayOneShot(starClip[0]);
        }
        yield return new WaitForSeconds(1);
        if (totalScore > (totalEnemy / 2) * 3000)
        {
            stars[1].SetActive(true);
            audioSource.PlayOneShot(starClip[1]);
        }
        yield return new WaitForSeconds(1);
        if (totalScore >= totalEnemy * 3000)
        {
            stars[2].SetActive(true);
            audioSource.PlayOneShot(starClip[2]);
        }
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(sceneNextIndex);
    }

    public void DisplaySceneSelectLevel()
    {
        SceneManager.LoadScene(2);
    }

    public void ReduceNumberOfPlays()
    {
        if (totalPlays > 0)
        {
            --totalPlays;
            numberOfPlays[totalPlays].SetActive(false);
        }
    }

    void WriteToFile(string path, string[] scores)
    {
        System.IO.File.WriteAllLines(path, scores);
    }

    void ReadToFile(string path, out string[] scores)
    {
        scores = System.IO.File.ReadAllLines(path);
    }
}
