using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    #region GameManager
    private static GameManagerSingleton instance;
    public static GameManagerSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = "GameManagerSingleton";
                instance = gameObject.AddComponent<GameManagerSingleton>();

                instance.Player = new Player();
                instance.BallManager = new BallManager();
                instance.LevelManager = new LevelManager();

                DontDestroyOnLoad(Instance.gameObject);
            }
            return instance;
        }
    }
    #endregion

    public Player Player;
    public BallManager BallManager;
    public LevelManager LevelManager;

    public BallStatus BallStatus { get; set; } = BallStatus.WaitingToLaunch;
    public GameStatus GameStatus { get; set; } = GameStatus.Stop;


    #region HelperMethods
    public void DestroyGameObject(GameObject go)
    {
        go.SetActive(false);
        Destroy(go);
    }

    public GameObject CreateGameObject(GameObject originalGo, Transform parentTransform)
    {
        return Instantiate(originalGo, parentTransform);
    }
    #endregion

}
