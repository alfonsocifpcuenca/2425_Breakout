using UnityEngine;

public class Block : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int hits = 1;

    public void TakeDamage()
    {
        this.hits--;

        if (this.hits <= 0)
            Destroy(gameObject);
    }
}
