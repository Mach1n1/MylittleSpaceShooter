using UnityEngine;
public class EnemyLaser : MonoBehaviour
{
    private int laserSpeed = 40; 
    void Update()
    {
        transform.Translate(laserSpeed * Time.deltaTime * Vector3.down);
        if (transform.position.y < 0) Destroy(this.gameObject);
    }
}