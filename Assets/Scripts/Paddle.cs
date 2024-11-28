using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private GameObject leftWall;

    [SerializeField]
    private GameObject rightWall;

    private float paddleLeftLimit;
    private float paddleRightLimit;
    public Vector3 OriginalScale { get; private set; }

    private void Awake()
    {
        GameManagerSingleton.Instance.LoadLevel();
        this.CalculatePaddleLimits();
        this.OriginalScale = transform.localScale;
    }

    public void CalculatePaddleLimits()
    {
        // Obtenemos el componente SpriteRenderer de la pared izquierda
        SpriteRenderer leftWallRenderer = this.leftWall.GetComponent<SpriteRenderer>();

        // Obtenemos el componente SpriteRenderer de la pared derecha
        SpriteRenderer rightWallRenderer = this.rightWall.GetComponent<SpriteRenderer>();

        // Obtenemos el SpriteRenderer de la pala
        SpriteRenderer paddleRenderer = this.GetComponent<SpriteRenderer>();

        // Si los SpriteRenderers son distintos de null, calculamos los límites
        if (leftWallRenderer != null && rightWallRenderer != null && paddleRenderer != null)
        {
            // Obtenemos el ancho de la pared izquierda
            float leftWallWidth = leftWallRenderer.bounds.size.x;
            // Obtenemos el ancho de la pared derecha
            float rightWallWidth = rightWallRenderer.bounds.size.x;
            // Obtenemos el ancho de la pala
            float paddleWidth = paddleRenderer.bounds.size.x;

            // Calculamos el límite izquierdo
            this.paddleLeftLimit = this.leftWall.transform.position.x + leftWallWidth / 2 + paddleWidth / 2;
            // Calculamos el límite dereche
            this.paddleRightLimit = this.rightWall.transform.position.x - rightWallWidth / 2 - paddleWidth / 2;
        }
        else
        {
            Debug.LogError("One or more required SpriteRenderers are missing.");
        }
    }

    void Update()
    {
        MovePaddle();
    }
    
    private void MovePaddle()
    {
        // Obtenemos la direccion de movimiento
        // -1 = izquieda
        // 0 = no se mueve
        // 1 = derecha
        float moveInput = Input.GetAxisRaw("Horizontal");

        // Obtenemos la posición actual de la pala en el eje X
        float currentXPosition = this.transform.position.x;
        
        // Calculamos la nueva posicion de la pala
        float newXPosition = moveInput * GameManagerSingleton.Instance.BallVelocity * Time.deltaTime + currentXPosition;

        // Acotamos el valor para no exceder los límites
        newXPosition = Mathf.Clamp(newXPosition, this.paddleLeftLimit, this.paddleRightLimit);

        // Establecemos la nueva posición a la pala
        Vector3 newPosition = new Vector3(newXPosition, this.transform.position.y, this.transform.position.z);
        this.transform.position = newPosition;
    }
}
