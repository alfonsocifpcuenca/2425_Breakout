using UnityEngine;

public class PowerUp_100 : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Sumamos 100 puntos
        GameManagerSingleton.Instance.Player.AddPoints(100);
        Destroy(this.gameObject);
    }
}