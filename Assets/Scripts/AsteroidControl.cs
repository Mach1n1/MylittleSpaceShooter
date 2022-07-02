using UnityEngine;
public class AsteroidControl : MonoBehaviour
{
    [SerializeField] private GameObject ExplosionPrefab;
    private readonly int speed = 5;     //�������� ��������
    void Update()
    {
        transform.Translate(speed * Time.deltaTime * Vector3.down); //������
        //����� �� ������� ������ �����
        if (transform.position.y < -1.5) Destroy(this.gameObject);
    }
    private void AsteroidDestroy()
    {
        Destroy(this.gameObject);
        //�������� ������
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            AsteroidDestroy();
            ScoreCounter.score += 50;
            //������� ����� ����� ���������
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Player"))
        {
            AsteroidDestroy();
            //������ � ������ ������ Player
            Player playerControls = collision.GetComponent<Player>();
            //������ ������ ��������� ����� ������
            if (playerControls != null) playerControls.PlayerLifs();
        }
        if (collision.CompareTag("Enemy")) AsteroidDestroy();
    }
}