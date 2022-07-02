using UnityEngine;
public class DeleteAnimation : MonoBehaviour
{    
    void Start() => Destroy(this.gameObject, 1.5f);
}