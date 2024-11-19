using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int hits = 1;

    private Potenciador potenciador;

    public void TakeDamage()
    {
        // Restamos un golpe al bloque
        this.hits--;

        // Si los golpes que le quedan son 0 o menor (contemplamos cualquier posible bug), destruimos el objeto
        if (this.hits <= 0)
        {
            if (potenciador != null)
                potenciador.EjecutaMiAccion();
            
            Destroy(gameObject);
        }

        GameManagerSingleton.Instance.CountBlocks();

        
    }
}
