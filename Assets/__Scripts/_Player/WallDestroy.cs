using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDestroy : MonoBehaviour
{
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AudioSource>().Play();
        }
    }


}
