using System.Linq;
using UnityEngine;

public class PowerUpMultiplyBalls : PowerUp
{
    public override void Execute()
    {
        var ball = GameManagerSingleton.Instance.BallManager.Balls.FirstOrDefault();
        if (ball == null) 
            return;

        Rigidbody2D originalRb = ball.GetComponent<Rigidbody2D>();
        if (originalRb == null) 
            return;

        // Crear bolas adicionales
        int actualBalls = GameManagerSingleton.Instance.BallManager.Balls.Count;
        for (int i = actualBalls; i < 3; i++)
        {
            // Crear bolas adicionales
            GameObject newBall = Object.Instantiate(ball, ball.transform.position, Quaternion.identity);
            Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
            float angle = Random.Range(-30f, 30f); // Variar ángulo en un rango
            newRb.velocity = Quaternion.Euler(0, 0, angle) * ball.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
