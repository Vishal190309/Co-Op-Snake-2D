using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenUI : MonoBehaviour
{
    // Start is called before the first frame update

    
    void ShowMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    void Start()
    {
        GetComponent<Animator>().Play("LogoAnimation", -1, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
