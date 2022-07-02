using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject DestroyPrefab;
    [SerializeField] private GameObject DamagePrefab;
    private Animator animator;
    private float fireRate = 0.3f;  //скорость стрельбы игрока
    private float nextfire = 0;
    private bool isPause;           //флаг паузы    
    private int lifePlayer = 5;     //счетчик жизней игрока
    //управление тектом    
    [SerializeField] private Text textLifeCounter;
    [SerializeField] private Text gameover;
    [SerializeField] private Text pause;
    //управление кнопками
    [SerializeField] private Button ButtonOut;
    [SerializeField] private Button ButtonRestart;
    void Start()
    {
        Time.timeScale = 1;
        //стартовая позиция игрока
        transform.position = new Vector3(20, 2, 1);
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MoveControlPlayer();
        GamePause();
        NextFirePlayer();
        LifeCounter();
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))//событие столкновения с лазером
        {
            //урон игроку
            PlayerLifs();
            //удаляем лазер после попадания
            Destroy(collision.gameObject);            
        }
    }
    private void NextFirePlayer()//стрельба
    {
        if (Time.time > nextfire)
        {
            //спавн лазера           координаты игрока  + 2 клетки вверх
            Instantiate(LaserPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            nextfire = fireRate + Time.time;
        }         
    }
    private void MoveControlPlayer() //управление игроком
    {
        int speedPlayer = 15; //скорость игрока
        //передача управления стрелкам и wasd. При нажатии кнопки будет либо +1 -1
        float horizonInput = Input.GetAxis("Horizontal");             
        float vertInput = Input.GetAxis("Vertical");
        //изменение направления движения по горизонтали
        transform.Translate(Vector3.right * Time.deltaTime * speedPlayer * horizonInput);
        //изменение направления движения по вертикали
        transform.Translate(Vector3.up * Time.deltaTime * speedPlayer * vertInput);
        //Анимация движения по горизонтали
        if (horizonInput > 0) animator.Play("PlayerTurnRight");
        else if(horizonInput < 0) animator.Play("PlayerTurnLeft");
            else animator.Play("PlayerIdle");
        //ограничение границы перемещения игрока
        if (transform.position.y < 1)
            transform.position = new Vector3(transform.position.x, 1, 1);
        if (transform.position.y > 19)
            transform.position = new Vector3(transform.position.x, 19, 1);
        if (transform.position.x < 1)
            transform.position = new Vector3(35, transform.position.y, 1);
        if (transform.position.x > 35)
            transform.position = new Vector3(1, transform.position.y, 1);
    }
    public void PlayerLifs()//смерть игрока
    {
        //анимация урона
        Instantiate(DamagePrefab, transform.position, Quaternion.identity);
        lifePlayer--;
        if (lifePlayer < 1)
        {
            //анимация взрыва игрока
            Instantiate(DestroyPrefab, transform.position, Quaternion.identity);
            //удаление игрока
            Destroy(this.gameObject);
            //активация текста и кнопок
            gameover.gameObject.SetActive(true);
            ButtonOut.gameObject.SetActive(true);
            ButtonRestart.gameObject.SetActive(true);
        }
    }
    private void GamePause()//пауза
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
            pause.gameObject.SetActive(isPause);
            ButtonOut.gameObject.SetActive(isPause);
            ButtonRestart.gameObject.SetActive(isPause);
        }
    }    
    public void LifeCounter()//счетчик жизней игрока на экране
    {
        textLifeCounter.text = "player life: " + lifePlayer;
    }
    /*private void LoadScorePlayer()
    {
        if (PlayerPrefs.HasKey("PlayerScoreSafe"))
        {
            scorePlayer = PlayerPrefs.GetInt("PlayerScoreSafe", scorePlayer);
        }
        else textScore.text = "Error loading";
    }
    private void SafeScorePlayer()
    {
        PlayerPrefs.SetInt("PlayerScoreSafe", scorePlayer);
        PlayerPrefs.Save();
    }
    private void DeleteScorePlayer()
    {
        scorePlayer = 0;
        PlayerPrefs.DeleteKey("X");
    }*/
}
