using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartScene : MonoBehaviour
{
    [SerializeField] private Button ButtonSoundTrue;
    [SerializeField] private Button ButtonSoundFalse;
    public void StartGame() => SceneManager.LoadScene("MainScene");
    public void ExitTheGame() => Application.Quit();
}