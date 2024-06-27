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
    [SerializeField] Text winText;
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
       
        PauseUI.SetActive(true);
        GameManager.Instance.PauseAudio(GameManager.AudioType.BACKGROUN_MUSIC);
        Time.timeScale = 0f;
    }

    public void ShowGameOverUI(bool bHeadCollision , SnakeType snakeType)
    {
        GameOverUI.SetActive(true);
        GameManager.Instance.PauseAudio(GameManager.AudioType.BACKGROUN_MUSIC);
        Time.timeScale = 0f;
        Debug.Log((player1ScoreText.text.Split(" ")[1]));
        if (!GameManager.Instance.isTwoPlayer)
        {
            if (float.Parse(player1ScoreText.text.Split(" ")[1]) > PlayerPrefs.GetFloat(Const.highScore))
            {
                PlayerPrefs.SetFloat(Const.highScore, float.Parse(player1ScoreText.text.Split(" ")[1]));
            }
            winText.text = "You Score : " + player1ScoreText.text.Split(" ")[1];
        }
        else if(bHeadCollision)
        {
            winText.text = (float.Parse(player1ScoreText.text.Split(" ")[1]) > float.Parse(player2ScoreText.text.Split(" ")[1])) ? "GREEN HAS HIGHER SCORE THAN BLUE. GREEN WINS !" : "YELLOW HAS HIGHER SCORE THAN GREEN. BLUE WINS !";
            if (float.Parse(player1ScoreText.text.Split(" ")[1]) == float.Parse(player2ScoreText.text.Split(" ")[1]))
                winText.text = "GREEN AND BLUE HAVE SAME SCORE. IT'S A DRAW !";
        }
        else
        {
            winText.text = snakeType == SnakeType.SNAKE2 ? "YELLOW COLLIDED! GREEN WON" : "GREEN COLLIDED! YELLOW WON";
        }
       
    }

    public void Resume()
    {
        GameManager.Instance.PlaySoundEffect(GameManager.AudioType.BUTTON_CLICK);
        PauseUI.SetActive(false);
        GameManager.Instance.PlayAudio(GameManager.AudioType.BACKGROUN_MUSIC);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        GameManager.Instance.PlaySoundEffect(GameManager.AudioType.BUTTON_CLICK);
        Time.timeScale = 1f;
        GameManager.Instance.PlayAudio(GameManager.AudioType.BACKGROUN_MUSIC);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        GameManager.Instance.PlaySoundEffect(GameManager.AudioType.BUTTON_CLICK);
        Time.timeScale = 1f;
        GameManager.Instance.PlayAudio(GameManager.AudioType.BACKGROUN_MUSIC);
        SceneManager.LoadScene(1);
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
