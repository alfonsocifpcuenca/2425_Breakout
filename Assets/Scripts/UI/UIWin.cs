using UnityEngine;
using UnityEngine.UI;

public class UIWin : MonoBehaviour
{
    private Text text;

    void Start()
    {
        this.text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (this.text != null)
            this.text.text = $"¡HAS GANADO CON {GameManagerSingleton.Instance.Points.ToString("N0")} PUNTOS";
    }
}
