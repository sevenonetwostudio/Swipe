using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool gameStarted;

    public GameObject platformSpawner;
    public GameObject gamePlayUI;
    public GameObject menuUI;
    public GameObject gameOverUI;

    public Text scoreText;
    public Text highScoreText;

    AudioSource audioSource;
    public AudioClip[] gameMusic;

    int score = 0;
    int highScore;

    int adCounter = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");

        highScoreText.text = "Best Score : " + highScore;

        CheckAdCount();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameStarted)
        {
            if(Input.GetMouseButtonDown(0))
            {
                GameStart();
            }
        }
    }

    public void GameStart()
    {
        gameStarted = true;
        platformSpawner.SetActive(true);

        gamePlayUI.SetActive(true);
        menuUI.SetActive(false);

        //Play Audio
        audioSource.clip = gameMusic[1];
        audioSource.Play();


        StartCoroutine("UpdateScore");

    }

    public void GameOver()
    {
        platformSpawner.SetActive(false);
        gameOverUI.SetActive(true);
        menuUI.SetActive(false);
        StopCoroutine("UpdateScore");
        SaveHighScore();

        //show ad
        //AdsManager.instance.ShowAd();

        //AdsManager.instance.ShowRewardedAd();

        if (adCounter >= 4)
        {
            adCounter = 0;
            PlayerPrefs.SetInt("AdCount", adCounter);

            AdsManager.instance.ShowRewardedAd();
        }
        else
        {
            Invoke("ReloadLevel", 1f);
        }

        
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene("Game");
    }

    IEnumerator UpdateScore()
    {
        while(true)
        {
            //yield return new WaitForSeconds(1f);

            score++;

            scoreText.text = score.ToString();
        }
    }

    public void IncrementScore()
    {
        score += 2;

        scoreText.text = score.ToString();

        audioSource.PlayOneShot(gameMusic[5], 0.2f);

    }

    void SaveHighScore()
    {
        if(PlayerPrefs.HasKey("HighScore"))
        {
            // we alreadt have a highscore

            if(score > PlayerPrefs.GetInt("HighScore"))
            {
                PlayerPrefs.SetInt("HighScore", score);
            }
        }
        else
        {
            //playing for the first time
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void CheckAdCount()
    {
        if(PlayerPrefs.HasKey("AdCount"))
        {
            adCounter = PlayerPrefs.GetInt("AdCount");
            adCounter++;

            PlayerPrefs.SetInt("AdCount", adCounter);
        }
        else
        {
            PlayerPrefs.SetInt("AdCount", 0);
        }
    }
}
