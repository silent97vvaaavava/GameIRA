using UnityEngine;
using UnityEngine.UI;


public class ChangeHealth : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject health;
    public Sprite heartWasted, heartAdd;
    public GameObject showBar;
    public AudioClip explosion;

    private const string tagPlayer = "Player",
                   tagEnemy = "Enemy";

    private AudioSource music;

    private string tagPorE; // хранит тэг противника
    private Image healthFirst, healthSecond; // контейнеры для сердечек

    // static
    public static int numberWindow { get; private set; }

    private void Awake()
    {
        numberWindow = 0;
        healthFirst = health.transform.GetChild(0).GetComponent<Image>(); //кешируем объекты сердечек
        healthSecond = health.transform.GetChild(1).GetComponent<Image>();
        music = GetComponent<AudioSource>();

        // проверка на каком объекте висит скрипт
        if (gameObject.tag == tagEnemy) 
        {
            tagPorE = tagPlayer;
        }
        else
        if (gameObject.tag == tagPlayer)
        {
            tagPorE = tagEnemy;
        }


    }

    private void Update()
    {
        if (gameObject.tag == tagPlayer)
        {
            if (healthFirst.sprite == heartAdd)
            {
                return;
            }
            else
            {
               _Inventory.AddHealth(healthFirst, heartAdd);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ChangeHealthWhenHit(showBar, tagPorE, collision.tag);
    }

    // меняет количество оставшихся жизней 
    public  void ChangeHealthWhenHit(GameObject bar, string tag, string tagOpponent)
    {
        if(tagOpponent == tag)
        {

            if (healthFirst.sprite == heartAdd)
            {
                healthFirst.sprite = _Inventory.Shield(heartAdd, heartWasted);
            }
            else
            {
                healthSecond.sprite = _Inventory.Shield(heartAdd, heartWasted);
                switch (gameObject.tag)
                {
                    case tagPlayer: // если жизни закончились у игрока
                        if (numberWindow == 2)
                        {
                            numberWindow = 3;
                        }
                        else
                        {
                            numberWindow = 1;
                        }
                        break;
                    case tagEnemy: // если жизни закончились у врага 
                        if (numberWindow == 1)
                        {
                            numberWindow = 3;
                        }
                        else
                        {
                            numberWindow = 2;
                        }
                        break;
                }
            }

            if (music.clip == explosion)
            {
                music.Play();
            }
            else
            {
                music.clip = explosion;
                music.Play();
            }
        }
    }



}
