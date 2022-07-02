using UnityEngine;
public class Asteroid : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    private readonly int speed = 5;
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down);
        //выход за пределы экрана внизу
        if (transform.position.y < -1.5) Destroy(this.gameObject);
    }
    private void AsteroidDestroy()
    {
        Destroy(this.gameObject);
        //анимация взрыва
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            AsteroidDestroy();
            ScoreCounter.score += 50;
            //удаляем лазер после попадания
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            AsteroidDestroy();
            //вычитание жизни игрока
            if (collision.TryGetComponent<Player>(out var playerControls)) playerControls.PlayerLifs();
        }
        if (collision.CompareTag("Enemy")) AsteroidDestroy();
    }
}