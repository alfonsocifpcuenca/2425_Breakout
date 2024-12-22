using UnityEngine;

/// <summary>
/// Clase abstracta que se usa de base para los potenciadores
/// </summary>
public abstract class PowerUpBase : MonoBehaviour
{
    // Obligamos a declarar el método ApplyEffect que tendrá la lógica de cada potenciado
    protected abstract void ApplyEffect(GameObject paddle);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Si el potenciador choca contra la pala (Tag == "Paddle") aplicamos el efecto del mismo
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // Aplicamos el efecto pasando como parámetro el objeto contra el que hemos colisionado, en este caso
            // la pala
            this.ApplyEffect(collision.gameObject);
        }        
    }
}