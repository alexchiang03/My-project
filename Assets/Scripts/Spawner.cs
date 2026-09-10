using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private float spawnInterval = 3f;
    
    private float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Instantiate(wallPrefab, transform.position, Quaternion.identity);
            timer = 0f;
        }
    }
}
