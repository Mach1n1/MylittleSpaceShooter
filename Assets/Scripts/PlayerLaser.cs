using UnityEngine;
public class PlayerLaser : MonoBehaviour
{
    private int laserSpeed = 40;
    void Update()
    {
        transform.Translate(laserSpeed * Time.deltaTime * Vector3.up);
        if (transform.position.y >= 20) Destroy(this.gameObject, 1);
    }
}