using System;
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
                
                DontDestroyOnLoad(Instance.gameObject);
            }
            return instance;
        }
    }
    #endregion

    public BallStatus BallStatus { get; set; } = BallStatus.WaitingToLaunch;
    public GameStatus GameStatus { get; set; } = GameStatus.Stop;

    #region Lives
    private int lives = 3;
    public int Lives { get; set; }
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
    #endregion

    #region Ball
    public int numbersOfBalls = 1;
    public int NumbersOfBalls { get { return this.numbersOfBalls; } }
    
    private float ballVelocity = 7f;

    public float BallVelocity { get { return this.ballVelocity; } set { } }

    public void AddBall()
    {
        if (numbersOfBalls < 3)
            this.numbersOfBalls++;
    }

    public void SubstractBall()
    {
        this.numbersOfBalls--;
    }

    
    #endregion

    #region Points
    private int points = 0;
    public int Points { get { return points; } }
    public void AddPoints(int points)
    {
        this.points += points;
    }
    #endregion

    #region Blocks
    private int blocksLeft = 0;

    public int BlocksLeft { get { return blocksLeft; } }

    public void SubstractBlock()
    {
        this.blocksLeft--;
        if (this.blocksLeft <= 0)
            Debug.Log("Pasamos de pantalla");
    }
        public void CountBlocks()
    {
        this.blocksLeft = GameObject.FindGameObjectsWithTag("Block").Length;
    }
    #endregion
}
