using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    #region Level
    private int currentlevel = 1;
    public int CurrentLevel { get { return currentlevel; } set { } }

    public void LoadLevel()
    {
        // Cargamos el nuevo nivel
        TextAsset jsonFile = Resources.Load<TextAsset>($"Levels/_{currentlevel}");
        if (jsonFile == null)
        {
            // Si no hay nivel, nos hemos pasado el juego
            SceneManager.LoadScene("SeceneWin");
            return;
        }
        
        // Obtenemos el JSON
        string jsonData = jsonFile.text;

        // Parseamos el JSON
        Level nivel = JsonUtility.FromJson<Level>(jsonData);

        // Montamos el nivel
        CreateLevel(nivel);
    }

    void CreateLevel(Level levelData)
    {
        GameObject blockContainer = GameObject.Find("BlocksContainer");
        GameObject blockPrefab = Resources.Load<GameObject>("Prefabs/Block");

        // Limpiamos los posibles bloques restantes
        for (int i = blockContainer.transform.childCount - 1; i >= 0; i--)
        {
            // Desactivamos para ocultar
            blockContainer.transform.GetChild(i).gameObject.SetActive(false);

            // Marcamos para destruir al final del frame
            Destroy(blockContainer.transform.GetChild(i).gameObject);
        }

        // Parámetros de ajuste
        float blockWidth = 3.84f;
        float blockHeight = 1.28f;

        // Reiniciamos el contador de bloques
        this.blocksLeft = 0;

        // Recorrer las filas del JSON
        for (int y = 0; y < levelData.bloques.Count; y++)
        {
            string row = levelData.bloques[y];
            string[] columns = row.Split(' ');

            for (int x = 0; x < columns.Length; x++)
            {
                int blockType = int.Parse(columns[x]);

                // Si block es 0, es un espacio vacío
                if (blockType == 0)
                    continue;

                // Instanciar el bloque
                GameObject blockInstance = Instantiate(blockPrefab, blockContainer.transform);

                // Establecer posición local del bloque (0,0 es la esquina superior izquierda del BlockContainer)
                blockInstance.transform.localPosition = new Vector3((x * blockWidth) + (blockWidth/2), -y * blockHeight - (blockHeight/2), 0);

                // Configurar el tipo del bloque
                Block blockScript = blockInstance.GetComponent<Block>();
                blockScript.Config((BlockType)blockType);
                this.blocksLeft++;
            }
        }
        
        Debug.Log($"Hay {blocksLeft} bloques en el nivel {currentlevel}");
    }
    #endregion

    #region Lives
    private int lives = 3;
    public int Lives { get { return lives; } set { this.lives = value; } }
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
    #endregion

    #region Ball
    public int numbersOfBalls = 1;
    public int NumbersOfBalls { get { return this.numbersOfBalls; } }
    
    private float ballVelocity = 7f;

    public float BallVelocity { get { return this.ballVelocity; } set { this.ballVelocity = value; } }

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

        // Si nos pasamos el nivel
        if (this.blocksLeft <= 0)
        {
            // Reseteamos el juego
            this.ResetLevel();

            // Aumentamos el nivel
            this.currentlevel++;

            // Cargamos el nivel nuevo
            this.LoadLevel();
        }
    }
    #endregion

    private void ResetLevel()
    {
        // Eliminamos todas las bolas excepto 1, cualquiera
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        GameObject ballToKeep = balls[0];
        foreach (var ball in balls)
        {
            if (ball != ballToKeep)
            {
                ball.SetActive(false);
                Destroy(ball);
            }
        }
        // Establecemos el nº de bolas
        this.numbersOfBalls = 1;

        // Resetamos la bola principal
        ballToKeep.GetComponent<Ball>().ResetBallPosition();

        // Establecemos el estado de la bola
        this.BallStatus = BallStatus.WaitingToLaunch;
    }
}
