using System.Collections;
using UnityEngine;

public class PowerUp_EnlargePaddle : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Buscamos el script Paddle
        var paddleScript = paddle.GetComponent<Paddle>();

        Transform paddleTransform = paddle.transform;

        // Solo cambiamos el tamaño si no lo ha cambiado ya
        if (paddleScript.OriginalScale.x == paddle.transform.localScale.x)
        {
            paddleTransform.localScale = new Vector3(paddleScript.OriginalScale.x * 1.5f, paddleScript.OriginalScale.y, paddleScript.OriginalScale.z);
            paddleScript.CalculatePaddleLimits();
        }

        Destroy(this.gameObject);
    }
}
