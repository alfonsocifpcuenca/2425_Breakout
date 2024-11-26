using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Ball : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    [SerializeField]
    private GameObject startMessage;

    private GameObject paddleGameObject;

    // Posición de la bola respecto de la pala
    private Vector2 initialPosition = new Vector2(0.35f, 1.25f);

    private void Awake()
    {
        this.paddleGameObject = GameObject.FindGameObjectWithTag("Paddle");
        if (this.paddleGameObject == null)
        {
            Debug.LogError("No se ha encontrado el objeto con el tag 'Paddle'");
            this.gameObject.SetActive(false);
        }

        // Obtenemos el componente Rigidbody2D
        this.rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (GameManagerSingleton.Instance.BallStatus == BallStatus.WaitingToLaunch)
            ResetBallPosition();
    }

    void Update()
    {
        LaunchBall();

        CheckAndCorrectHorizontalMovement();
    }

    private void CheckAndCorrectHorizontalMovement()
    {
        // Si la pelota no está lanzada
        if (GameManagerSingleton.Instance.GameStatus != GameStatus.Playing)
            return;

        // Si la velocidad en Y es casi 0, la pelota se mueve casi horizontalmente
        if (Mathf.Abs(this.rigidbody2D.velocity.y) < 0.5f)
        {
            // Generar un nuevo ángulo aleatorio para la dirección
            float randomDirectionY = Random.Range(0.5f, 1f) * Mathf.Sign(this.rigidbody2D.velocity.y);

            // Aplicar la nueva dirección manteniendo la magnitud de la velocidad actual
            this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, randomDirectionY).normalized * this.rigidbody2D.velocity.magnitude;
        }
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
            this.rigidbody2D.velocity = newDirection.normalized * GameManagerSingleton.Instance.BallVelocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManagerSingleton.Instance.NumbersOfBalls > 1)
        {
            GameManagerSingleton.Instance.SubstractBall();
            Destroy(this.gameObject);
        } 
        else 
        { 
            GameManagerSingleton.Instance.SubstractLive();
            GameManagerSingleton.Instance.GameStatus = GameStatus.Stop;
            GameManagerSingleton.Instance.BallStatus = BallStatus.WaitingToLaunch;
            this.ResetBallPosition();
        }
    }

    private void LaunchBall()
    {
        // Si pulsamos la tecla Space y la pelota no ha sido lanzada la lanzamos
        if (Input.GetKeyDown(KeyCode.Space) && GameManagerSingleton.Instance.BallStatus == BallStatus.WaitingToLaunch)
        {
            // Establecemos el estado de la pelota como 'Playing'
            GameManagerSingleton.Instance.BallStatus = BallStatus.Playing;

            // Sacamos a la pelota de la pala
            this.transform.parent = null;

            // Calculamos la dirección de lanzamiento de la bola
            Vector3 launchDirection = new Vector3(0.8f, 1, 0).normalized;

            // Aplicamos una velocidad a la bola en la dirección calculada
            this.rigidbody2D.velocity = launchDirection * GameManagerSingleton.Instance.BallVelocity;

            // Ocultamos el mensaje de ayuda para iniciar la partida
            this.startMessage?.SetActive(false);

            // Contamos los bloques restantes
            GameManagerSingleton.Instance.CountBlocks();
        }
    }

    /// <summary>
    /// Establece la posición y estados iniciales de la pelota
    /// </summary>
    private void ResetBallPosition()
    {
        // Quitamos la velocidad de la bola
        this.rigidbody2D.velocity = Vector2.zero;

        // Reestablecemos el tamaño de la pala
        this.paddleGameObject.transform.localScale = this.paddleGameObject.GetComponent<Paddle>().OriginalScale;

        // Calculamos nuevamente los límites
        this.paddleGameObject.GetComponent<Paddle>()?.CalculatePaddleLimits();

        // Establecemos la bola como hijo de la pala
        this.transform.parent = this.paddleGameObject.transform;

        // Situamos la bola en la posición incial
        this.transform.localPosition = initialPosition;
        
        // Mostramos el mensaje de tutorial
        this.startMessage?.SetActive(true);
    }
}
