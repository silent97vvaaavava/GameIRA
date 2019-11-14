using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRotetion : MonoBehaviour
{
    public Rigidbody2D bullet;
   

    public Transform zRotate;
    public Transform PositionE;

    public float minAngle = -40;
    public float maxAngle = 40;

    public void SetRotation() // Добавить в Player Turret задает поворот головы
    {

        float angle = Mathf.Atan2(Random.Range(8f, 8f), PositionE.position.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        zRotate.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
}
