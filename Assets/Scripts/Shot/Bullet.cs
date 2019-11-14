using UnityEngine;

public class Bullet : MonoBehaviour
{
   /*
    * на каждом снаряде (нет)
    * не работает
    */ 
   

    private void OnBecameInvisible()
    {
        
        Destroy(gameObject, 1);

    }
}
