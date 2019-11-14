using UnityEngine;
using UnityEngine.EventSystems;

/* присоединен к Player 
 * 
 * определение позиции игрока
 * в update сброс его положения при нажатии клавиши или при новом отсчете времени
 * OnPointerClick определяет будет ли двигаться игрок
 * OnDrag перетягивание базы
 */



public class PlayerPosition : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    Vector3 positionShadow;
    public PlayerShot turret;
    GameObject arrow;
    int count = 0;

    [SerializeField] AudioSource pressTank;

    [SerializeField] private RectTransform limitUp;
    [SerializeField] private RectTransform limitDown;
    [SerializeField] private GameObject arrowU;
    [SerializeField] private GameObject arrowD;
    [SerializeField] private GameObject shadow;
    [SerializeField] private MainMenu ResetFightSet;
    public RotaterDirectionLine lineDirr;
    public TimerMain timerR;
    float distance;
    int time = 0;

    [SerializeField] private Transform directionLine;
    Vector2 positionLine;
    public Transform turretPosition;

    GameObject DirectionArrow;
    public DirectionGun emptyLine;

    public CreateWall CreateCheck; 

    private void Start()
    {

        shadow.SetActive(false);


        if(arrow == null)
        {
            arrow = GameObject.Find("Arrow");
            arrow.SetActive(false);
        }
    }




    private void Update()
    {
        if(DirectionArrow ==  null)
        {
            DirectionArrow = GameObject.Find("directionArrow: 0");
        }

        if (ResetFightSet.cancelFight || timerR.resetPos)
        {
            turret.zRotate.rotation = Quaternion.Euler(0, 0, 0);
            arrow.SetActive(false);
            shadow.SetActive(false);
            transform.position = new Vector2(transform.position.x, 0);
            ResetFightSet.cancelFight = false;
            timerR.resetPos = false;
        }

        PositionLineDirection();
        ActiveArrow();
        ActiveShadow();
        //SoundBase();
    }


   
    /*
     * включает тень преследования
     */
    void ActiveShadow()
    {
        if (count == 0)
        {
            positionShadow = new Vector2(shadow.transform.position.x, this.transform.position.y);
        }
        if (count == 1)
        {
            shadow.transform.position = positionShadow;
            shadow.SetActive(true);
        }
        else
            shadow.SetActive(false);
    }

    /*
     * дает положение линии направления
     */
     void PositionLineDirection()
    {
        
        if (arrow.activeSelf)
        {
            positionLine = new Vector2(directionLine.position.x, turretPosition.position.y);
            //positionLine = turretPosition.position;
            directionLine.position = positionLine;
        }
    }

    void ActiveArrow()
    {
  
        if (count == 1)
        {
            arrow.SetActive(true);
            if(!arrowD.activeSelf)
            {
                arrowD.SetActive(true);
            }
            if (!arrowU.activeSelf)
            {
                arrowU.SetActive(true);
            }
            turret.zRotate.rotation = Quaternion.Euler(0, 0, 0);
            lineDirr.beginPoint.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            //pressTank.Play();
            arrow.SetActive(false);
            count = 0;
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        var dragTank = Camera.main.ScreenToWorldPoint(Input.mousePosition);

       if(arrow.activeSelf )
        {
            transform.position = new Vector3(transform.position.x, dragTank.y, transform.position.z);
        }
        var limitUpPos = Camera.main.ScreenToWorldPoint(limitUp.position);
        var limitDownPos = Camera.main.ScreenToWorldPoint(limitDown.position);

        if (transform.position.y >= limitUpPos.y) //  ограничение передвежения по полю
        {
            transform.position = new Vector3(transform.position.x, limitUpPos.y, transform.position.z);
            arrowU.SetActive(false);
        }
        else arrowU.SetActive(true);
        if (transform.position.y <= limitDownPos.y)
        {
            transform.position = new Vector3(transform.position.x, limitDownPos.y, transform.position.z);
            arrowD.SetActive(false);
        }
        else
            arrowD.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        CreateCheck.enabled = true;
        if (DirectionArrow.GetComponent<SpriteRenderer>().color.a == 0)
        {
            pressTank.Play();
            count++;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
        /*
         * без нее не работает остальное
         */    
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CreateCheck.enabled = false;
        if (DirectionArrow.GetComponent<SpriteRenderer>().color.a != 0 && count == 0)
        {
            emptyLine.ResetDirection();
            
        }

    }

    void SoundBase()
    {
        if(Input.GetMouseButtonUp(0))
        if(arrow.activeSelf)
        {
            pressTank.Play();
        }
        else
        if(!arrow.activeSelf && DirectionArrow.GetComponent<SpriteRenderer>().color.a == 0)
        {
            pressTank.Play();
        }
    }
}

