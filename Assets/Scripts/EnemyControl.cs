using UnityEngine;
using System;
public class EnemyControl : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject DamagePrefab;
    private Transform findPlayer;   //���� ��� ������ ������
    private float nextfireEnemy = 0;
    private int lifeEnemy = 2;      //����� �����
    private void Start()
    {
        //�������� ���������� ������ � ����������
        findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        MoveControl();
        FireControl();
    }
    private void MoveControl()
    {
        float speed = 3f;          //�������� �������� �����
        //���������� ����� �� ���������� ������
        transform.position = Vector3.MoveTowards(transform.position, findPlayer.position, speed * Time.deltaTime);
        //���� ���� ������ � ���� ������, �� �� ��������� ����
        if ( transform.position.y < 5)
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        //����� �� ������� ������ �����
        if (transform.position.y < -1.5) Destroy(this.gameObject);
    }
    private void FireControl()
    {
        float fireRate = 1;             //�������� ��������
        //���� ���� �������� ������, �� �� �������� 
        if (Math.Abs(transform.position.x - findPlayer.position.x) < 0.5f)
        {
            if (Time.time > nextfireEnemy)
             //����� ������
            Instantiate(LaserPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
            nextfireEnemy = Time.time + fireRate;
        }
    }
    private void EnemyDestroy()
    {
        //������� ������� ����� ��� ��������� � ���� ������
        Destroy(this.gameObject);
        //�������� ������
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
    private void EnemyDamage()
    {
        lifeEnemy--;
        //�������� �����
        Instantiate(DamagePrefab, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision) //������� �� ������������
    {
        if (collision.CompareTag("Laser"))
        {
            EnemyDamage();
            if (lifeEnemy < 1)
            {
                EnemyDestroy();
                ScoreCounter.score += 100;
            }
            //������� ����� ����� ���������
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            EnemyDestroy();
            //��������� ����� ������
            if (collision.TryGetComponent<Player>(out var playerControls))
                playerControls.PlayerLifs();
        }
        if (collision.CompareTag("Asteroid"))
        {
            EnemyDamage();
            if (lifeEnemy < 1)
                EnemyDestroy();
        }
    }
}