using UnityEngine;
using UnityEngine.SceneManagement;

public class Player 
{
    // Variable para gestionar las vidas del player
    private int lives;
    // Variable para gestionar los puntos del player
    private int points;

    // Propiedad para acceder a las vidas del player
    public int Lives { get { return lives; } }  
    // Propiedad para acceder a los puntos del player
    public int Points { get { return points; } }

    // Constructor del player
    public Player()
    {
        // Iniciamos las vidas en 3
        this.lives = 3;
        // Iniciamos los puntos en 0
        this.points = 0;
    }

    /// <summary>
    /// Método para añadir una vida al player
    /// </summary>
    public void AddLive()
    {
        this.lives++;
        GameManagerSingleton.Instance.EventManager.OnLiveAdded.Invoke();
    }

    /// <summary>
    /// Método para quitar una vida al player, además gestiona si está muerto o no
    /// </summary>
    public void SustractLive()
    {
        this.lives--;
        GameManagerSingleton.Instance.EventManager.OnLiveLost.Invoke();

        if (this.lives == 0)
            SceneManager.LoadScene(SceneName.SceneGameOver.ToString());
    }

    /// <summary>
    /// Método para establecer las vidas del player
    /// </summary>
    /// <param name="lives"></param>
    public void SetLives(int lives)
    {
        this.lives = lives;
        GameManagerSingleton.Instance.EventManager.OnLivesChanged.Invoke();
    }

    /// <summary>
    /// Método para añadir puntos al player
    /// </summary>
    /// <param name="points"></param>
    public void AddPoints(int points)
    {
        this.points += points;
        GameManagerSingleton.Instance.EventManager.OnPointsAdded.Invoke();
    }

}
