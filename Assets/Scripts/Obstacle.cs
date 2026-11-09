using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float speed;

    private Transform player;
    private Renderer[] renderers;

    public void setup(float moveSpeed, Transform playerTransform)
    {
        speed = moveSpeed;
        player = playerTransform;
        renderers = GetComponentsInChildren<Renderer>();
    }

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Find the actual back edge of the entire obstacle
        Bounds obstacleBounds = renderers[0].bounds;

        foreach (Renderer renderer in renderers)
        {
            obstacleBounds.Encapsulate(renderer.bounds);
        }

        // Because the obstacle moves toward negative Z,
        // max.z is the last part that passes the player
        if (obstacleBounds.max.z < player.position.z)
        {
            Destroy(gameObject);
        }
    }
} 