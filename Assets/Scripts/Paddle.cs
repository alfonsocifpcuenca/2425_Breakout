using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private float speed = 8f;

    private float limits = 3.15f;

    void Update()
    {
        MovePaddle();
    }
    
    private void MovePaddle()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");

        float currentXPosition = this.transform.position.x;
        float xIncrement = moveInput * this.speed * Time.deltaTime;

        float newXPosition = Mathf.Clamp(currentXPosition + xIncrement, -limits, limits);

        Vector3 newPosition = new Vector3(newXPosition, this.transform.position.y, this.transform.position.z);
        this.transform.position = newPosition;
    }
}
