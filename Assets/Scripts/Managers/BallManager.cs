using System.Collections.Generic;
using UnityEngine;

public class BallManager
{
    private int maximumBalls = 12;
    private float ballVelocity;

    private List<GameObject> balls = new List<GameObject>();
    public List<GameObject> Balls { get { return balls; } }
    public float BallVelocity { get { return this.ballVelocity; } set { this.ballVelocity = value; } }

    public BallManager() { 
        this.balls = new List<GameObject>();
        this.ballVelocity = 7f;
    }

    public bool AddBall(GameObject ball)
    {
        if (this.balls.Count < this.maximumBalls) {
            this.balls.Add(ball);
            return true;
        }

        return false;
    }

    public void SubstractBall(GameObject ball)
    {
        this.balls.Remove(ball);
    }
}
