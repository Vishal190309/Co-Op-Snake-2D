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
    [SerializeField] PowerUpService powerUpController;
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
        GameObject controller = Instantiate(foodControllers[UnityEngine.Random.Range(0,foodControllers.Length)],transform);
        controller.transform.position = getRandomPosition();
        controller.SetActive(true);
        foodSpawned.Add(controller.transform);
    }

    public void DestroyFood(GameObject gameObject)
    {
        foodSpawned.Remove(gameObject.transform);
        Destroy(gameObject);
    }


    public bool getPositionOccupied(Vector3 position)
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
        } while (snakeController.getPositionOccupied(position)|| getPositionOccupied(position) || powerUpController.getPositionOccupied(position ));
        
        return position;
    }

   
}

[Serializable]
public class Food
{
    public FoodType type;
    public int length;
    public int score;

}
public enum FoodType
{
    MASS_GAINER,
    MASS_BURNER
}
