using UnityEngine.UI;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text textScore;  //текст для вывода очков
    public static int score;   //счетчик очков
    private void Start()
    {
        score = 0;
        textScore = GetComponent<Text>();
    }
    void Update()
    {
        textScore.text = "Score: " + score;
    }
}
