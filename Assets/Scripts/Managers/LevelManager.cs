using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelManager
{
    private int currentlevel = 1;
    private int blocksLeft = 0;
    
    public int CurrentLevel { get { return currentlevel; } set { } }
    public int BlocksLeft { get { return blocksLeft; } }
        
    private GameObject blockContainer;
    private GameObject blockPrefab;
    private float blockWidth = 3.84f;
    private float blockHeight = 1.28f;

    public LevelManager()
    {

    }

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

    public void LoadLevel()
    {
        // Cargamos el nuevo nivel
        TextAsset jsonFile = Resources.Load<TextAsset>($"Levels/_{this.currentlevel}");
        if (jsonFile == null)
        {
            // Si no hay nivel, nos hemos pasado el juego
            SceneManager.LoadScene(SceneName.SceneWin.ToString());
            return;
        }

        // Obtenemos el JSON
        string jsonData = jsonFile.text;

        // Parseamos el JSON
        Level leveData = JsonUtility.FromJson<Level>(jsonData);

        // Montamos el nivel
        this.CreateLevel(leveData);
    }

    private void ClearLevel(GameObject blockContainer)
    {    
        for (int i = blockContainer.transform.childCount - 1; i >= 0; i--)
        {
            GameObject go = blockContainer.transform.GetChild(i).gameObject;

            // Desactivamos para ocultar
            GameManagerSingleton.Instance.DestroyGameObject(go);            
        }
    }

    private void CreateLevel(Level levelData)
    {
        this.blockContainer = this.blockContainer ?? GameObject.Find("BlocksContainer");
        this.blockPrefab = this.blockPrefab ?? Resources.Load<GameObject>("Prefabs/Block");

        // Borramos todos los bloques
        this.ClearLevel(this.blockContainer);

        // Reiniciamos el contador de bloques
        this.blocksLeft = 0;

        // Lista para almacenar los bloques instanciados
        List<Block> blocks = new List<Block>();

        // Recorrer las filas del JSON
        for (int y = 0; y < levelData.bloques.Count; y++)
        {
            string row = levelData.bloques[y];
            string[] columns = row.Split(' ');

            for (int x = 0; x < columns.Length; x++)
            {
                int blockType = int.Parse(columns[x]);

                // Si block es 0, es un espacio vac�o
                if (blockType == 0)
                    continue;

                // Instanciar el bloque
                GameObject blockInstantiated = GameManagerSingleton.Instance.CreateGameObject(this.blockPrefab, this.blockContainer.transform);

                // Establecer posici�n local del bloque (0,0 es la esquina superior izquierda del BlockContainer)
                blockInstantiated.transform.localPosition = new Vector3((x * this.blockWidth) + (this.blockWidth / 2), -y * this.blockHeight - (this.blockHeight / 2), 0);

                // Configurar el tipo del bloque
                Block blockScript = blockInstantiated.GetComponent<Block>();
                blockScript.Config((BlockType)blockType);

                // Aumentamos el n�mero de bloques
                this.blocksLeft++;

                // A�adir el bloque a la lista
                blocks.Add(blockScript);
            }
        }

        // Asociar potenciadores a bloques aleatorios
        if (levelData.potenciadores > 0)
        {
            // Elegir bloques aleatorios sin repetir
            List<Block> selectedBlocks = blocks.OrderBy(_ => Random.value).Take(levelData.potenciadores).ToList();

            foreach (Block block in selectedBlocks)
            {
                // Generar un potenciador y asociarlo al bloque
                PowerUp powerUp;
                do
                {
                    powerUp = PowerUpFactory.CreatePowerUp();
                    block.SetPowerUp(powerUp);
                } while (powerUp == null);
            }
        }

        Debug.Log($"Hay {blocksLeft} bloques en el nivel {currentlevel}");
    }

    private void ResetLevel()
    {
        // Eliminamos todas las bolas excepto 1, cualquiera
        foreach (var ball in GameManagerSingleton.Instance.BallManager.Balls.Skip(1).ToList())
        {
            GameManagerSingleton.Instance.BallManager.SubstractBall(ball);
            GameManagerSingleton.Instance.DestroyGameObject(ball.gameObject);
        }

        // Resetamos la bola principal
        GameManagerSingleton.Instance.BallManager.Balls[0].GetComponent<Ball>().ResetBallPosition();

        // Establecemos el estado de la bola
        GameManagerSingleton.Instance.BallStatus = BallStatus.WaitingToLaunch;
    }
}