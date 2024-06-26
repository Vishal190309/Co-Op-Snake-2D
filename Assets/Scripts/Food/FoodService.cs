using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodService : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] foodControllers;
    [SerializeField] Vector2 spawnIntervalRange;
    float currentSpawnInterval =1f;
    [SerializeField] SnakeController snakeController;
    List<Transform> foodSpawned = new List<Transform>();
    float elapsedTime;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > currentSpawnInterval)
        {
            SpawnFood();
            elapsedTime = 0;
            currentSpawnInterval = UnityEngine.Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
        }
    }
    void SpawnFood()
    {
        if (snakeController.getSnakeSize() > 1)
        {
            switch (UnityEngine.Random.Range(0, foodControllers.Length))
            {
                case 0:
                    SpawFoodObject(0);
                    break;
                case 1:
                    SpawFoodObject(1);
                    break;

            }
        }
        else
        {
            SpawFoodObject(0);
        }
    }

    public void DestroyFood(GameObject gameObject)
    {
        foodSpawned.Remove(gameObject.transform);
        Destroy(gameObject);
    }

    private void SpawFoodObject(int foodNo)
    {
        GameObject controller = Instantiate(foodControllers[foodNo], transform);
        controller.transform.position = getRandomPosition();
        foodSpawned.Add(controller.transform);
    }

    private bool getPositionOccupiedByFood(Vector3 position)
    {
        foreach (Transform t in foodSpawned)
        {
            if (Vector2.Distance(t.position, position) < 0.35f)
                return true;
        }
        return false;
    }

    Vector2 getRandomPosition()
    {
        Vector2 position;
        do
        {
            float xPosition = UnityEngine.Random.Range(50, Screen.width - 50);
            float yPosition = UnityEngine.Random.Range(50, Screen.height - 50);
            position = Camera.main.ScreenToWorldPoint(new Vector2(xPosition, yPosition));
        } while (snakeController.getPositionOccupied(position)||getPositionOccupiedByFood (position) );
        
        return position;
    }

   
}

[Serializable]
public class Food
{
    public FoodType type;
    public int length;

}
public enum FoodType
{
    MASS_GAINER,
    MASS_BURNER
}
