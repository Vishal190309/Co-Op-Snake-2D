using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Food foodType;
    [SerializeField] float despawnTime = 5f;
    float elapsedTime = 0f;
    FoodService foodService;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SnakeHead")
        {
            switch (foodType.type)
            {
                case FoodType.MASS_GAINER:
                    collision.gameObject.GetComponent<SnakeController>().IncreaseLength(foodType.length);
                    foodService.DestroyFood(gameObject);
                    break;
                case FoodType.MASS_BURNER:
                    collision.gameObject.GetComponent<SnakeController>().DecreaseLength(foodType.length);
                    foodService.DestroyFood(gameObject);
                    break;
            }
           
        }
    }
    private void Start()
    {
       foodService =  transform.parent.gameObject.GetComponent<FoodService>();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= despawnTime)
        {
            elapsedTime = 0f;
            foodService.DestroyFood(gameObject);
            
        }
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

