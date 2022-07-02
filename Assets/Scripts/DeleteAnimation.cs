using UnityEngine;
public class DeleteAnimation : MonoBehaviour
{    
    //Я не смог интегрировать это в код
    //Этот скрипт на префабе анимации
    void Start() => Destroy(this.gameObject, 1.5f);
}