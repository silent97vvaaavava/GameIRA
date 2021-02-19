using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/* передвижение танка
 * поворот башни
 * создание тени последнего положения 
 * включение и выключение стрелок передвижения
 * траектория выстрела
 * инициализация снаряда и выстрел им в указанном направлении
 */

public class _Hero : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject arrowDrag;
    [SerializeField] Transform turret;
    [SerializeField] GameObject shadow;
    [SerializeField] GameObject field;
    [SerializeField] GameObject prefBullet;
    [SerializeField] float speed = 10f;
    [SerializeField] float minAngle = -95; // ограничение поврота башни
    [SerializeField] float maxAngle = 95;
    [SerializeField] GameObject cancel, ready;
    [SerializeField] AudioClip tapBase;


    LineRenderer trajectoryFirst; // траектория выстрела
    LineRenderer trajectoryBoost; // траектория для буста

    Vector3 startPosMouse; // положение мыши при первом нажатии  для передвижения базы

    Vector3 posUp, posDown;
    float sizeBoundHero;
    float sizeBoundField;
    static public GameObject bullet; // контейнер для созданного объекта из префаба 
    static public bool SHOT = false;
    Vector2 angleShot = Vector2.right;

    private Vector3 startPosBase;

    int shot = 0; // для буста

    
    private void Awake()
    {
        trajectoryFirst = turret.transform.GetChild(0).GetComponent<LineRenderer>();
        trajectoryBoost = turret.transform.GetChild(1).GetComponent<LineRenderer>();

        sizeBoundHero = transform.GetComponent<BoxCollider2D>().bounds.size.y/2;
        startPosBase = this.transform.position; // кэшируем стартовое положение базы 
        //trajectory.positionCount = 0;
    }

    private void Start()
    {
        sizeBoundField = field.GetComponent<BoxCollider2D>().bounds.size.y / 2;
    }

    // рисование траеткории
    void ShowTrajectory(LineRenderer trajectory, Vector2 posTurret, Vector2 posSecond, int posCount = 2)
    {
        Vector3[] point = { posTurret, posSecond };
        trajectory.positionCount = posCount;
        trajectory.SetPositions(point);
    }

    


   // поворот башни
    void RotateTurret(Vector3 posTurret, Vector3 posTap)
    {
        Vector2 lookAt = posTap - posTurret;
        //angleShot = posTap - turret.Find("target").transform.position;
        float angle = Mathf.Atan2(lookAt.y, lookAt.x) * Mathf.Rad2Deg;
        angle = Mathf.Clamp(angle, minAngle, maxAngle);
        turret.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //ShowTrajectory(turret.GetChild(0).transform.position, posTap);
    }

    // передвижение танка   
    // ограничение передвижения данные ограничения брать мз столкновения с коллайдерами
    void PositionBase(Vector3 posBase)
    {
        posBase.x = transform.position.x;
        posBase.y = Mathf.Clamp(posBase.y, -sizeBoundField + sizeBoundHero - 0.6f, sizeBoundField - sizeBoundHero - 0.6f);
        
        if (arrowDrag.activeSelf)
        {
            transform.position = posBase;
        }
    }

    // показ последнего положения танка
    void ShowShadow(Vector3 posShadow)
    {
        posShadow.x += 0.24f;
        if (arrowDrag.activeSelf && !shadow.activeSelf)
        {
            shadow.transform.position = posShadow;
            shadow.SetActive(true);
        }
        else
        {
            shadow.SetActive(false);
        }

    }

    // вкл/выкл стрелок движения
    // добавить звук постановки базы
    void ArrowDrag(bool active)
    {
        if(!active)
        {
            arrowDrag.SetActive(true);
            gameObject.GetComponent<AudioSource>().clip = tapBase;
            gameObject.GetComponent<AudioSource>().Play();
        }
        else
        {
            arrowDrag.SetActive(false);
            gameObject.GetComponent<AudioSource>().Play();

        }
    }

    // инициализация снаряда и его выстрел
    void Shot(float time = -.1f)
    {
        if(time < 0)
        {
            // проверить и убрать кнопку готово и отменить если они активны
            ControlButton();

            if (bullet == null && !SHOT)
            {
                if (!_Inventory.DoubleShot())
                {
                    Vector3 angle;
                    //проверить не нулевые ли значения для угла 
                    if (Vector3.SqrMagnitude(trajectoryFirst.GetPosition(1)) > 0 && Vector3.SqrMagnitude(trajectoryFirst.GetPosition(0)) > 0)
                    {
                        angle = trajectoryFirst.GetPosition(1) - trajectoryFirst.GetPosition(0);
                    }
                    else
                    {
                        angle = Vector3.right;
                    }
                    bullet = Instantiate<GameObject>(prefBullet, turret.Find("target").transform);
                    bullet.name = "PlayerBullet";
                    bullet.GetComponent<Rigidbody2D>().velocity = angle.normalized * speed;
                } else
                {
                    Vector3 angle;
                        //проверить не нулевые ли значения для угла 
                        if (Vector3.SqrMagnitude(trajectoryFirst.GetPosition(1)) > 0 && Vector3.SqrMagnitude(trajectoryFirst.GetPosition(1)) > 0)
                        {
                            angle = trajectoryFirst.GetPosition(1) - trajectoryFirst.GetPosition(0);
                        }
                        else
                        {
                            angle = Vector3.right;
                        }

                        Vector3 angleBoost;
                        //проверить не нулевые ли значения для угла 
                        if (Vector3.SqrMagnitude(trajectoryBoost.GetPosition(1)) > 0 && Vector3.SqrMagnitude(trajectoryBoost.GetPosition(1)) > 0)
                        {
                            angleBoost = trajectoryBoost.GetPosition(1) - trajectoryBoost.GetPosition(0);
                        }
                        else
                        {
                            angleBoost = Vector3.right;
                        }


                        bullet = Instantiate<GameObject>(prefBullet, turret.GetChild(0).transform);
                        bullet.name = "PlayerBullet";
                        bullet.GetComponent<Rigidbody2D>().velocity = angle.normalized * speed;
                        // boost
                        bullet = Instantiate<GameObject>(prefBullet, turret.GetChild(1).transform);
                        bullet.name = "PlayerBulletBoost";
                        bullet.GetComponent<Rigidbody2D>().velocity = angleBoost.normalized * speed;
                    // поставить после проверку уничтожены ли все снаряды

                }

                ShowShadow(transform.position);
                ArrowDrag(true);
                DropTrajectory();
                SHOT = true; // проверяет один ли выстрел
            }
        }
    }

    private void Update()
    {
        Shot(_Timer.Timer); // только один выстрел
    }

    private void LateUpdate()
    {
        if(_Timer.Timer > (_Timer.newTime - .1f))
        {
            transform.position = startPosBase;
            RotateTurret(Vector3.zero, Vector3.zero);
            // начальная позиция траекторий 
            DropTrajectory();
        }

        // if field scale than position base for axis x changed
        if(this.transform.position.x == field.transform.position.x)
        {
            return;
        }
        else
        {
            var posX = transform.position;
            posX.x = field.transform.position.x;
            this.transform.position = posX;
            startPosBase.x = posX.x;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        // проверяет можно ли еще менять позиции
        if (_Timer.Timer > 0)
        {
            PositionBase(eventData.pointerCurrentRaycast.worldPosition);

            // если выключены стрелки то вращать башней
            if (!arrowDrag.activeSelf)
            {
                RotateTurret(turret.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                // проверять включен ли буст
                if (!_Inventory.DoubleShot())
                {
                    ShowTrajectory(trajectoryFirst, turret.GetChild(0).transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                } else
                {
                    if(shot == 0)
                    {
                        ShowTrajectory(trajectoryFirst, turret.GetChild(0).transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                    else
                    {
                        ShowTrajectory(trajectoryBoost, turret.GetChild(1).transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
                    }
                }

            }
        }
        // включает кнопку cancel
        if(eventData.dragging && !cancel.activeSelf)
        {
            cancel.SetActive(true);
        }

        // включает кнопку ready
        if (eventData.dragging && !ready.activeSelf)
        {
            ready.SetActive(true);
        }

        
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        // подсчет для траекории
        if (_Inventory.DoubleShot() && eventData.dragging)
        {
            if (shot == 0)
            {
                shot++;
            }
            else
            {
                shot++;
            }
        }


        if (!eventData.dragging && _Timer.Timer > 0) // есть ли движение базы и не законченно ли время
        {
            ArrowDrag(arrowDrag.activeSelf);
            ShowShadow(transform.position);
            RotateTurret(Vector2.zero, Vector2.zero);
            // начальная позиция траекторий 

            DropTrajectory();

        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    // при нажатии на кнопку Cancel сбросить положение танка
    public void Reset()
    {
        transform.position = startPosBase;
        RotateTurret(Vector3.zero, Vector3.zero);
        // начальная позиция траекторий 
        DropTrajectory();
      

        ControlButton();
    }

    // начальная позиция траекторий 
    void DropTrajectory()
    {
        if (!_Inventory.DoubleShot())
        {
            ShowTrajectory(trajectoryFirst, Vector3.zero, Vector3.zero, 2);
        }
        else
        {
            ShowTrajectory(trajectoryFirst, Vector3.zero, Vector3.zero, 2);
            ShowTrajectory(trajectoryBoost, Vector3.zero, Vector3.zero, 2);
            shot = 0;
        }
    }
    
    void ControlButton()
    {
        if (ready.activeSelf)
        {
            ready.SetActive(false);
        }
        if (cancel.activeSelf)
        {
            cancel.SetActive(false);
        }
    }
}
