using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _Enemy.CountHit++;
            gameObject.transform.parent.gameObject.SetActive(false);
            gameObject.transform.parent.gameObject.transform.parent.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
