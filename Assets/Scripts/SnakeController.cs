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
    public GameplayUI gameplayUI;
    bool bOutOfScreen = false;
    int Score;


    void Start()
    {
        snakeBodys.Add(transform);
    }

    private void FixedUpdate()
    {
        moveSnake();
    }

    // Update is called once per frame
    void Update()
    {


        checkIfOutOfBounds();
        checkInput();

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
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
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
          
            if (powerUpService.getbScoreMultiplierEnabled())
            {
                Score += ScoreMultipler * newScore;
                gameplayUI.SetScore(Score);
            }
            else
            {
                Score +=  newScore;
                gameplayUI.SetScore(Score);
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
            gameplayUI.SetScore(Score);
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


