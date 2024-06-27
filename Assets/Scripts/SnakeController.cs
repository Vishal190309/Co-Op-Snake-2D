using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float snakeSpeed = 4f;
    [SerializeField] private Vector2 movementDirection = Vector2.left;
    [SerializeField] private Transform snakeBody;
    [SerializeField] private List<Transform> snakeBodys;
    [SerializeField] int ScoreMultipler = 2;
    [SerializeField] PowerUpService powerUpService;
    [SerializeField] UIController uiController;
    [SerializeField] SnakeType snakeType;
    bool bOutOfScreen = false;
    int Score;
    private bool bShieldEnabled;
    private bool bScoreMultiplierEnabled;
    private bool bSpeedMultiplierEnabled;
    float currentShieldTime;
    float currentScoreMultiplierTime;
    float currentSpeedMultiplierTime;


    void Start()
    {
        snakeBodys.Add(transform);
    }

    private void FixedUpdate()
    {
        Debug.Log("moveSnake");
        moveSnake();
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log("update");
        checkIfOutOfBounds();
        checkInput();
        updateScoreBoostTimer();
        updateShieldTimer();
        updateSpeedBoostTimer();

    }

    void updateShieldTimer()
    {
        if (bShieldEnabled)
        {

            currentShieldTime += Time.deltaTime;
            if (currentShieldTime >= Const.shieldPowerupDuration)
            {
                enableShield(false);
            }
        }
    }

    void updateScoreBoostTimer()
    {
        if (bScoreMultiplierEnabled)
        {
            currentScoreMultiplierTime += Time.deltaTime;
            if (currentScoreMultiplierTime >= Const.scoreMultiplierPowerupDuration)
            {
                enableScoreMultiplier(false);
            }
        }
    }

    void updateSpeedBoostTimer()
    {
        if (bSpeedMultiplierEnabled)
        {
            currentSpeedMultiplierTime += Time.deltaTime;
            if (currentSpeedMultiplierTime >= Const.speedBoostPowerupDuration)
            {
                enableSpeedMultiplier(false);
            }
        }
    }

    public bool getShieldEnabled()
    {
        return bShieldEnabled;
    }

    public void enableShield(bool bEnabled)
    {
        currentShieldTime = 0f;
        bShieldEnabled = bEnabled;
        uiController.EnableShield(snakeType, bEnabled);
    }

    public bool getbScoreMultiplierEnabled()
    {
        return bScoreMultiplierEnabled;
    }

    public void enableScoreMultiplier(bool bEnabled)
    {
        currentScoreMultiplierTime = 0f;
        bScoreMultiplierEnabled = bEnabled;
        uiController.EnableScoreMultiplier(snakeType, bEnabled);
    }

    public bool getSpeedMultiplierEnabled()
    {
        return bSpeedMultiplierEnabled;
    }

    public void enableSpeedMultiplier(bool bEnabled)
    {
        currentSpeedMultiplierTime = 0f;
        bSpeedMultiplierEnabled = bEnabled;
        uiController.EnableSpeedMultiplier(snakeType, bEnabled);

        if (bSpeedMultiplierEnabled)
        {
            SetSpeed(Const.increasdSpeed);
            Time.fixedDeltaTime = 0.05f;
        }
        else
        {
            SetSpeed(Const.normalSpeed);
            Time.fixedDeltaTime = 0.08f;
        }
        currentSpeedMultiplierTime = 0f;
    }


    void moveSnake()
    {
        for (int i = snakeBodys.Count - 1; i > 0; i--)
        {
            snakeBodys[i].position = snakeBodys[i - 1].position;
        }

        Vector2 position = transform.position;
        position += movementDirection * snakeSpeed * Time.deltaTime; ;
        transform.position = position;


    }



    void checkIfOutOfBounds()
    {
        Vector2 snakePosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (snakePosition.x < 0f)
        {
            transform.position = new Vector2(Const.wallLeftOffset, gameObject.transform.position.y);
        }
        else if (snakePosition.x > Screen.width)
        {
            transform.position = new Vector2(-Const.wallLeftOffset, gameObject.transform.position.y);
        }
        else if (snakePosition.y < 0f)
        {
            transform.position = new Vector2(gameObject.transform.position.x, Const.wallTopOffset);
        }
        else if (snakePosition.y > Screen.height - 80f)
        {
            transform.position = new Vector2(gameObject.transform.position.x, Const.wallBottomOffset);
        }

    }

   

    void checkInput()
    {
        if(!bOutOfScreen)
        {
            if (Input.GetKeyDown(KeyCode.A) && movementDirection != Vector2.right)
            {
                movementDirection = Vector2.left;
            }
            else if (Input.GetKeyDown(KeyCode.D) && movementDirection != Vector2.left)
            {
                movementDirection = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.W) && movementDirection != Vector2.down)
            {
                movementDirection = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) && movementDirection != Vector2.up)
            {
                movementDirection = Vector2.down;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            uiController.ShowPauseMenu();
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnakeBody")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }



    public void IncreaseScoreAndLength(int newScore, float length)
    {
        if (snakeBodys.Count > 0)
        {
            for (int i =0; i < length; i++)
            {
                Transform bodyOfSnake = Instantiate(snakeBody);
                bodyOfSnake.transform.position = snakeBodys[snakeBodys.Count - 1].position;
                snakeBodys.Add(bodyOfSnake);
            }
          
            if (getbScoreMultiplierEnabled())
            {
                Score += ScoreMultipler * newScore;
                uiController.SetScore(snakeType,Score);
            }
            else
            {
                Score +=  newScore;
                uiController.SetScore(snakeType, Score);
            }
        }


    }

    public void DecreaseScoreAndLength(int newScore, float length)
    {
        if (snakeBodys.Count > 0)
        {
            for (int i = 0; i < length; i++)
            {
                Destroy(snakeBodys[snakeBodys.Count - 1].gameObject);
                snakeBodys.RemoveAt(snakeBodys.Count - 1);
            }
            Score -= newScore;
            uiController.SetScore(snakeType, Score);
        }



    }


    public bool getPositionOccupied(Vector3 position)
    {
        foreach (Transform t in snakeBodys)
        {
            if (Vector2.Distance(t.position, position) < 0.35f)
                return true;
        }
        return false;
    }

    public float getSnakeSize()
    {
        return snakeBodys.Count;
    }

    public void SetSpeed(float speed)
    {
        snakeSpeed = speed;
    }
}


