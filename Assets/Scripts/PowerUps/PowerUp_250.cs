using UnityEngine;

public class PowerUp_250 : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Sumamos 250 puntos
        GameManagerSingleton.Instance.Player.AddPoints(250);
        Destroy(this.gameObject);
    }
}