using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameplayUI : MonoBehaviour
{

    [SerializeField] Image ShieldImage;
    [SerializeField] Image ScoreMultiplierImage;
    [SerializeField] Image SpeedImage;
    [SerializeField] TextMeshProUGUI ScoreText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void EnableShield(bool bEnable)
    {
        EnalbleDisableImage(ShieldImage, bEnable);
    }

    public void EnableScoreMultiplier(bool bEnable)
    {
        EnalbleDisableImage(ScoreMultiplierImage, bEnable);
    }

    public void EnableSpeedMultiplier(bool bEnable)
    {
        EnalbleDisableImage(SpeedImage, bEnable);
    }

    void EnalbleDisableImage(Image image,bool bEnable)
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

    public void SetScore(int score)
    {
        ScoreText.SetText("Score: " + score);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
