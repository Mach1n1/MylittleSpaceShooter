using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserControl : MonoBehaviour
{
    private int laserSpeed = 40; 
    void Update()
    {
        //                 направление * скорость   * константа времени 
        transform.Translate(Vector3.down * laserSpeed * Time.deltaTime);
        //                   расстояние     и                  время для удаления лазера
        if (transform.position.y < 0) Destroy(this.gameObject);
    }
}
