using UnityEngine.UI;
using UnityEngine;
using System;
public class BossControl : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject DamagePrefab;
    private Transform findPlayer;   //���� ��� ������ ������
    private float nextfireEnemy = 0;
    private int lifeBoss = 20;      //����� �����
    void Start()
    {
        //�������� ���������� ������ � ����������
        findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //healBar = GetComponent<Image>();
        //HP = lifeBoss;
    }
    void Update()
    {
        FireControlBoss();
        MoveControlBoss();
        //healBar.fillAmount = HP / lifeBoss;
    }
    private void FireControlBoss()
    {
        float fireRate = 1;             //�������� ��������
        //���� ���� �������� ������, �� �� �������� 
        if (Math.Abs(transform.position.x - findPlayer.position.x) < 1)
        {
            if (Time.time > nextfireEnemy)
            {
                //�����
                Instantiate(LaserPrefab, transform.position + new Vector3(-2, -0.4f, 0), Quaternion.AngleAxis(30,Vector3.up));
                //������
                Instantiate(LaserPrefab, transform.position + new Vector3(2, -0.4f, 0), Quaternion.identity);
                nextfireEnemy = Time.time + fireRate;
            }
        }
    }
    private void MoveControlBoss()
    {
        float speed = 2;          //�������� �������� �����
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
            Player playerControls = collision.GetComponent<Player>();
            //������ ������ ��������� ����� ������
            if (playerControls != null) playerControls.PlayerLifs();
        }
        if (collision.CompareTag("Asteroid"))
        {
            BossDamage();
        }
    }
}
