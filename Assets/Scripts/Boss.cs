using UnityEngine;
using System;
public class Boss : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject DamagePrefab;
    private Transform findPlayer;
    private float nextfireEnemy = 0;
    private int lifeBoss = 20;
    void Start()
    {
        findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        FireBoss();
        MoveBoss();
    }
    private void FireBoss()
    {
        float fireRate = 1;
        //���� ���� �������� ������, �� �� �������� 
        if (Math.Abs(transform.position.x - findPlayer.position.x) < 1)
        {
            if (Time.time > nextfireEnemy)
            {
                //����� �����
                Instantiate(LaserPrefab, transform.position + new Vector3(-2, -0.4f, 0), Quaternion.AngleAxis(30,Vector3.up));
                //������ �����
                Instantiate(LaserPrefab, transform.position + new Vector3(2, -0.4f, 0), Quaternion.identity);
                nextfireEnemy = Time.time + fireRate;
            }
        }
    }
    private void MoveBoss()
    {
        float speed = 2;
        //���������� ����� �� ���������� ������
        transform.position = Vector3.MoveTowards(transform.position, findPlayer.position, speed * Time.deltaTime);
        //���������� ����� �� ��������
        if (transform.position.y < 16)
            transform.Translate(speed * Time.deltaTime * Vector3.up);
    }
    private void BossDamage()
    {
        lifeBoss--;
        Instantiate(DamagePrefab, transform.position, Quaternion.identity);
        if (lifeBoss == 0)
        {
            Destroy(this.gameObject);
            Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
            ScoreCounter.score += 1000;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //������� �� ������������
    {
        if (collision.CompareTag("Laser"))
        {
            BossDamage();
            //������� ����� ����� ���������
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            BossDamage();
            //��������� ����� ������
            if (collision.TryGetComponent<Player>(out var playerControls)) playerControls.PlayerLifs();
        }
        if (collision.CompareTag("Asteroid"))
            BossDamage();
    }
}