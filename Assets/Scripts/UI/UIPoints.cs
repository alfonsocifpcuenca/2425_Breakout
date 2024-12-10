using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIPoints : MonoBehaviour
{
    private Text text;

    void Start()
    {
        this.text = GetComponent<Text>();
        // Actualizamos el texto por primera vez
        this.TextUpdate();

        // Nos suscribimos al evento OnPointsAdded para actualizar el texto en la UI
        GameManagerSingleton.Instance.EventManager.OnPointsAdded.AddListener(TextUpdate);
    }

    private void TextUpdate()
    {
        // Actualizamos el texto con los puntos del GameManager
        this.text.text = $"PUNTOS {GameManagerSingleton.Instance.Player.Points.ToString("N0")}";
    }
}
