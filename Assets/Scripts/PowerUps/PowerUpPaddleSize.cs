using UnityEngine;

public class PowerUpPaddleSize : PowerUp
{
    public override void Execute()
    {
        var paddle = GameObject.FindGameObjectWithTag("Paddle");
        
        if (paddle != null)
        {
            var paddleScript = paddle.GetComponent<Paddle>();

            Transform paddleTransform = paddle.transform;

            // Cambiar tamaño de la pala
            Vector3 originalScale = paddleTransform.localScale;

            // Solo cambiamos el tamaño si no lo ha cambiado ya
            if (paddleScript.OriginalScale.x == paddle.transform.localScale.x)
            {
                paddleTransform.localScale = new Vector3(originalScale.x * 1.5f, originalScale.y, originalScale.z);
                paddleScript.CalculatePaddleLimits();
            }
        }
    }
}
