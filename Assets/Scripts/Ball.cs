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

        // Si el objeto es la pala
        var objectPaddle = collision.gameObject.GetComponent<Paddle>();
        if (objectPaddle != null)
        {
            Vector3 paddlePosition = collision.transform.position;
            float paddleWidth = collision.collider.bounds.size.x;

            // Calcula el porcentaje de impacto de la bola en la pala (0 a 1)
            float hitPercent = (transform.position.x - paddlePosition.x) / paddleWidth + 0.5f;
            hitPercent = Mathf.Clamp01(hitPercent); // Limita el porcentaje entre 0 y 1

            // Mapea el porcentaje al rango de ángulos (45 a 135 grados)
            float minAngle = 45f;
            float maxAngle = 135f;
            float bounceAngle = Mathf.Lerp(maxAngle, minAngle, hitPercent);

            // Calcula la nueva dirección de la bola
            Vector2 newDirection = Quaternion.Euler(0, 0, bounceAngle) * Vector2.right;

            // Ajusta la velocidad manteniendo la dirección
            this.rigidbody2D.velocity = newDirection.normalized * this.launchSpeed;
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
            Vector3 launchDirection = new Vector3(0.8f, 1, 0).normalized;

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
