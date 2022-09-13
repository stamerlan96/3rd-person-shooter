using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField, Min(0f)] public float spawnTime = 2f;
    [SerializeField, Min(0f)] public float spawnDelay = 5f;

    public Enemy enemyPrefab;


    private void Start()
    {
        InvokeRepeating("Spawn", spawnDelay, spawnTime);
    }
    public void Spawn()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        newEnemy.Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
