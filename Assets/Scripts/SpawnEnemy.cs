using System.Collections;
using UnityEngine;
public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject EnemyPrefab;
    [SerializeField] private GameObject AsteroidPrefab;
    [SerializeField] private GameObject BossPrefab;
    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(AsteroidSpawn());
        StartCoroutine(BossSpawn());
    }
    IEnumerator BossSpawn()
    {
        //ждем, пока не наберутся очки и выпускаем боса
        yield return new WaitUntil( () => ScoreCounter.score > 500);
        Instantiate(BossPrefab, new Vector3(20, 22, 1), Quaternion.identity);
    }
    IEnumerator EnemySpawn()
    {
        while(true)
        {
            Instantiate(EnemyPrefab, new Vector3(Random.Range(1, 34), 22, 1), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator AsteroidSpawn()
    {
        while (true)
        {
            Instantiate(AsteroidPrefab, new Vector3(Random.Range(1, 34), 22, 1), Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}