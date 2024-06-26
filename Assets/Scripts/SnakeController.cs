using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float snakeSpeed = 4f;
    [SerializeField] private Vector2 movementDirection = Vector2.left;
    [SerializeField] private Vector2 distanceTravled;
    [SerializeField] private Transform snakeBody;
    [SerializeField] private List<Transform> snakeBodys;
    [SerializeField] int ScoreMultipler = 2;
    [SerializeField] PowerUpService powerUpService;
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

        checkInput();
        checkIfOutOfBounds();

    }



    void moveSnake()
    {
        for (int i = snakeBodys.Count - 1; i > 0; i--)
        {
            snakeBodys[i].position = snakeBodys[i - 1].position;
        }

        Vector2 position = transform.position;
        distanceTravled = movementDirection * snakeSpeed * Time.deltaTime;
        position += distanceTravled;
        transform.position = position;


    }



    void checkIfOutOfBounds()
    {
        Vector2 snakePosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if (snakePosition.x < 0)
        {
            distanceTravled = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, snakePosition.y));
            transform.position = distanceTravled;
        }
        else if (snakePosition.x > Screen.width)
        {
            distanceTravled = Camera.main.ScreenToWorldPoint(new Vector2(0f, snakePosition.y));
            transform.position = distanceTravled;
        }
        else if (snakePosition.y < 0)
        {
            distanceTravled = Camera.main.ScreenToWorldPoint(new Vector2(snakePosition.x, Screen.height));
            transform.position = distanceTravled;
        }
        else if (snakePosition.y > Screen.height)
        {
            distanceTravled = Camera.main.ScreenToWorldPoint(new Vector2(snakePosition.x, 0f));
            transform.position = distanceTravled;
        }
    }

    void checkInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            movementDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            movementDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            movementDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            movementDirection = Vector2.down;
        }
    }



    public void IncreaseScoreAndLength(int newScore, float length)
    {
        if (snakeBodys.Count > 0)
        {
            Transform bodyOfSnake = Instantiate(snakeBody);
            bodyOfSnake.transform.position = snakeBodys[snakeBodys.Count - 1].position;
            snakeBodys.Add(bodyOfSnake);
            if (powerUpService.getbScoreMultiplierEnabled())
            {
                Score += ScoreMultipler * newScore;
            }
        }


    }

    public void DecreaseScoreAndLength(int newScore, float length)
    {
        if (snakeBodys.Count > 0)
        {
            Destroy(snakeBodys[snakeBodys.Count - 1].gameObject);
            snakeBodys.RemoveAt(snakeBodys.Count - 1);
            Score -= newScore;
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


