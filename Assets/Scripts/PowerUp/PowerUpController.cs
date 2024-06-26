using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    [SerializeField] PowerupType powerupType;
    [SerializeField] float powerupDuration = 7f;
    [SerializeField] float despawnTime = 10f;
    PowerUpService powerupService;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        powerupService = transform.parent.gameObject.GetComponent<PowerUpService>();
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= despawnTime)
        {
            powerupService.DestroyPowerup(gameObject);
            elapsedTime = 0;
        }
    }

    public PowerupType getPowerupType()
    {
        return powerupType;
    }
    public float getPowerupDuration()
    {
        return powerupDuration;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SnakeHead")
        {
            switch (powerupType)
            {
                case PowerupType.SHIELD:
                    powerupService.enableShield(true);
                    powerupService.DestroyPowerup(gameObject);
                    break;
                case PowerupType.SCORE_BOOST:
                    powerupService.enableScoreMultiplier(true);
                    powerupService.DestroyPowerup(gameObject);
                    break;
                case PowerupType.SPEED_UP:
                    powerupService.enableSpeedMultiplier(true);
                    powerupService.DestroyPowerup(gameObject);
                    break;
            }

        }
    }

    
}