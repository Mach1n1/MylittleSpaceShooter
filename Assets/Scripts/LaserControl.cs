using UnityEngine;
public class LaserControl : MonoBehaviour
{
    private int laserSpeed = 40;
    void Update()
    {
        //                 ����������� * ��������   * ��������� ������� 
        transform.Translate(Vector3.up * laserSpeed * Time.deltaTime);
        //                   ����������     �                  ����� ��� �������� ������
        if (transform.position.y >= 20) Destroy(this.gameObject, 1);
    }
}
