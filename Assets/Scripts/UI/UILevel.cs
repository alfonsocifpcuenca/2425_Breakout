using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    private Text text;
    
    void Start()
    {
        this.text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (this.text != null)
            this.text.text = $"NIVEL {GameManagerSingleton.Instance.CurrentLevel}";
    }
}
