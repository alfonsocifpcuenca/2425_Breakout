using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager para la escena Main
/// </summary>
public class SceneMainManager : MonoBehaviour
{

    bool locked = false;

    private void Awake()
    {
        GameManagerSingleton.Instance.Player = new Player();
        GameManagerSingleton.Instance.BallManager = new BallManager();
        GameManagerSingleton.Instance.LevelManager = new LevelManager();
    }

    void Update()
    {
        if (locked)
            return;       

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            locked = true;
            GameManagerSingleton.Instance.BallManager.BallVelocity = 4f;
            GameManagerSingleton.Instance.Player.SetLives(5);
            GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
            SceneManager.LoadScene(SceneName.ScenePlay.ToString());
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            locked = true;
            GameManagerSingleton.Instance.BallManager.BallVelocity = 8f;
            GameManagerSingleton.Instance.Player.SetLives(3);
            GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
            SceneManager.LoadScene(SceneName.ScenePlay.ToString());
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            locked = true;
            GameManagerSingleton.Instance.BallManager.BallVelocity = 10f;
            GameManagerSingleton.Instance.Player.SetLives(1);
            GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
            SceneManager.LoadScene(SceneName.ScenePlay.ToString());
        }
    }
}
