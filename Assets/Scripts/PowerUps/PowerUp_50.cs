using UnityEngine;

public class PowerUp_50 : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Suamanos 50 puntos 
        GameManagerSingleton.Instance.Player.AddPoints(50);
        Destroy(this.gameObject);
    }
}
