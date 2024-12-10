using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    #region GameManager
    private static GameManagerSingleton instance;
    public static GameManagerSingleton Instance
    {
        get
        {
            // Si instance no está definido, lo creamos
            if (instance == null)
            {
                // Creamos un nuevo GameObject
                GameObject gameObject = new GameObject();
                // Establecemos el nombre
                gameObject.name = "GameManagerSingleton";
                // Añadimos el componet GameManagerSingleton
                instance = gameObject.AddComponent<GameManagerSingleton>();

                // Inicializamos todos los managers
                instance.Player = new Player();
                instance.BallManager = new BallManager();
                instance.LevelManager = new LevelManager();
                instance.EventManager = new EventManager();

                // Marcamos el objeto para que no se destruya entre escenas
                DontDestroyOnLoad(Instance.gameObject);
            }

            // Devolvemos la instancia
            return instance;
        }
    }
    #endregion

    // Variable encargada de gestionar todo lo referente al player (vidas, puntos...)
    public Player Player;

    // Manager encargado de gestionar las bolas (podemos tener varias simultáneas)
    public BallManager BallManager;

    // Manager encargado de gestionar los niveles (montarlos, establecer el nivel...)
    public LevelManager LevelManager;

    // Manager encargado de gestionar los eventos de la aplicación
    public EventManager EventManager;

    public BallStatus BallStatus { get; set; } = BallStatus.WaitingToLaunch;
    public GameStatus GameStatus { get; set; } = GameStatus.Stop;

    #region HelperMethods
    /// <summary>
    /// Desactivamos un objeto y lo destruimos
    /// </summary>
    /// <param name="go"></param>
    public void DestroyGameObject(GameObject go)
    {
        // Descativamos el objeto
        go.SetActive(false);

        // Marcamos el objeto para destruir al final del frame
        Destroy(go);
    }

    /// <summary>
    /// Creamos un gameobject
    /// </summary>
    /// <param name="originalGo"></param>
    /// <param name="parentTransform"></param>
    /// <returns></returns>
    public GameObject CreateGameObject(GameObject originalGo, Transform parentTransform)
    {
        return Instantiate(originalGo, parentTransform);
    }
    #endregion

}
