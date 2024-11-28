using UnityEngine;
using UnityEngine.SceneManagement;

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
            SceneManager.LoadScene("SeceneMain");
        }
    }
}
