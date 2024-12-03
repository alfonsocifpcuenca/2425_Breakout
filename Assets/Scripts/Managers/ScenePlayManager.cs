using System.Net.Sockets;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePlayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelMenu;

    private void Awake()
    {
        if (panelMenu == null)
            this.gameObject.SetActive(false);
    }

    void Update()
    {

        if (GameManagerSingleton.Instance.GameStatus == GameStatus.Playing && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            GameManagerSingleton.Instance.GameStatus = GameStatus.Pause;
            this.panelMenu.SetActive(true);
        }

        if (GameManagerSingleton.Instance.GameStatus == GameStatus.Pause)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
                this.panelMenu.SetActive(false);
                Time.timeScale = 1f;
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                Application.Quit();
            }
        }        
    }
}
