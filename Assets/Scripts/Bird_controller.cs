using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Spawner spawner;
    // Rigidbody of the player.
    private Rigidbody rb;
    private float movementX;
    private float borderPadding = 0.5f;
    private float speed = 6.7f;
    private int jumpForce = 7;
    private bool gameStarted = false;
    private bool gameOver = false;
    public Animator my_animator;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;

        if (HUDController.Instance != null)
        {
            HUDController.Instance.ShowStart();
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.ResetScore();
        }
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;

        if (movementX != 0)
        {
            StartGameIfNeeded();
        }
    }

    private void StartGameIfNeeded()
    {
        if (!gameStarted && !gameOver)
        {
            gameStarted = true;

            rb.useGravity = true;

            HUDController.Instance.StartGame();

            spawner.StartSpawning();
        }
    }

    void OnJump(InputValue jumpValue)
    {
        if (jumpValue.isPressed)
        {
            StartGameIfNeeded();

            if (!gameOver)
            {
                rb.AddForce(
                    Vector3.up * jumpForce,
                    ForceMode.Impulse
                );
            }
        }
    }

    void clampPlayerMovement()
    {
        Vector3 position = rb.position;

        //find distance between player and camera
        float distance = transform.position.z - Camera.main.transform.position.z;

        //find border of camera with the distance of Z
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + borderPadding;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance)).x - borderPadding;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1.0f, distance)).y - borderPadding;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, distance)).y + borderPadding;

    //Border control
    if (position.y >= topBorder)
    {
        position.y = topBorder;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;
    }

    if (position.y <= bottomBorder)
    {
        position.y = bottomBorder;

        Vector3 velocity = rb.linearVelocity;
        velocity.y = 0f;
        rb.linearVelocity = velocity;
    }
    
    if(position.x >= rightBorder)
        {
            position.x = rightBorder;

            Vector3 velocity = rb.linearVelocity;
            velocity.x = 0f;
            rb.linearVelocity = velocity;
        }
    
    if(position.x <= leftBorder)
        {
            position.x = leftBorder;
            Vector3 velocity = rb.linearVelocity;
            velocity.x = 0f;
            rb.linearVelocity = velocity;
        }

        rb.position = position;
    }

    void OnCollisionEnter(Collision collision)
    {
        //floor game over
        if (collision.gameObject.CompareTag("obstacle") || collision.gameObject.CompareTag("moving wall"))
        {
            LoseGame();
            return;
        }

        Transform parent = collision.transform.parent;

        if (parent != null && (parent.CompareTag("obstacle") || parent.CompareTag("moving wall")))
        {
            LoseGame();
        }
    }

    private void LoseGame()
    {

        gameOver = true;

        // Save score before resetting it
        int finalScore = ScoreManager.Instance.Score;

        // Show Game Over with final score
        HUDController.Instance.ShowGameOver(finalScore);

        // Reset the tracked score
        ScoreManager.Instance.ResetScore();

        // Stop player
        rb.useGravity = false;
        rb.linearVelocity = Vector3.zero;
        movementX = 0f;
        my_animator.Play("Death");
    }

    void Reset()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate()
    {
        if (!gameStarted || gameOver)
        {
            return;
        }

        Vector3 movement = new Vector3(movementX, 0f, 0f);

        clampPlayerMovement();

        rb.AddForce(movement * speed);
    }

    private void Update()
    {
        if (gameOver &&
            Keyboard.current != null &&
            Keyboard.current.anyKey.wasPressedThisFrame)
        {
            Reset();
        }
    }
}