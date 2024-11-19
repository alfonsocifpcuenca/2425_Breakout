using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int lives = 3;
    public int Lives { get { return lives; } }

    private int blocksLeft = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

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
