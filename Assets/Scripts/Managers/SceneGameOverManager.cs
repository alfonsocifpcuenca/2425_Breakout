using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneGameOver : MonoBehaviour
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
