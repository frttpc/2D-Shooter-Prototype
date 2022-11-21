using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _spawnedObjectPrefab;
    [SerializeField] private int totalSpawnNumber;
    private int currentSpawnNumber;
    [SerializeField] private int spawnTimer;

    [SerializeField] [Tooltip("Experimental. Not working Yet!")] private bool usePooling = false;
    [SerializeField] private ObjectPool<GameObject> _spawnedObjectPool;

    private bool isSpawning = false;

    private void Update()
    {
        SpawnObject();
    }

    public void SpawnObject()
    {
        if (usePooling)
        {
            SpawnPooledObjects();
        }
        else
        {
            if(!isSpawning)
                StartCoroutine(SpawnSequentially());
        }
    }

    private void SpawnPooledObjects() 
    {
        if (_spawnedObjectPool.CountAll < totalSpawnNumber)
        {
            GameObject gameObject = Instantiate(_spawnedObjectPrefab, transform.position, Quaternion.identity);
            _spawnedObjectPool.Release(gameObject);
        }
        else
        {
            _spawnedObjectPool.Get();
        }
    }

    private IEnumerator SpawnSequentially()
    {   
        while (currentSpawnNumber < totalSpawnNumber)
        {
            Instantiate(_spawnedObjectPrefab, transform.position, Quaternion.identity);
            currentSpawnNumber++;
            isSpawning = true;
            yield return new WaitForSeconds(spawnTimer);
        }
        isSpawning = false;
    }
}
