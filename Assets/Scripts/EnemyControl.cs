using UnityEngine;
using System;
public class EnemyControl : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject DamagePrefab;
    private Transform findPlayer;   //поле для поиска игрока
    private float nextfireEnemy = 0;
    private int lifeEnemy = 2;      //жизни врага
    private void Start()
    {
        //передаем координаты игрока в переменную
        findPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        MoveControl();
        FireControl();
    }
    private void MoveControl()
    {
        float speed = 3f;          //скорость движения врага
        //направляем врага на координаты игрока
        transform.position = Vector3.MoveTowards(transform.position, findPlayer.position, speed * Time.deltaTime);
        //если враг близок к низу экрана, то он пролетает мимо
        if ( transform.position.y < 5)
            transform.Translate(speed * Time.deltaTime * Vector3.down);
        //выход за пределы экрана внизу
        if (transform.position.y < -1.5) Destroy(this.gameObject);
    }
    private void FireControl()
    {
        float fireRate = 1;             //скорость стрельбы
        //если враг напротив игрока, то он стреляет 
        if (Math.Abs(transform.position.x - findPlayer.position.x) < 0.5f)
        {
            if (Time.time > nextfireEnemy)
             //спавн лазера
            Instantiate(LaserPrefab, transform.position + new Vector3(0, -2, 0), Quaternion.identity);
            nextfireEnemy = Time.time + fireRate;
        }
    }
    private void EnemyDestroy()
    {
        //удаляем корабль врага при попадании в него лазера
        Destroy(this.gameObject);
        //анимация взрыва
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
    private void EnemyDamage()
    {
        lifeEnemy--;
        //анимация урона
        Instantiate(DamagePrefab, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision) //реакция на столкновения
    {
        if (collision.CompareTag("Laser"))
        {
            EnemyDamage();
            if (lifeEnemy < 1)
            {
                EnemyDestroy();
                ScoreCounter.score += 100;
            }
            //удаляем лазер после попадания
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            EnemyDestroy();
            //вычитание жизни игрока
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