using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Food food;
    [SerializeField] float despawnTime = 5f;
    float elapsedTime = 0f;
    FoodService foodService;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "SnakeHead")
        {
            switch (food.type)
            {
                case FoodType.MASS_GAINER:
                    GameManager.Instance.PlaySoundEffect(GameManager.AudioType.FOOD_PICKUP);
                    collision.gameObject.GetComponent<SnakeController>().IncreaseScoreAndLength(food.score,food.length);
                    foodService.DestroyFood(gameObject);
                    break;
                case FoodType.MASS_BURNER:
                    GameManager.Instance.PlaySoundEffect(GameManager.AudioType.FOOD_PICKUP);
                    collision.gameObject.GetComponent<SnakeController>().DecreaseScoreAndLength(food.score, food.length);
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
        return food;
    }

    public void SetFoodType(Food type)
    {
        food = type;
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

