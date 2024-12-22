using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager para la escena GameOver
/// </summary>
public class SceneGameOver : MonoBehaviour
{
    // Variable para asegurarnos que se ejecuta una única vez
    bool locked = false;
    void Update()
    {
        // Si está bloqueado la ejecución no hacemos nada, así prevenimos que este script pase dos veces por la lógica
        if (locked)
            return;

        // Si pulsamos la tecla cargamos la siguiente escena
        if (Input.GetKeyDown(KeyCode.Space))
        {
            locked = true;
            SceneManager.LoadScene(SceneName.SceneMain.ToString());
        }
    }
}
