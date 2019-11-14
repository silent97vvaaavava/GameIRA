using UnityEngine;

   /*
    * на каждом снаряде врага
    */


public class OnBecomeInvEmeny : MonoBehaviour
{
    //public FireEnemy bullet;

    //private void Start()
    //{
    //    bullet = Camera.main.GetComponent<Observer>();
    //}

    private void OnBecameInvisible()
    {
        //numBullet++;
        //bulletEn.countBulletEnemy = 1;
        //Debug.Log(bulletEn.countBulletEnemy); // провекра вышел ли снаряд за границу для каждого проверку
        Destroy(gameObject, 1);
    }
}
