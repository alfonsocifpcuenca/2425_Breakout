using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class LevelManager
{
    // Variable para almacenar el nivel actual
    private int currentlevel = 1;

    // Variable para almacenar los bloques que faltan para acabar el nivel
    private int blocksLeft = 0;
    
    // Propiedad para obtener el nivel actual
    public int CurrentLevel { get { return currentlevel; } set { } }

    // Propiedad para obtener el número de bloques que quedan para acabar el nivel
    public int BlocksLeft { get { return blocksLeft; } }
        
    // Variable para almacenar el contenedor donde se ubicarán los bloques
    private GameObject blockContainer;
    // Variabla para almacenar el prefab de los bloques
    private GameObject blockPrefab;

    // Variables para controlar las dimensiones de los bloques
    private float blockWidth = 3.84f;
    private float blockHeight = 1.28f;

    /// <summary>
    /// Método que para restar un bloque
    /// </summary>
    public int SubstractBlock()
    {
        this.blocksLeft--;
        return blocksLeft;
    }

    /// <summary>
    /// Método para cambiar de nivel
    /// </summary>
    public void ChangeLevel()
    {
        // Reseteamos el juego
        this.ResetLevel();

        // Aumentamos el nivel
        this.currentlevel++;
        GameManagerSingleton.Instance.EventManager.OnLevelChanged.Invoke();

        // Cargamos el nivel nuevo
        this.LoadLevel();
    }

    /// <summary>
    /// Método para cargar un nuevo nivel
    /// </summary>
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
        Level levelData = JsonUtility.FromJson<Level>(jsonData);

        // Montamos el nivel
        this.CreateLevel(levelData);
    }

    /// <summary>
    /// Método para limpiar un nivel
    /// </summary>
    /// <param name="blockContainer"></param>
    private void ClearLevel(GameObject blockContainer)
    {    
        // Obtenemos todos los hijos del block container y los eliminamos, así nos aseguramos
        // que la pantalla queda limpia entre niveles
        for (int i = blockContainer.transform.childCount - 1; i >= 0; i--)
        {
            GameObject go = blockContainer.transform.GetChild(i).gameObject;
            GameManagerSingleton.Instance.DestroyGameObject(go);            
        }
    }

    /// <summary>
    ///  Método que crea un nivel
    /// </summary>
    /// <param name="levelData"></param>
    private void CreateLevel(Level levelData)
    {
        // Buscamos el block container si no lo tenemos localizado previamente
        this.blockContainer = this.blockContainer ?? GameObject.Find("BlocksContainer");
        
        // Cargamos el prefab del bloque si no lo tenemos cargado previamente
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

                // Si block es 0, es un espacio vacío
                if (blockType == 0)
                    continue;

                // Instanciar el bloque
                GameObject blockInstantiated = GameManagerSingleton.Instance.CreateGameObject(this.blockPrefab, this.blockContainer.transform);

                // Establecer posición local del bloque (0,0 es la esquina superior izquierda del BlockContainer)
                blockInstantiated.transform.localPosition = new Vector3((x * this.blockWidth) + (this.blockWidth / 2), -y * this.blockHeight - (this.blockHeight / 2), 0);

                // Configurar el tipo del bloque
                Block blockScript = blockInstantiated.GetComponent<Block>();
                blockScript.Config((BlockType)blockType);

                // Aumentamos el número de bloques
                this.blocksLeft++;

                // Añadir el bloque a la lista
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
                GameObject powerUp;
                do
                {
                    powerUp = PowerUpFactory.CreatePowerUp();
                    block.SetPowerUp(powerUp);
                } while (powerUp == null);
            }
        }
    }

    /// <summary>
    /// Método para resetear un nivel
    /// </summary>
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
