using UnityEngine;

/*
 * к BulletPl
 * снаряд за границы
 */ 


public class OnBecomeInPlayer : MonoBehaviour
{
    //public Observer bulletPl;
    //public int numBullet = 0;

    //private void Start()
    //{
    //    bulletPl = Camera.main.GetComponent<Observer>();
    //}

    private void OnBecameInvisible()
    {
        //numBullet++;
        //bulletPl.countBulletPLayer = 1;
        //Debug.Log(bulletPl.countBulletPLayer);// провекра вышел ли снаряд за границу для каждого проверку
        Destroy(gameObject, 1);
    }
 

   
}
