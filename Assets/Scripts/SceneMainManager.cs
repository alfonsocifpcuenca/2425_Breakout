using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMainManager : MonoBehaviour
{

    bool locked = false;
    void Update()
    {
        if (locked)
            return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            locked = true;
            GameManagerSingleton.Instance.BallVelocity = 6f;
            GameManagerSingleton.Instance.Lives = 5;
            GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
            SceneManager.LoadScene("SceneOne");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            locked = true;
            GameManagerSingleton.Instance.BallVelocity = 8f;
            GameManagerSingleton.Instance.Lives = 3;
            GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
            SceneManager.LoadScene("SceneOne");
        } 
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            locked = true;
            GameManagerSingleton.Instance.BallVelocity = 10f;
            GameManagerSingleton.Instance.Lives = 1;
            GameManagerSingleton.Instance.GameStatus = GameStatus.Playing;
            SceneManager.LoadScene("SceneOne");
        }
    }
}
