using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    [SerializeField, Min(0f)] public float spawnDelay=5f;

    public Enemy enemyPrefab;


    private void Start()
    {
        InvokeRepeating("Spawn", 2, spawnDelay);
    }
    public void Spawn()
    {
        Enemy newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        newEnemy.Target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
