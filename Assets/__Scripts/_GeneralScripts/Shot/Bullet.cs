using UnityEngine;

public class Bullet : MonoBehaviour
{
    /* вращение снаряда 
     * удаление за выход границ
     * звуковые эффекты
     * непосредственно полет снаряда
     */

    [Header("Set in Inspector")]
    public AudioSource voiceSource;
    public AudioClip clipRebound;
    public GameObject explosion;

    GameObject explosionCreate;


    // вращение вдоль оси Z
    private void Update()
    {
        transform.Rotate(Vector3.forward, 8);
  
    }


    // удаление объекта если он вылетел за границы камеры
    private void OnBecameInvisible()
    {
        Destroy(gameObject, 3f);
    }

    // звук при ударе об бордюры
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "L1" || collision.gameObject.name == "L2")
        {
            voiceSource.clip = clipRebound;
            voiceSource.Play();
        }
    }

    // создание анимации взрыва при столкновении
    void Explosion()
    {
        if (explosionCreate == null)
        {
            explosionCreate = Instantiate<GameObject>(explosion, transform.position, Quaternion.identity);
            explosionCreate.name = "Explosion";

            gameObject.SetActive(false);

        }
    }

    // остлеживание столкновений 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.tag =="Player" && collision.gameObject.tag == "Enemy" )
        {
            Explosion();
        }
        else
        if (gameObject.tag == "Enemy" && (collision.gameObject.tag == "Player" || collision.gameObject.name == "Player"))
        {
            Explosion();
        }

    }

   
}
