using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UILifes : MonoBehaviour
{
    private Text text;
    
    void Start()
    {
        this.text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (this.text != null)
            this.text.text = $"VIDAS {GameManagerSingleton.Instance.Player.Lives}";
    }
}
