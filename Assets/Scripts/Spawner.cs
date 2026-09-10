using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] wallPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int randomListObject;
    private float speed = 15f;
    [SerializeField] private Transform player;
    
    [SerializeField] private Material materialA;
    [SerializeField] private Material materialB;
    private bool useFirstMaterial = true;
    private float timer;

    private void SpawnObstacle()
    {
        randomListObject = Random.Range(0, wallPrefab.Length);
        GameObject walls = Instantiate(wallPrefab[randomListObject], transform.position, Quaternion.identity, transform);

        Material selectedMaterial;
        if (useFirstMaterial)
        {
            selectedMaterial = materialA;
        }
        else
        {
            selectedMaterial = materialB;
        }

        useFirstMaterial = !useFirstMaterial;

        Obstacle obstacleScript = walls.GetComponent<Obstacle>();
        if (obstacleScript != null)
        {
            obstacleScript.setup(speed, player, selectedMaterial);
        }
        else
        {
            Debug.LogError("Obstacle script ontbreekt op prefab: " + walls.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }
}
