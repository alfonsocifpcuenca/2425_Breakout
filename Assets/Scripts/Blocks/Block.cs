using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Block : MonoBehaviour, IDamagable
{
    // Variables de configuración
    private int hitsToDestroy = 1;
    private int pointsPerHit = 50;
    private BlockType type = BlockType.Yellow;


    private int hits = 0;
    private SpriteRenderer spriteRenderer;
    private Sprite normalSprite;
    private Sprite brokenSprite;
    private PowerUp powerUp;
    
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
        this.powerUp = PowerUpFactory.CreatePowerUp();
    }

    public void Config(BlockType blockType)
    {
        // Obtenemos una configuración `blockConfig` en función del tipo `blockType`
        BlockConfig blockConfig = BlockConfigFactory.GetConfig(blockType);

        // Establecemos las variables a partir de la configuración
        this.hitsToDestroy = blockConfig.hitsToDestroy;
        this.pointsPerHit = blockConfig.pointsPerHit;
        this.type = blockType;

        // Establecemos los sprites para el block, el normal y el roto
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
