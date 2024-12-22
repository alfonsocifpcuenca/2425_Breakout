using UnityEngine;

public class PowerUp_500 : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Sumamos 500 puntos
        GameManagerSingleton.Instance.Player.AddPoints(500);
        Destroy(this.gameObject);
    }
}