using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float launchSpeed = 10f;
    private bool isLaunched = false;
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private Transform paddle;

    // Posición de la bola respecto de la pala
    private Vector2 initialPosition = new Vector2(0.35f, 1.25f);

    private void Awake()
    {
        // Obtenemos el componente Rigidbody2D
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        ResetBallPosition();
    }

    void Update()
    {
        LaunchBall();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Obtenemos el componente IDamagable del objeto con el que hemos colisionado
        var objectDamagable = collision.gameObject.GetComponent<IDamagable>();

        // Si el objeto es distinto de null, llamamos a su método TakeDamage()
        if (objectDamagable != null)
        {
            objectDamagable.TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        this.ResetBallPosition();
    }

    private void LaunchBall()
    {
        // Si pulsamos la tecla Space y la pelota no ha sido lanzada la lanzamos
        if (Input.GetKeyDown(KeyCode.Space) && !this.isLaunched)
        {
            // Establecemos la pelota como lanzada
            this.isLaunched = true;

            // Sacamos a la pelota de la pala
            this.transform.parent = null;

            // Establecemos una dirección aleatoria de lanzamiento
            float randomDirection = Random.Range(-1f, 1f);

            // Calculamos la dirección de lanzamiento de la bola
            Vector3 launchDirection = new Vector3(randomDirection, 1, 0).normalized;

            // Aplicamos una velocidad a la bola en la dirección calculada
            this.rigidbody2D.velocity = launchDirection * launchSpeed;
        }
    }

    /// <summary>
    /// Establece la posición y estados iniciales de la pelota
    /// </summary>
    private void ResetBallPosition()
    {
        this.isLaunched = false;
        this.transform.parent = this.paddle;
        this.rigidbody2D.velocity = Vector2.zero;
        this.transform.localPosition = initialPosition;
    }
}
