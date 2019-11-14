using System.Text.RegularExpressions;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    public GameObject explosion;
    GameObject explosionPlayer;
    GameObject destroyWall;

   

    ChoiceBooster booster;

    public CreateWall lengthDestroy;


    private void Start()
    {
        if (lengthDestroy == null)
        {
            lengthDestroy = GameObject.Find("Wall").GetComponent<CreateWall>();
        }

        if(booster == null)
        {
            booster = GameObject.Find("Observ").GetComponent<ChoiceBooster>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Enemy" && collision.gameObject.name == "Player" || collision.gameObject.tag == "Player") //встречас снаряда с игроком его уничтожение 
        {
           
           
            var clone = Instantiate(explosion, transform.position, Quaternion.identity);
            clone.name = "ExplosionEnemy";
            string s = collision.name;
            string pattern = @"\D*";
            string target = "";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(s, target);

            if (booster.misunderstood)
            {
                destroyWall = GameObject.Find("wallEnemy: " + result);
            }
            else
                destroyWall = GameObject.Find("wall: " + result);
            if(destroyWall != null)
            lengthDestroy.destroyBordersLength += lengthDestroy.bordersLength[int.Parse(result)];
            //Debug.Log(lengthDestroy.bordersLength[int.Parse(result)]);

            Destroy(destroyWall);
            Destroy(gameObject);

            //Debug.Log(collision);
        }
    }
}
