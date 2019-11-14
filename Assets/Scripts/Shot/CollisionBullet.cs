using System.Text.RegularExpressions;
using UnityEngine;

public class CollisionBullet : MonoBehaviour
{
    public GameObject explosion;
    public Observer bullet;
    GameObject explosionPlayer;
    GameObject destroyWall;



    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (gameObject.tag == "Player" && (collision.gameObject.name == "Enemy" || collision.gameObject.tag == "Enemy"))
        {
           
            if (explosionPlayer == null)
            {
                explosionPlayer = Instantiate(explosion, transform.position, Quaternion.identity);
                explosionPlayer.name = "Explosion";
            }
            string s = collision.name;
            string pattern = @"\D*";
            string target = "";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(s, target);

            destroyWall = GameObject.Find("wallEnemy: " + result);

            Destroy(destroyWall);


            Destroy(gameObject); 
        }


    
    }
    
    
}
