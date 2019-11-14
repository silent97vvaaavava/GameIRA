using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* присоединен к Enemy
 * не работает (забей пока)
 * 
 */ 


public class BotController : MonoBehaviour
{
    private Vector2 size;
    public TimerMain timer;
    public MainMenu ready;
    [SerializeField] private RectTransform limitUp;
    [SerializeField] private RectTransform limitDown;

    Vector3 limitUpPos;
    Vector3 limitDownPos;

    private void Start()
    {
        limitUpPos = Camera.main.ScreenToWorldPoint(limitUp.position);
        limitDownPos = Camera.main.ScreenToWorldPoint(limitDown.position);
        size = gameObject.GetComponent<Collider2D>().bounds.size;
        Reset();       
    }

    private void Update()
    {
        if (transform.position.y >= limitUpPos.y) //  ограничение передвежения по полю
        {
            transform.position = new Vector3(transform.position.x, limitUpPos.y, transform.position.z);    
        } 
        if (transform.position.y <= limitDownPos.y)
        {
            transform.position = new Vector3(transform.position.x, limitDownPos.y, transform.position.z);
            
        }
    }


    public void Reset()
    {
        if (timer.timeRound <=0 && ready.timerZero)
        
        transform.position = new Vector2(transform.position.x, Random.Range(-5.5f, 5f));
    }
}
