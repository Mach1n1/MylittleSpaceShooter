using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    [SerializeField] private GameObject LaserPrefab;
    [SerializeField] private GameObject DestroyPrefab;
    [SerializeField] private GameObject DamagePrefab;

    private Animator animator;
    private float fireRate = 0.3f;
    private float nextfire = 0;
    private bool isPause;
    private int lifePlayer = 5;
    //���������� ������    
    [SerializeField] private Text textLifeCounter;
    [SerializeField] private Text gameover;
    [SerializeField] private Text pause;
    //���������� ��������
    [SerializeField] private Button ButtonOut;
    [SerializeField] private Button ButtonRestart;
    void Start()
    {
        Time.timeScale = 1; //�� �����
        transform.position = new Vector3(20, 2, 1);//��������� ������� ������
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        MovePlayer();
        FirePlayer();
        LifeCounterUI();
        GamePause();
    }   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            PlayerLifs();
            Destroy(collision.gameObject);            
        }
    }
    private void FirePlayer()
    {
        if (Time.time > nextfire)
        {
            Instantiate(LaserPrefab, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            nextfire = fireRate + Time.time;
        }         
    }
    private void MovePlayer()
    {
        int speedPlayer = 15;
        //�������� ���������� �������� � wasd
        float horizonInput = Input.GetAxis("Horizontal");             
        float vertInput = Input.GetAxis("Vertical");
        //��������� ����������� �������� �� �����������
        transform.Translate(Vector3.right * Time.deltaTime * speedPlayer * horizonInput);
        //��������� ����������� �������� �� ���������
        transform.Translate(Vector3.up * Time.deltaTime * speedPlayer * vertInput);
        //�������� �������� �� �����������
        if (horizonInput > 0) animator.Play("PlayerTurnRight");
        else if(horizonInput < 0) animator.Play("PlayerTurnLeft");
            else animator.Play("PlayerIdle");
        //����������� ������� ����������� ������ �� ������
        if (transform.position.y < 1)
            transform.position = new Vector3(transform.position.x, 1, 1);
        if (transform.position.y > 19)
            transform.position = new Vector3(transform.position.x, 19, 1);
        if (transform.position.x < 1)
            transform.position = new Vector3(35, transform.position.y, 1);
        if (transform.position.x > 35)
            transform.position = new Vector3(1, transform.position.y, 1);
    }
    public void PlayerLifs()
    {
        //�������� �����
        Instantiate(DamagePrefab, transform.position, Quaternion.identity);
        lifePlayer--;
        if (lifePlayer < 1) //������
        {
            Instantiate(DestroyPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            //��������� ������ � ������
            gameover.gameObject.SetActive(true);
            ButtonOut.gameObject.SetActive(true);
            ButtonRestart.gameObject.SetActive(true);
        }
    }
    private void GamePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0 : 1;
            //��������� ������ � ������
            pause.gameObject.SetActive(isPause);
            ButtonOut.gameObject.SetActive(isPause);
            ButtonRestart.gameObject.SetActive(isPause);
        }
    }    
    public void LifeCounterUI() =>
        textLifeCounter.text = "player life: " + lifePlayer;
}
