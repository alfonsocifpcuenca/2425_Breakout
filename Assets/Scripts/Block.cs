using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int hitsToDestroy = 2;
    
    [SerializeField]
    private int pointsPerHit = 100;

    private int hits = 0;

    [SerializeField]
    private BlockType type = BlockType.Yellow;

    private SpriteRenderer spriteRenderer;
    private Sprite normalSprite;
    private Sprite brokenSprite;

    private PowerUp powerUp;
    
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.SetInitialSprite();

        this.powerUp = PowerUpFactory.CreatePowerUp();

    }
    private void SetInitialSprite()
    {
        this.normalSprite = Resources.Load<Sprite>($"Sprites/{type}");
        this.brokenSprite = Resources.Load<Sprite>($"Sprites/{type}Broken");
        this.spriteRenderer.sprite = normalSprite;
    }

    public void TakeDamage()
    {
        // Restamos un golpe al bloque
        this.hits++;

        // Sumamos los puntos
        GameManagerSingleton.Instance.AddPoints(pointsPerHit);

        // Cambiamos el sprite en el primer golpe
        if (this.hits == 1)
        {
            this.spriteRenderer.sprite = brokenSprite;
        }

        // Si los golpes que le quedan son 0 o menor (contemplamos cualquier posible bug), destruimos el objeto
        if (this.hits >= this.hitsToDestroy)
        {
            if (this.powerUp != null)
            {
                this.powerUp.Execute();
            }
            
            Destroy(gameObject);
            GameManagerSingleton.Instance.SubstractBlock();
        }        
    }
}
