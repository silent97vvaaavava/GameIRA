using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Enemy : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject enemyBase;
    [SerializeField] GameObject prefBullet;
    [SerializeField] BoxCollider2D field;
    [SerializeField] Transform turret;
    [SerializeField] private GameObject parentWall;
    [SerializeField] GameObject[] prefWall;
    [SerializeField] Transform fieldBase;


    //перфабы стены
    GameObject wall;

    private float speed = 10f;


    static public bool SHOT;
    static public GameObject bullet; 

    float magnitude;
    float heightBase;
    float maxAngle = 45f;
    float minAngle = -45f;

    Vector3 startPos;

    // количество уничтоженных стен
    private static int countHitWall = 0;

    public static int CountHit
    {
        get
        {
            return countHitWall;
        }
        set
        {
            countHitWall = value;
        }

    }


    private void Awake()
    {
        startPos = enemyBase.transform.position;
        //magnitude = field.bounds.size.y / 2; // кэшируем длину высоту поля
        CountHit = 0;
    }

    private void Start()
    {
        magnitude = field.bounds.size.y / 2; // кэшируем длину высоту поля

    }

    private void Update()
    {
        Position(_Inventory.Location());
        _Inventory.Strike(ref SHOT);
        Fire(_Timer.Timer);
        
       
    }

    private void LateUpdate()
    {
        // if field scale than position base changed
        if(enemyBase.transform.position.x == fieldBase.transform.position.x)
        {
            return;
        } else
        {
            var posX = enemyBase.transform.position;
            posX.x = fieldBase.transform.position.x;
            enemyBase.transform.position = posX;
            startPos.x = posX.x;
        }
    }

    // случайная позиция на поле с учетом размеров поля 
    void Position(float time = -0.1f)
    {
        if (time < 0 && enemyBase.transform.position == startPos)
        {
            if (!enemyBase.activeSelf)
            {
                enemyBase.SetActive(true);
                if (heightBase != 0)
                {
                    return;
                }
                else
                {
                    heightBase = enemyBase.GetComponent<BoxCollider2D>().bounds.size.y / 2;
                }
                magnitude = magnitude - heightBase;
            }
            // установка случайного положения
            Vector3 pos = enemyBase.transform.position;
            pos.y = Random.Range(-magnitude - 0.6f, magnitude - 0.6f);
            enemyBase.transform.position = pos;
            ShowWall(enemyBase.activeSelf);
        }
        else // при окончании раунда вернуть в начальное положение
        
        if (time > 0 && enemyBase.transform.position != startPos)
        {
            enemyBase.transform.position = startPos;
            if (enemyBase.activeSelf)
            {
                enemyBase.SetActive(false);
            }
            ShowWall(enemyBase.activeSelf);
        }

    }

    // выстрел под случайным углом
    // башню поварачивает в сторону выстрела
    void Fire(float time)
    {
        if (time < 0)
        {
            if (turret.rotation == Quaternion.identity)
            {
                turret.rotation = Quaternion.AngleAxis(Random.Range(minAngle, maxAngle), Vector3.forward);
                //Vector3 angle = turret.position - turret.Find("target").transform.position;
            }
            Vector3 angle = turret.position - turret.Find("target").transform.position;

            

            if (bullet == null && !SHOT)
            {
                bullet = Instantiate<GameObject>(prefBullet, turret.Find("target").transform);
                bullet.name = "bulletEnemy";
                bullet.GetComponent<Rigidbody2D>().velocity = angle.normalized * -speed;
                _Inventory.Misunderstood(bullet);
                SHOT = true;
            }
        } 
        else
        {
            turret.rotation = Quaternion.identity;
        }
    }

    void ShowWall(bool active)
    {
        int randWall;
        if (wall == null)
        {
            randWall = Random.Range(0, prefWall.Length);
            Vector3 posWall = new Vector3(Random.Range(0, 2.3f), Random.Range(-1.6f, 1.3f), 0);
            wall = Instantiate<GameObject>(prefWall[randWall], posWall, Quaternion.identity);
            wall.transform.parent = parentWall.transform;
            wall.name = "EnemyWall";
        }
        else
        if(!active)
        {
            Destroy(wall);
            wall = null;
        }

    }
}
