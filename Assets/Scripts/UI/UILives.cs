using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UILives : MonoBehaviour
{
    private Text text;
    
    void Start()
    {
        this.text = GetComponent<Text>();
        // Actualizamos el texto por primera vez
        this.TextUpdate();

        // Nos suscribimos al evento OnLiveAdded para actualizar el texto en la UI
        GameManagerSingleton.Instance.EventManager.OnLiveAdded.AddListener(TextUpdate);
        GameManagerSingleton.Instance.EventManager.OnLiveLost.AddListener(TextUpdate);
    }

    private void TextUpdate()
    {
        // Actualiza el texto de las vidas en función del GameManager
        this.text.text = $"VIDAS {GameManagerSingleton.Instance.Player.Lives}";
    }
}
