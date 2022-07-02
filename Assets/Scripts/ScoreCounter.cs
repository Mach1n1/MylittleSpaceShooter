using UnityEngine.UI;
using UnityEngine;
public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private Text textScore;
    public static int score;
    private void Start()
    {
        score = 0;
        textScore = GetComponent<Text>();
    }
    void Update() => textScore.text = "Score: " + score;
}