using System.Collections;
using System.Collections.Generic;
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
    void OnSinglePlayerButtonClick()
    {
        GameManager.Instance.isTwoPlayer = false;
        SceneManager.LoadScene(2);
    }

    void OnCoOpPlayerButtonClick()
    {
        GameManager.Instance.isTwoPlayer = true;
        SceneManager.LoadScene(3);
    }

    void OnQuitButtonClick()
    {
        Application.Quit();
    }
    void Start()
    {
        singlePlayerButton.onClick.AddListener(OnSinglePlayerButtonClick);
        coOpButton.onClick.AddListener(OnCoOpPlayerButtonClick);
        quitButton.onClick.AddListener(OnQuitButtonClick);
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
