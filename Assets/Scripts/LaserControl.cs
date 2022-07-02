using UnityEngine;
public class LaserControl : MonoBehaviour
{
    private int laserSpeed = 40;
    void Update()
    {
        //                 направление * скорость   * константа времени 
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        //                   расстояние     и                  время для удаления лазера
        if (transform.position.y >= 20) Destroy(this.gameObject, 1);
    }
}
