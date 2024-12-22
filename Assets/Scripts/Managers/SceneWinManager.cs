using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager para la escena Win
/// </summary>
public class SceneWinManager : MonoBehaviour
{

    bool locked = false;
    void Update()
    {
        if (locked)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            locked = true;
            SceneManager.LoadScene(SceneName.SceneMain.ToString());
        }
    }
}
