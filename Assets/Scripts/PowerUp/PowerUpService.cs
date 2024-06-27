using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpService : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] SnakeController snakeController;
    [SerializeField] FoodService foodControlller;
    [SerializeField] PowerUpController[] powerUpControllers;
    [SerializeField] List<Transform> spawnedPowerup;
    [SerializeField] Vector2 powerUpSpawnTimeRange;
   

    float currentSpawnPowerupTime;
    float elapsedTime;
    private void Start()
    {
        currentSpawnPowerupTime = UnityEngine.Random.Range(powerUpSpawnTimeRange.x, powerUpSpawnTimeRange.y);
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= currentSpawnPowerupTime) 
        { 
            SpawnPowerup();
            currentSpawnPowerupTime = UnityEngine.Random.Range(powerUpSpawnTimeRange.x, powerUpSpawnTimeRange.y);
            elapsedTime = 0;
        }

      


    }

  

    

    void SpawnPowerup()
    {
        GameObject controller = Instantiate(powerUpControllers[UnityEngine.Random.Range(0, powerUpControllers.Length)].gameObject,transform);
        controller.transform.position = getRandomPosition();
        controller.SetActive(true);
        spawnedPowerup.Add(controller.transform);
    }

    public void DestroyPowerup(GameObject powerup)
    {
        spawnedPowerup.Remove(powerup.transform);
        Destroy(powerup);
    }

    Vector2 getRandomPosition()
    {
        Vector2 position;
        do
        {
            float xPosition = UnityEngine.Random.Range(50, Screen.width - 50);
            float yPosition = UnityEngine.Random.Range(50, Screen.height - 130);
            position = Camera.main.ScreenToWorldPoint(new Vector2(xPosition, yPosition));
        } while (snakeController.getPositionOccupied(position) || getPositionOccupied(position) || foodControlller.getPositionOccupied(position));
        return position;
    }

    public bool getPositionOccupied(Vector3 position)
    {
        foreach (Transform t in spawnedPowerup)
        {
            if (Vector2.Distance(t.position, position) < 0.35f)
                return true;
        }
        return false;
    }


}

public enum PowerupType
{
    SHIELD,
    SCORE_BOOST,
    SPEED_UP
}