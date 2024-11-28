using UnityEngine;
using UnityEngine.UI;

public class UIBalls : MonoBehaviour
{
    private Text text;

    void Start()
    {
        this.text = GetComponent<Text>();
    }

    private void LateUpdate()
    {
        if (this.text != null)
            this.text.text = $"BOLAS {GameManagerSingleton.Instance.NumbersOfBalls.ToString("00")}";
    }
}
