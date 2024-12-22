using UnityEngine;

public class PowerUp_SpeedPaddle : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Obtenemos el script Paddle de la pala
        var paddleScript = paddle.GetComponent<Paddle>();

        // Incrementamos su velocidad
        paddleScript.IncreaseSpeed();
        Destroy(this.gameObject);
    }
}
