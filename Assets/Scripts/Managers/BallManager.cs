using System.Collections.Generic;
using UnityEngine;

public class BallManager
{
    // Establecemos el número máximo de bolas en pantalla
    private int maximumBalls = 12;

    // Velocidad de las bolas
    private float ballVelocity;

    // Variable que almancena en una lista las bolas que hay en pantalla
    private List<GameObject> balls = new List<GameObject>();

    // Propiedad para acceder a las bolas que hay en pantalla
    public List<GameObject> Balls { get { return balls; } }

    // Propiedad para obtener y establecer la velocidad de las bolas
    public float BallVelocity { get { return this.ballVelocity; } set { this.ballVelocity = value; } }

    public BallManager() { 
        // Iniciamos la lista de bolas
        this.balls = new List<GameObject>();

        // Establecemos una velocidad por defecto
        this.ballVelocity = 7f;
    }

    /// <summary>
    /// Método para añadir una bola al juego
    /// </summary>
    /// <param name="ball"></param>
    /// <returns></returns>
    public bool AddBall(GameObject ball)
    {
        // Si las bolas no han alcanzado el maximo añadimos la bola a la lista
        if (this.balls.Count < this.maximumBalls) {
            this.balls.Add(ball);
            return true;
        }

        // Si no hemos podido añadir la bola devolvemos false
        return false;
    }

    /// <summary>
    /// Método para eliminar una bola de la lista
    /// </summary>
    /// <param name="ball"></param>
    public void SubstractBall(GameObject ball)
    {
        // ELiminamos una bola de la lista de bolas
        this.balls.Remove(ball);
    }
}
