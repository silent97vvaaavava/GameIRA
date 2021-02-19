using UnityEngine;
using UnityEngine.UI;


public class _Timer : MonoBehaviour
{
    /*
     * счет времени 
     * оповещение начала огня
     * отображение времени на табло
     */
    static public _Timer S; // singltone


    static bool endFight;
    public static bool ENDFIGHT
    {
        get { return endFight; }
        set
        {
            endFight = value;
        }
    }
    
    public static float newTime { get; private set; }

    public static float Timer
    {
        get
        {
            return timer;
        }
        set
        {
            if (timer < 0 && (_Hero.bullet == null && _Enemy.bullet == null) && _Hero.SHOT && _Enemy.SHOT && !ENDFIGHT)
            {
                timer = value;
                _Hero.SHOT = _Enemy.SHOT = false;
            }
        }
    }
    private static float timer;


    [Header("Set in Inspector")]
    public Text timeTablo; // отображенеи хода времени 
         
    private void Awake()
    {
        if (S == null)
        {
            S = this;
        }

        if (endFight)
        {
            endFight = false;
        }

        timer = newTime = 60f;
    }

    private void Start()
    {
        timeTablo.text = "01:00";
    }

    private void Update()
    {
        ShowTime();
    }

    private void LateUpdate()
    {
        Timer = newTime;  
    }

    //показ времени
    void ShowTime()
    {
        if (timer > 0 && !endFight)
        {
            timer -= Time.deltaTime;
        }

        if (Mathf.Round(timer) <= 9f)
        {
            timeTablo.text = "00:0" + Mathf.Round(timer).ToString();
        }
        else
        if (Mathf.Round(timer) >= 10 )
        {
            timeTablo.text = "00:" + Mathf.Round(timer).ToString();
        } else if(Mathf.Round(timer) <= 0)
        {
            timeTablo.text = "00:00";
        }
    }

    public void StopedTimer()
    {
        timer = -0.1f;
    }

 
}
