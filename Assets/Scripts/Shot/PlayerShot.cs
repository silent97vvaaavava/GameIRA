using UnityEngine;


/*
 * к gunTurret
 * врещене башни
 * направление выстрела
 * сам выстрел
 * НЕ РАБОТАЕТ
 */

public class PlayerShot : MonoBehaviour
{
    //scripts
    public Observer existsPlayer;
  

    public float speed = 10f;
    public Rigidbody2D bullet;
    //public Transform bulPos;

    public Transform zRotate;
    public Transform PositionPL;
    public Transform ammunitionBoost;

    Quaternion firstShot;
    Quaternion secondShot;


    public float minAngle = -95;
    public float maxAngle = 95;

    public int numBullet = 0;
    public void SetRotation(Transform turret) // Добавить в Player Turret задает поворот головы
    {
        Vector3 mousePosMain = Input.mousePosition;
        mousePosMain.z = Camera.main.transform.position.z;
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePosMain);

        if (lookPos.x > PositionPL.position.x)
        {
            lookPos = lookPos - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            turret.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        

    }

   

}
