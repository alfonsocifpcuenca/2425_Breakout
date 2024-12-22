using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manager para la escena GameOver
/// </summary>
public class SceneGameOver : MonoBehaviour
{
    // Variable para asegurarnos que se ejecuta una �nica vez
    bool locked = false;
    void Update()
    {
        // Si est� bloqueado la ejecuci�n no hacemos nada, as� prevenimos que este script pase dos veces por la l�gica
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
