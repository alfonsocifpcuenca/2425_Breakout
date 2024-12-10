public class PowerUpAddLive : PowerUp
{
    public override void Execute()
    {
        GameManagerSingleton.Instance.Player.AddLive();
    }
}
