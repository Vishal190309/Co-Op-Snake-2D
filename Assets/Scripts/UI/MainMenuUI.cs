using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Button singlePlayerButton;
    [SerializeField] Button coOpButton;
    [SerializeField] Button quitButton;
    [SerializeField] Text highScore;
    void OnSinglePlayerButtonClick()
    {
        GameManager.Instance.PlaySoundEffect(GameManager.AudioType.BUTTON_CLICK);
        GameManager.Instance.isTwoPlayer = false;
        SceneManager.LoadScene(2);
    }

    void OnCoOpPlayerButtonClick()
    {
        GameManager.Instance.PlaySoundEffect(GameManager.AudioType.BUTTON_CLICK);
        GameManager.Instance.isTwoPlayer = true;
        SceneManager.LoadScene(3);
    }

    void OnQuitButtonClick()
    {
        GameManager.Instance.PlayAudio(GameManager.AudioType.BUTTON_CLICK);
        Application.Quit();
    }
    void Start()
    {
        singlePlayerButton.onClick.AddListener(OnSinglePlayerButtonClick);
        coOpButton.onClick.AddListener(OnCoOpPlayerButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
        highScore.text = "High Score: " + PlayerPrefs.GetFloat(Const.highScore).ToString();
    }

    private void Awake()
    {
        gameObject.SetActive(true);
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }
}
