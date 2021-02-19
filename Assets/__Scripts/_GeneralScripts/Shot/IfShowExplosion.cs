using UnityEngine;

public class IfShowExplosion : MonoBehaviour
{
    
    [SerializeField] AudioClip clip;
    GameObject enemy;
    GameObject player;




    void Update()
    {
        if(GameObject.Find("ExplosionEnemy") && enemy == null)
        {
            enemy = new GameObject("explosionSound: 1");
            enemy.AddComponent<AudioSource>().clip = clip;
            enemy.GetComponent<AudioSource>().Play();

        }
        else
        if(GameObject.Find("Explosion") && player==null)
        {
            player = new GameObject("explosionSound: 2");
            player.AddComponent<AudioSource>().clip = clip;
            player.GetComponent<AudioSource>().Play();
        }


        if (player != null && player.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(player, 5f);
        }
        if (enemy != null && enemy.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(enemy, 5f);
        }

    }
}
