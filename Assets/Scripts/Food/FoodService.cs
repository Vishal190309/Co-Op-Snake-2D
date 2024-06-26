using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodService : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject[] foodControllers;
    [SerializeField] float spawnInterval = 3f;
    float elapsedTime;
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime > spawnInterval)
        {
            SpawnFood();
            elapsedTime = 0;
        }
    }
    void SpawnFood()
    {
        switch(UnityEngine.Random.Range(0, foodControllers.Length))
        {
            case 0:
                SpawFoodObject(0);
                break;
            case 1:
                SpawFoodObject(1);
                break;

        }
    }

    private GameObject SpawFoodObject(int foodNo)
    {
        GameObject controller = Instantiate(foodControllers[foodNo], transform);
        return controller;
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
