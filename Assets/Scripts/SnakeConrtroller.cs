using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeConrtroller : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed =10f;
    [SerializeField] private Vector2 movementDirection = Vector2.left;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkInput();
        moveSnake();
        checkIfOutOfBounds();
    }

    void moveSnake()
    {
        Vector2 position = transform.position;
        position += movementDirection * speed * Time.deltaTime;
        transform.position = position;

    }

    void checkIfOutOfBounds()
    {
        Vector2 snakePosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);

        if(snakePosition.x<0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, snakePosition.y));
        }
        else if (snakePosition.x > Screen.width)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector2(0f, snakePosition.y));
        }
        else if (snakePosition.y < 0)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector2(snakePosition.x, Screen.height));
        }
        else if (snakePosition.y > Screen.height)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector2(snakePosition.x, 0f));
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
}


