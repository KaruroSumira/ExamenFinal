using UnityEngine;

public class SapoSpawner : MonoBehaviour
{
    public GameObject sapoJefePrefab;
    public Transform spawnPoint;
    private bool hasSpawned = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasSpawned && other.CompareTag("Player"))
        {
            Instantiate(sapoJefePrefab, spawnPoint.position, spawnPoint.rotation);
            hasSpawned = true;
            Destroy(gameObject);
        }
    }
}
