using UnityEngine;

public class Ball : MonoBehaviour
{

    public float launchSpeed = 10f;
    private bool isLaunched = false;
    private Rigidbody2D rigidbody2D;
    public Transform paddle;

    private Vector2 initialPosition = new Vector2(0.35f, 1.25f);

    private void Awake()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        if (this.rigidbody2D == null)
        {
            Debug.LogError("Rigidbody no encontrado en el objeto Ball. Asegúrate de que el componente Rigidbody esté adjunto.");
            enabled = false;
        }
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
        // Comprobamos si el objeto con el que hemos colisionado (collision.transform) tiene IDamagable
        var objectDamagable = collision.gameObject.GetComponent<IDamagable>();
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
        if (Input.GetKeyDown(KeyCode.Space) && !this.isLaunched)
        {
            this.isLaunched = true;
            this.transform.parent = null;
            float randomDirection = Random.Range(-1f, 1f);
            Vector3 launchDirection = new Vector3(randomDirection, 1, 0).normalized;
            this.rigidbody2D.velocity = launchDirection * launchSpeed;
        }
    }

    private void ResetBallPosition()
    {
        this.isLaunched = false;
        this.transform.parent = this.paddle;
        this.rigidbody2D.velocity = Vector2.zero;
        this.transform.localPosition = initialPosition;
    }
}
