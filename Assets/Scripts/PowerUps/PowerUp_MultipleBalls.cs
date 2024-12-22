using System.Linq;
using UnityEngine;

public class PowerUp_MultipleBalls : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Obtenemos una bola del Manager
        var ball = GameManagerSingleton.Instance.BallManager.Balls.FirstOrDefault();
        if (ball != null)
        {
            // Vamos a clonar la bola 2 veces más
            for (int i = 0; i < 2; i++)
            {
                GameObject newBall = Object.Instantiate(ball, ball.transform.position, Quaternion.identity);
                Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
                
                // Calculamos un angulo aleatorio entre -30 y 30
                float angle = Random.Range(-30f, 30f);

                // Aplicamos a la bola un giro con el ángulo calculado
                newRb.velocity = Quaternion.Euler(0, 0, angle) * ball.GetComponent<Rigidbody2D>().velocity;
            }
        }
        Destroy(this.gameObject);
    }
}
