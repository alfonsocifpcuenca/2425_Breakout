using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int lives;
    public int Lives { get { return lives; } }


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        this.lives = 3;
    }

    public void SubstractLive()
    {
        this.lives--;

        if (this.lives < 0)
            Debug.Log("Game Over");
    }

    public void AddLive()
    {
        this.lives++;
    }
}
