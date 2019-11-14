using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlaySoundsWhenTuchBorder : MonoBehaviour
{
    [SerializeField] AudioSource border;
    [SerializeField] AudioClip rebound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "L1" || collision.gameObject.name == "L2")
        {
            border.clip = rebound;
            //border.pitch = 2;
            border.Play();
            //Debug.Log(collision.gameObject.name);
        }
        
    }
}
