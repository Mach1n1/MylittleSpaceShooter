using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class StartScene : MonoBehaviour
{
    [SerializeField] private Button ButtonSoundTrue;
    [SerializeField] private Button ButtonSoundFalse;
    //AudioSource mainSong;
    public void Start()
    {
        //mainSong = GetComponent<AudioSource>();
    }
    public void SoundOff()
    {
        ButtonSoundTrue.gameObject.SetActive(false);
        ButtonSoundFalse.gameObject.SetActive(true);
        //mainSong.volume = 0;
    }
    public void SoundPlay()
    {
        ButtonSoundTrue.gameObject.SetActive(true);
        ButtonSoundFalse.gameObject.SetActive(false);
        //mainSong.volume = 0.6f;
    }
    public void StartGame()//вызов сцены с игрой
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ExitTheGame()
    {
        Application.Quit();
    }
}
