using UnityEngine;

public class DirectionGun : MonoBehaviour
{
    [HideInInspector] public GameObject[] dirrection; // массив стрелок направления
    int quantity = 55; // размер массива


    [SerializeField] GameObject directionArrow;
    [SerializeField] private Transform directionPosition;
    [SerializeField] Transform directionLine;

    public TimerMain time; // таймер
    public MainMenu ResetPosition; // реакция меню

    public SpriteRenderer arrowDir; // стрелка направления 
                                    //  public Transform arrowAim;

    public GameObject arrowPlayer;

    public int color = 95;
    public int deltaColor = 20;
    float startPositionX;



    int count = 1;

    // пустые переменные 
    GameObject firstArrow;


    private void Start()
    {
        dirrection = new GameObject[quantity];
        startPositionX = transform.position.x;

       ArrayDirection();
    }




    private void Update()
    {

        ArrowPlayer();
        //ResetDirection();

        ColorMissedArrow();
        ColorDrop();

    }

    private void OnTriggerExit2D(Collider2D collision) // когда выходит из объекта задает максимальный цвет
    {
        //Debug.Log(collision.gameObject.name);
        if (collision.tag == "AimArrow" && transform.localPosition.x >= collision.gameObject.transform.localPosition.x) // когда джостик вышел за предел указателя вправо активирует его работу
        {
            collision.gameObject.GetComponent<Animator>().enabled = true;
               }
        else
            if (transform.localPosition.x <= collision.gameObject.transform.localPosition.x) // джостик ушел влево - скрытие ячейки указателя 
        {
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision) // when arrowTrigger is over arrowDirection
    {
        if (transform.localPosition.x <= collision.gameObject.transform.localPosition.x)
        {
            collision.gameObject.GetComponent<Animator>().enabled = false;
            collision.gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
    }


    /* проверяет активны ли стрекли игрока
     * отключает направляющую
     */
    void ArrowPlayer()
    {
        if (arrowPlayer.activeSelf)
        {
            foreach (GameObject container in dirrection)
            {
                container.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
        }
    }

    /* заполняет массив объектами из стрелочек
     */
    void ArrayDirection()
    {
        for (int i = 0; i < quantity; i++)
        {
            if (dirrection[i] == null)
            {
                dirrection[i] = GameObject.Find("directionArrow: " + i) as GameObject;
            }
        }
    }

    /*начинает рисовать все заново, если был сделан тап по базе
     перенести в другой скрипт, чтобы нажатие определялось на базе*/
    public void ResetDirection()
    {
        transform.localPosition = new Vector2(0f, 0f);

        foreach (GameObject container in dirrection)
        {
            container.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        }
        directionLine.rotation = new Quaternion(0, 0, 0, 0);
        directionPosition.rotation = new Quaternion(0, 0, 0, 0);
        //directionLine.localPosition = new Vector2(.31f, .1f);
        //distance = startDistance;
        //count = 1;
    }

    /* пропущены ли стрелки и если да, то закрасить/активировать анимацию */
    void ColorMissedArrow()
    {
        for (int i = 1; i < quantity; i++)
        {

        
            if (dirrection[i - 1].GetComponent<SpriteRenderer>().color.a == 0 && dirrection[i].GetComponent<SpriteRenderer>().color.a > 0) // включение анимации
                {
                    dirrection[i-1].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                    dirrection[i - 1].GetComponent<Animator>().enabled = true;
                }
        }
        if (dirrection[quantity-1].transform.position.x <= transform.position.x && arrowPlayer.activeSelf == false) 
        {
            for (int i = 0; i < quantity; i++)
            {
                dirrection[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                dirrection[i].GetComponent<Animator>().enabled = true;
            }
            
        }
        ////отключение анимации в случае пропуска
        //for(int i = quantity-1; i > 0; i--)
        //{
           
        //    if (dirrection[i-1].GetComponent<SpriteRenderer>().color.a == 0 && dirrection[i].GetComponent<SpriteRenderer>().color.a > 0)
        //    {
        //        dirrection[i].GetComponent<Animator>().enabled = false;
        //        dirrection[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        //    }
        //}

    }

    // убирает все стрелки направления при нахождении пальца на базе
    void ColorDrop()
    {
        if (dirrection[1].transform.position.x >= transform.position.x)
        {
            for (int i = 0; i < quantity; i++)
            {
                dirrection[i].GetComponent<Animator>().enabled = false;
                dirrection[i].GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
            }
        }
    }


    /*
     * создает клоны направляющей стрелки при условии, 
     * что аим отдалился от начальной точки на определенное расстояние   
     */
    //void CreateDirectionArrow()
    //{
    //    //   Instantiate();
       
    //    if(transform.localPosition.x > distance && dirrection[0] == null)
    //    {

    //        dirrection[0] = Instantiate(directionArrow, directionLine) as GameObject;
    //        dirrection[0].name = "directionArrow: 0";
    //        dirrection[0].transform.localPosition = new Vector3(distance, 0, 0);
    //        dirrection[0].GetComponent<FixedJoint2D>().connectedBody = directionLine.GetComponent<Rigidbody2D>();
           
    //        distance = distance + deltaDistance;
    //        //Debug.Log(deltaDistance);
    //        //Debug.Log(transform.position);
    //    }
    // else
    //        if(dirrection[count] == null && transform.localPosition.x > distance && quantity -1  > count)
    //    {   
    //        dirrection[count] = Instantiate(directionArrow, directionLine) as GameObject;
    //        dirrection[count].name = "directionArrow: " + count;
    //        dirrection[count].transform.localPosition = new Vector3(distance, 0, 0);
    //        dirrection[count].GetComponent<FixedJoint2D>().connectedBody = directionLine.GetComponent<Rigidbody2D>();
    //        distance = distance + deltaDistance;
          
    //         count++;
    //       //Debug.Log(distance);
    //    }
    //    Debug.Log(count);
    //}


}
