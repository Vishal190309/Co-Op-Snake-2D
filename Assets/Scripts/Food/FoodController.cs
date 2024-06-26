using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Food foodType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    public Food getFoodType()
    {
        return foodType;
    }

    public void SetFoodType(Food type)
    {
        foodType = type;
    }


    private void OnCollisionEnter(Collision collision)
    {
       /* SnakeController snakeController = collision.gameObject.GetComponent<SnakeController>()
        if (snakeController != null)
        {
            snakeController.
        }*/
    }
}

