using System.Collections;
using UnityEngine;
public class UseCoroutines : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject asteroidPrefab;
    [SerializeField] private GameObject BossPrefab;
    void Start()
    {
        StartCoroutine(EnemySpawn());
        StartCoroutine(AsteroidSpawn());
        StartCoroutine(BossSpawn());
    }
    IEnumerator BossSpawn()
    {
        //����, ���� �� ��������� ���� � ��������� ����
        yield return new WaitUntil( () => ScoreCounter.score > 500);
        int scorePlayer = ScoreCounter.score;
        Instantiate(BossPrefab, new Vector3(20, 22, 1), Quaternion.identity);
        StopCoroutine(EnemySpawn());
        StopCoroutine(AsteroidSpawn());
    }
    IEnumerator EnemySpawn()
    {
        while(true)
        {
            //����� �����                                    �� �����������
            Instantiate(enemyPrefab, new Vector3(Random.Range(1, 34), 22, 1), Quaternion.identity);
            yield return new WaitForSeconds(5);
        }
    }
    IEnumerator AsteroidSpawn()
    {
        while (true)
        {
            //����� ���������                              �� �����������
            Instantiate(asteroidPrefab, new Vector3(Random.Range(1, 34), 22, 1), Quaternion.identity);
            yield return new WaitForSeconds(2);
        }
    }
}