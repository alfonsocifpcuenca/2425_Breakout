using UnityEngine;

public class PowerUp_ShrinkPaddle : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        var paddleScript = paddle.GetComponent<Paddle>();

        Transform paddleTransform = paddle.transform;

        // Cambiar tamaño de la pala
        Vector3 originalScale = paddleTransform.localScale;

        // Solo cambiamos el tamaño si no lo ha cambiado ya
        if (paddleScript.OriginalScale.x == paddle.transform.localScale.x)
        {
            paddleTransform.localScale = new Vector3(originalScale.x * 0.5f, originalScale.y, originalScale.z);
            paddleScript.CalculatePaddleLimits();
        }

        Destroy(this.gameObject);
    }
}
