using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    private Text text;

    void Start()
    {
        this.text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (this.text != null)
            this.text.text = $"PUNTOS {GameManagerSingleton.Instance.Points.ToString("000")}";
    }
}
