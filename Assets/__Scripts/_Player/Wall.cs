using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Wall : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Header("Set in Inspector")]
    [SerializeField] private GameObject wallParent;
    public GameObject prefWall;
    public Slider slider;
    public GameObject cancel, ready;

    private List<GameObject> listWall;
    private List<float> listLengthDelta;
    private List<float> listLengthWall;


    private GameObject wall;
    private Vector2 posT, posB;

    float magnitudePos; 
    float length; // length Tower 

    float lengthDelete = 0; // length destroy wall

    float lengthField;  // all length wall
    float lengthConstField; // хранит длину поля и не меняет ее относительно стен
    float LengthField
    {
        get { return lengthField; }
        set
        {
            lengthField -= value;
        }

    }


    private void Awake()
    {
        listWall = new List<GameObject>();
        listLengthWall = new List<float>();
        listLengthDelta = new List<float>();

        magnitudePos = GetComponent<BoxCollider2D>().bounds.size.y / 2;

        lengthConstField = lengthField = transform.GetComponent<BoxCollider2D>().bounds.size.y;

        //slider.maxValue = LengthField;
        //slider.value = LengthField;
    }

   
    private void Update()
    {
        if (_Timer.Timer > (_Timer.newTime - .1f))
        {
            ClearListWall();
        }

        _Inventory.Thief(ref lengthConstField, ref slider, ref lengthDelete, ref lengthField);
        
     
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_Timer.Timer > 0 && (eventData.pointerCurrentRaycast.gameObject.name == gameObject.name || eventData.pointerCurrentRaycast.gameObject.name == "Top" || eventData.pointerCurrentRaycast.gameObject.name == "Bottom"))
        {
            CreateWallDrag(eventData.pointerCurrentRaycast.worldPosition, eventData.dragging);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_Timer.Timer > 0 && LengthField > 0)
        {
            CreateWallDown(eventData.pointerPressRaycast.worldPosition, eventData.dragging);
            gameObject.GetComponent<AudioSource>().Play();

        }
        // сделать проверку на расстояние, если расстояние между башнями больше чем оставшеяся длина, то не создавать или же отрегулировать расстояние 
        
    }

   


    /* 
     * создание стены и ее установка при простоном нажатии
     */
    void CreateWallDown(Vector2 pos, bool changeLength = false)
    {
        wall = Instantiate<GameObject>(prefWall, wallParent.transform);
        
        length = wall.transform.GetChild(0).GetComponent<BoxCollider2D>().bounds.size.y;
        pos.y = Mathf.Clamp(pos.y, -magnitudePos + length - 0.6f, magnitudePos - length - 0.6f);
        wall.transform.position = pos;
        wall.name = "" + (listWall.Count);
        posT = wall.transform.GetChild(0).position;
        posB = wall.transform.GetChild(2).position;

        var line = wall.transform.GetChild(0).GetComponent<LineRenderer>();
        line.SetPosition(1, posT);
        line.SetPosition(0, posB);
        listWall.Add(wall);
        ChangeSlider(posT, posB, length, changeLength);

    }

    /*
     * создание длинной стены
     */
    void CreateWallDrag(Vector2 pos, bool drag)
    {
        if (wall != null)
        {
            var top = wall.transform.GetChild(0);
            var center = wall.transform.GetChild(1);
            var bottom = wall.transform.GetChild(2);

            if (pos.y < wall.transform.position.y - length / 2)
            {
                
                if (Mathf.Abs(posT.y - bottom.position.y) + length < LengthField)
                {
                    pos.y = Mathf.Clamp(pos.y, -magnitudePos + length / 2 - 0.6f, posB.y);
                }
                else
                {
                    pos.y = Mathf.Clamp(pos.y, posT.y - LengthField + length / 2, posB.y);
                }
                pos.x = bottom.position.x;
                bottom.position = pos;
            }
            else
            if (pos.y > wall.transform.position.y + length / 2)
            {
                if ((Mathf.Abs(posB.y - top.position.y) + length) < LengthField)
                {
                    pos.y = Mathf.Clamp(pos.y, posT.y, magnitudePos - length / 2 - 0.6f);
                }
                else
                {
                    pos.y = Mathf.Clamp(pos.y, posT.y, posB.y + LengthField - length / 2);
                }
                pos.y = Mathf.Clamp(pos.y, posT.y, magnitudePos - length / 2 - 0.6f);
                pos.x = bottom.position.x;
                top.position = pos;
            }

            // середина стены
            var line = top.GetComponent<LineRenderer>();
            line.SetPosition(0, bottom.position);
            line.SetPosition(1, top.position);
            MakeCollider(top.position, bottom.position, center, line);

            // изменение показания слайдера
            ChangeSlider(top.position, bottom.position, length, drag);
        }
    }


    // создание коллайдера середины
    void MakeCollider(Vector2 posA, Vector2 posB, Transform center, LineRenderer line)
    {
        var collWall = center.GetComponent<BoxCollider2D>();
        collWall.size = line.bounds.size;
        var pos = center.position;
        pos.y = Mathf.Lerp(posA.y, posB.y, 0.5f);
        center.position = pos;
    }


    // работа слайдера
    // в процентном соотношении 
    void ChangeSlider(Vector3 posA, Vector3 posB, float delta, bool drag)
    {
        listLengthDelta.Add(Mathf.Abs(posA.y-posB.y) + delta);// запись изменения длины стены
        float deltaLength = 0f; // разница межд соседними

        // проверка нет ли следующего элемента после 0 в списке
        if(listLengthDelta.Count-1 == 0)
        {
            deltaLength = GetLengthPercent(listLengthDelta[listLengthDelta.Count - 1], lengthConstField)/100f;
            slider.value = slider.value - deltaLength;
        }
        else
        {
            deltaLength = GetLengthPercent(listLengthDelta[listLengthDelta.Count - 1] - listLengthDelta[listLengthDelta.Count - 2], lengthConstField)/100f;
            if (drag) // если стена меняет длины
            {
                if (listLengthDelta[listLengthDelta.Count - 1] > listLengthDelta[listLengthDelta.Count - 2])
                {
                    slider.value = slider.value - deltaLength;
                }
                else
                {
                    slider.value = slider.value + Mathf.Abs(deltaLength);
                }
            }
        }
    }



    // кэширование длины поставленной стены и сохранение оставшейся длины, обнуление списка динамических длин
    void HowLength()
    {
        listLengthWall.Add(listLengthDelta[listLengthDelta.Count - 1]);
        listLengthDelta.Clear();
        LengthField = listLengthWall[listLengthWall.Count - 1];
        Debug.Log(LengthField);
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        if (_Timer.Timer > 0 && LengthField > 0)
        {
            HowLength();
            //gameObject.GetComponent<AudioSource>().Play();

        }
        wall = null;


        // если стена поставлена - показать кнопки
        if (!cancel.activeSelf)
        {
            cancel.SetActive(true);
        }

        if (!ready.activeSelf)
        {
            ready.SetActive(true);
        }
    }

    // очистка массива с длинами и массива со стенами
    // проверка уничтоженных стен и высчитывание оставшейся длины
    public  void ClearListWall()
    {
        
        if (listWall.Count != 0)
        {
            foreach (var w in listWall)
            {
                if (!w.activeSelf)
                {
                    lengthDelete += listLengthWall[int.Parse(w.name)];
                }
                Destroy(w);
            }
            listWall.Clear();
            listLengthWall.Clear();
            LengthField -= lengthConstField - lengthDelete;
            slider.value = GetLengthPercent(LengthField, lengthConstField)/100f;
        }
    }

    float GetLengthPercent(float deltaX, float X)
    {
        return (deltaX)*100 / X;
    }
    
}
