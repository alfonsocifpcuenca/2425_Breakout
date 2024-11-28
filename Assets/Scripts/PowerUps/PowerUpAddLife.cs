public class PowerUpAddLife : PowerUp
{
    public override void Execute()
    {
        GameManagerSingleton.Instance.AddLive();
    }
}

