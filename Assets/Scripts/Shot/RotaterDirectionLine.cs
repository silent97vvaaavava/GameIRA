using UnityEngine;

public class RotaterDirectionLine : MonoBehaviour
{

    public float minAngle = -40; // ограничение 
    public float maxAngle = 40;

    public Transform beginPoint; // начальная точка
    Vector3 mousePosition;

    void Update()
    {
        mousePosition = Input.mousePosition;
        mousePosition.z = Camera.main.transform.position.z;
       // SetRotationDir();
    }

    /* поворачивает направляющую
     */
    public void SetRotationDir()
    {
        Vector3 lookIt = Camera.main.ScreenToWorldPoint(mousePosition);

        if(lookIt.x > beginPoint.position.x)
        {
            lookIt = lookIt - transform.position;
            float angle = Mathf.Atan2(lookIt.y, lookIt.x) * Mathf.Rad2Deg;
            angle = Mathf.Clamp(angle, minAngle, maxAngle);
            beginPoint.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
