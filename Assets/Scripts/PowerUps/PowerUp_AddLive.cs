using UnityEngine;

public class PowerUp_AddLive : PowerUpBase
{
    protected override void ApplyEffect(GameObject paddle)
    {
        // Sumamos una vida al player
        GameManagerSingleton.Instance.Player.AddLive();
        Destroy(this.gameObject); 
    }
}
