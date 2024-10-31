using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonStart : MonoBehaviour
{
    public void OnClick()
    {
        SceneManager.LoadScene("SceneOne");
    }
}
