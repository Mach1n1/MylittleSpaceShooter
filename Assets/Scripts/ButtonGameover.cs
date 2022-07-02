using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonGameover : MonoBehaviour
{   public void RestartGame() => SceneManager.LoadScene("MainScene");
    public void OutInMenu() => SceneManager.LoadScene("StartScene");
}