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
    }

    public void SustractLive()
    {
        this.lives--;

        if (this.lives == 0)
            SceneManager.LoadScene(SceneName.SceneGameOver.ToString());
    }

    public void SetLives(int lives)
    {
        this.lives = lives;
    }

    public void AddPoints(int points)
    {
        this.points += points;
    }

}
