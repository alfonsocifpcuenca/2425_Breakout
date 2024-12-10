using UnityEngine;
using UnityEngine.SceneManagement;

public class Player 
{
    private int lives;
    private int points;

    public int Lives { get { return lives; } }  
    public int Points { get { return points; } }

    public Player()
    {
        this.lives = 3;
        this.points = 0;
    }

    public void AddLive()
    {
        this.lives++;
        GameManagerSingleton.Instance.EventManager.OnLiveAdded.Invoke();
    }

    public void SustractLive()
    {
        this.lives--;
        GameManagerSingleton.Instance.EventManager.OnLiveLost.Invoke();

        if (this.lives == 0)
            SceneManager.LoadScene(SceneName.SceneGameOver.ToString());
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
        GameManagerSingleton.Instance.EventManager.OnLivesChanged.Invoke();
    }

    public void AddPoints(int points)
    {
        this.points += points;
        GameManagerSingleton.Instance.EventManager.OnPointsAdded.Invoke();
    }

}
