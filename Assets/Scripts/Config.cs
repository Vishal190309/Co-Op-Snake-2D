

using UnityEngine;

public class Const
{
    public const float wallLeftOffset = 8.7f;
    public const float wallTopOffset = 4.085f;
    public const float wallBottomOffset = -4.82f;
    public const float disabledAlpha = 100f;
    public const float enabledAlpha = 255f;
    public const float normalSpeed = 0.35f;
    public const float normalUpdateTime = 0.08f;
    public const float fastUpdateTime = 0.05f;
    public const float shieldPowerupDuration = 5f;
    public const float scoreMultiplierPowerupDuration = 7f;
    public const float speedBoostPowerupDuration = 10f;
    public const string highScore = "HighScore";
    public static Vector3 disabledScale = new Vector3(1f,1f,1f);
    public static Vector3 enabledScale = new Vector3(1.25f, 1.25f, 1.25f);
}

public enum SnakeType
{
   SNAKE1,
   SNAKE2
}
