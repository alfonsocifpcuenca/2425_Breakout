using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    private Text text;
    
    void Start()
    {
        this.text = GetComponent<Text>();
        // Actualizamos el texto por primera vez
        this.TextUpdate();

        // Nos suscribimos al evento OnLevelChanged para actualizar el texto en la UI
        GameManagerSingleton.Instance.EventManager.OnLevelChanged.AddListener(TextUpdate);
    }

    private void TextUpdate()
    {
        // Actualizamos el nivel en función del GameManager
        this.text.text = $"NIVEL {GameManagerSingleton.Instance.LevelManager.CurrentLevel}";
    }
}
