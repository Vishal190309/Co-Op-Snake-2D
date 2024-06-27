using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PauseUI;
    public GameObject GameOverUI;

    //Player1Gameplay

    [SerializeField] Image player1ShieldImage;
    [SerializeField] Image player1ScoreMultiplierImage;
    [SerializeField] Image player1SpeedImage;
    [SerializeField] Text player1ScoreText;

    //Player2Gameplay

    [SerializeField] Image player2ShieldImage;
    [SerializeField] Image player2ScoreMultiplierImage;
    [SerializeField] Image plauyer2SpeedImage;
    [SerializeField] Text player2ScoreText;

    bool bIsGamePaused =false ;
    void Start()
    {
        
    }

    public void EnableShield(SnakeType snakeType,bool bEnable)
    {
        switch (snakeType)
        {
            case SnakeType.SNAKE1 :
                EnalbleDisableImage(player1ShieldImage, bEnable);
                break;
            case SnakeType.SNAKE2 :
                EnalbleDisableImage(player2ShieldImage, bEnable);
                break;
        }
      
    }

    public void EnableScoreMultiplier(SnakeType snakeType, bool bEnable)
    {
        switch (snakeType)
        {
            case SnakeType.SNAKE1:
                EnalbleDisableImage(player1ScoreMultiplierImage, bEnable);
                break;
            case SnakeType.SNAKE2:
                EnalbleDisableImage(player2ScoreMultiplierImage, bEnable);
                break;
        }
    }

    public void EnableSpeedMultiplier(SnakeType snakeType, bool bEnable)
    {
        switch (snakeType)
        {
            case SnakeType.SNAKE1:
                EnalbleDisableImage(player1SpeedImage, bEnable);
                break;
            case SnakeType.SNAKE2:
                EnalbleDisableImage(player1SpeedImage, bEnable);
                break;
        }
    }

    void EnalbleDisableImage(Image image, bool bEnable)
    {
        if (bEnable)
        {
            Color color = image.color;
            color.a = Const.enabledAlpha;
            image.color = color;
            image.transform.localScale = Const.enabledScale;
        }
        else
        {
            Color color = image.color;
            color.a = Const.disabledAlpha;
            image.color = color;
            image.transform.localScale = Const.disabledScale;
        }
    }

    public void SetScore(SnakeType snakeType,int score)
    {
        switch (snakeType)
        {
            case SnakeType.SNAKE1:
                player1ScoreText.text = "Score: " + score;
                break;
            case SnakeType.SNAKE2:
                player2ScoreText.text = "Score: " + score;
                break;
        }
    }

    public void ShowPauseMenu()
    {
        if (bIsGamePaused)
        {
            PauseUI.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            PauseUI.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ShowGameOverUI()
    {
        GameOverUI.SetActive(true);
        if (!GameManager.Instance.isTwoPlayer)
        {
            //PlayerPrefs.SetFloat(Const.highScore,);
        }
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
