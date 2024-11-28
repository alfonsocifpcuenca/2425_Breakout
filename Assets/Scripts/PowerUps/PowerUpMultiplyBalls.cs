using UnityEngine;

public class PowerUpMultiplyBalls : PowerUp
{
    public override void Execute()
    {
        var ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball == null) 
            return;

        Rigidbody2D originalRb = ball.GetComponent<Rigidbody2D>();
        if (originalRb == null) 
            return;

        // Crear bolas adicionales
        for (int i = GameManagerSingleton.Instance.NumbersOfBalls; i < 3; i++)
        {
            if (GameManagerSingleton.Instance.NumbersOfBalls > 2)
                return;

            GameManagerSingleton.Instance.AddBall();

            GameObject newBall = Object.Instantiate(ball, ball.transform.position, Quaternion.identity);

            // Aplicar una velocidad ligeramente diferente
            Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
            if (newRb != null)
            {
                float angle = Random.Range(-30f, 30f); // Variar ángulo en un rango
                newRb.velocity = Quaternion.Euler(0, 0, angle) * originalRb.velocity;
            }
        }
    }
}
