using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
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
                
                DontDestroyOnLoad(Instance.gameObject);
            }
            return instance;
        }
    }


    private int lives = 3;
    public int Lives { get { return lives; } }

    private int blocksLeft = 0;

    public void SubstractLive()
    {
        Debug.Log("Quitamos una vida");
        this.lives--;

        if (this.lives < 0)
            Debug.Log("Game Over");
    }

    public void AddLive()
    {
        this.lives++;
    }

    public void CountBlocks()
    {
        this.blocksLeft = GameObject.FindGameObjectsWithTag("Block").Length;
        Debug.Log($"Quedan {blocksLeft} blocks");
    }
}
