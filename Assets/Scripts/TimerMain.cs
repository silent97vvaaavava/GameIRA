using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* зацеплен на MainCamera
 * дает счетчик времени
 * работа с UI
 * выключает и включает активность объектов Enemy и button
 */



public class TimerMain : MonoBehaviour
{
    public float timeRound;
    private int timer;
    public Text textTime;
    ShotBulletPL bulletPref;
    public Observer bulletPl;
    public Observer bulletEn;
    public MainMenu ready;
    public bool resetTur;
    public bool resetPos;

    // переменные числа выстрелов 
    public ShotBulletPL FirePlayer;
    public FireEnemy positionRandom;
    int numEnemy = 1;
    int numPlayer = 1;

    public FireEnemy Visible;
    //private Vector3 positionEnemy;

    // scripts
    [SerializeField] private PlayerPosition TimeWork;
    [SerializeField] private RotaterDirectionLine DirectWork;
    [SerializeField] private PlayerTurret TurretWork;
    [SerializeField] private CreateWall WallWork;
    [SerializeField] CreateWall destroy;
    [SerializeField] ChoiceBooster booster;

    // gameObjects
    [SerializeField] private GameObject buttonFight;
    [SerializeField] private GameObject Enemy;
    [SerializeField] private GameObject Turret;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private Transform gunPosition;
    [SerializeField] GameObject inventoryMenu;
    [SerializeField] Button inventory;


    // Audio Timer 
    [SerializeField] AudioSource timerSound;
    Vector2 startPositionPlayer;
    Vector2 startPositionGun;


    GameObject bE;
    GameObject bP;

    private void Start()
    {
        startPositionPlayer = playerPosition.position;
        startPositionGun = gunPosition.position;

        bP = new GameObject("just");
        bE = bP;
    }

    void Update()
    {
        Timer();
        ResetTimer();
        GetBulletNum();
        StartNewTimer();
        //Debug.Log(numEnemy);
        //Debug.Log(numPlayer);

        if (GameObject.Find("BulletE"))
        {
            
            bE = GameObject.Find("BulletE");
        }

        if (GameObject.Find("BulletPl"))
        {
            Destroy(GameObject.Find("just"));
            bP = GameObject.Find("BulletPl");
        }

        //Debug.Log(bP);
    }

    /*
     * начало нового раунда
     * помещает баззы в исходные положения
     * также с кнопками
     */
    void StartNewTimer()
    {
        if (bE == null && bP == null)
        {
            //Debug.Log("Work");
            timeRound = 60f;
            timerSound.Play();
            ready.timerZero = false;
            buttonFight.SetActive(true);
            Turret.transform.rotation = new Quaternion(0, 0, 0, 0);
            gunPosition.rotation = new Quaternion(0, 0, 0, 0);
            playerPosition.position = startPositionPlayer;
            gunPosition.position = startPositionGun;
            Enemy.SetActive(false);
            numEnemy = 1;
            numPlayer = 1;
            FirePlayer.numBullet = 0;
            Visible.countVisible = 0;
            TimeWork.enabled = true;
            DirectWork.enabled = true;
            TurretWork.enabled = true;
            WallWork.enabled = true;
            destroy.DestroyWall();
            bP = new GameObject("just");
            bE = bP;
            
            if(positionRandom.count != 0)
            {
                positionRandom.count = 0;
            }

            if(positionRandom.positionRandom != Vector3.zero)
            {
                positionRandom.positionRandom = Vector3.zero;
            }

            if(booster.shield)
            {
                booster.shield = false;
            }

            if(booster.strike)
            {
                booster.strike = false;
            }

            if(booster.misunderstood)
            {
                booster.misunderstood = false;
            }


            if(booster.ammunitionReady)
            {
                booster.ammunitionReady = false;
            }

            if (!inventory.enabled)
            {
                inventory.enabled = true;
            }

            if(booster.steal)
            {
                booster.steal = false;
            }
            // resetTur = true;
            // resetPos = true;
        }
    }

    /*
     * таймер и его отображение
     */
    void Timer()
    {
        if (timeRound >= 0)
            timeRound -= Time.deltaTime; // счет

        timer = (int)Mathf.Round(timeRound); // отображение
        if (timer >= 10)
        {
            textTime.text = "00:" + timer.ToString();
        }
        else
        if (timer >= 1)
        {
            textTime.text = "00:0" + timer.ToString();
        }
        else
            if (timer == 0)
        {
            textTime.text = "00:00";
           TimeWork.enabled = false;
           DirectWork.enabled = false;
           TurretWork.enabled = false;
           WallWork.enabled = false;
            inventory.enabled = false;

            timerSound.Stop();
            if(inventoryMenu.activeSelf)
            {
                inventoryMenu.SetActive(false);
            }
        }
    }

    /*
     * обнуление таймера
     */
     void ResetTimer()
    {
        if (ready.timerZero)
        {
            timeRound = 0f;
            textTime.text = "00:00";
            buttonFight.SetActive(false);
            ready.timerZero = false;
        }
    }

    /*
     * находит снаряды и инкрементирует когда их нет
     */
    void GetBulletNum()
    {
        if (timer == 0)
        {
            if (GameObject.Find("BulletPl"))
            {
                numPlayer++;
            }
            else
            {
                numPlayer = 0;
            }
            if (GameObject.Find("BulletE"))
            {
                numEnemy++;
            }
            else
            {
                numEnemy = 0;
            }

       
        }
    }


}
