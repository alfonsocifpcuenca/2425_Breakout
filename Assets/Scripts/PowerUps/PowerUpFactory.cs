using UnityEngine;

public static class PowerUpFactory
{
    public static PowerUp CreatePowerUp()
    {
        int random = Random.Range(0, 10);

        if (random < 1)
        {
            return new PowerUpMultiplyBalls();
        }
        else if (random < 3)
        {
            return new PowerUpPaddleSize();
        }
        else if (random < 4)
        {
            return new PowerUpAddLive();
        }
        return null; 
    }
}