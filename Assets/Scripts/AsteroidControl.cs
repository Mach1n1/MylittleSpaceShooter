using UnityEngine;
public class AsteroidControl : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    private readonly int speed = 5;     //скорость движения
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down); //вектор
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
            //доступ к методу класса Player
            Player playerControls = collision.GetComponent<Player>();
            //запуск метода вычитания жизни игрока
            if (playerControls != null) playerControls.PlayerLifs();
        }
        if (collision.CompareTag("Enemy")) AsteroidDestroy();
    }
}