/*Wall
 */ 

using UnityEngine;
using UnityEngine.UI;


public class CreateWall : MonoBehaviour
{
    /*
     * создаем переменные для загрузки префабов
     */
    public GameObject TopBorder;
    public GameObject centerBorder;
    public GameObject BottomBorder;
    public Slider sliderBorder;

    [SerializeField] GameObject limitOne;


    //public Observer Field;
    //// public GameObject ReadyWall;

    //scripts
    [SerializeField] ChoiceBooster booster;

    GameObject bottom;
    GameObject top;
    GameObject center;
    GameObject temporaryBottom;
    GameObject temporaryCenter;
    GameObject temporaryTop;

    bool check;
    GameObject poboch;

    public const string nameTag = "Field";

    private Vector2 lastPos;
    private Vector2 startPos;
    private float deltaLength;

    // array 
    [HideInInspector] public float[] bordersLength; // записываем длину каждой стены
    [HideInInspector] public GameObject[] borders;
    [HideInInspector] public float destroyBordersLength = 0;

    float limitCreateWall = 1;

    int maxCountBorder = 15;

    float positionCenter;
    float distance; // длина одной стены
    float startDistance;
    int countTowers = 0;
    float lengthBorders;
    float bufLength = 1f;

    float fieldLength;

   

    private void Start()
    {
        //PlayerPrefs.SetInt("Thief", 5);

        bordersLength = new float[maxCountBorder];
        borders = new GameObject[maxCountBorder];
        fieldLength = PlayerPrefs.GetFloat(nameTag);
        sliderBorder.value = 1;
    }


    void PositionOnBoard()
    {
        Vector2 posTap = Vector2.zero;
        float distanceML;
        if (Input.GetMouseButtonDown(0))
        {
            posTap = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            distanceML = limitOne.transform.position.y - posTap.y;

            //Debug.Log(distanceML);
            //Debug.Log(posTap);
            //Debug.Log(distance);

            if (distanceML < distance)
            {
                Debug.Log(distance - distanceML+distance/2);

            }
        }
        
       



    }

    private void Update()
    {
        

        if (sliderBorder.value > 0 || sliderBorder.value >= PlayerPrefs.GetFloat("LengthWallBot"))
        BuildWall();
        SliderVue();
        BorderLength();
        if (Input.GetMouseButtonUp(0))
            {
                check = false;

                if (temporaryBottom == null)
                {
                    temporaryBottom = GameObject.Find("bottom: " + countTowers);
                    if (temporaryBottom != null)
                        temporaryBottom.tag = "Player";
                }

                if (temporaryTop == null)
                {
                    temporaryTop = GameObject.Find("top: " + countTowers);
                    if (temporaryTop != null)
                        temporaryTop.tag = "Player";
                }
                if (temporaryCenter == null)
                {
                    temporaryCenter = GameObject.Find("center: " + countTowers);
                    if (temporaryCenter != null)
                    {
                        temporaryCenter.transform.Find("Border: "+countTowers).tag = "Player";
                    }
                    
                }
            
            
        }
        PositionOnBoard();
        FillingOutArrayBorder();

        Clear();
        lastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        
    }

    void BorderLength()
    {
        // переделать под проценты
        bordersLength[countTowers] = distance + deltaLength * 2;
        lengthBorders = 0 + destroyBordersLength;
        
        for(var i = 1; i <= countTowers; i++) {
           
            lengthBorders += Mathf.Round(bordersLength[i]*100)/100;
           
        }
        if (booster.steal)
        {
            if(sliderBorder.value > 0)
            lengthBorders -= booster.thief;
        }

    }

    /* определение высоты игрового поля
     * подсчет длины поставленных стен
     * вывод результата в слайдере 
     */


    public void SliderVue()
    {

        if (sliderBorder.value >= 0)
        {

            sliderBorder.value = 1 - lengthBorders / fieldLength;
        }
    }


   


    void BuildWall()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
        var distancePoboch = Vector2.Distance(startPos, pos);
        
        if (hit.transform && hit.transform.tag == nameTag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPos = pos; // начальная позиция 
                CreateReadyWall();
                
            }
            else
            if (Input.GetMouseButton(0) && distancePoboch > startDistance && sliderBorder.value > 0)
            {
                
                if (pos.y < startPos.y)
                {
                    CreatWallDragDown(pos);

                }
               // else
                if (pos.y <= startPos.y + deltaLength && pos.y >= startPos.y - deltaLength * 2)
                {
                    GameObject.Find("bottom: " + countTowers).transform.position = new Vector2(startPos.x, startPos.y - deltaLength * 2);
                    
                }
               // else
                if (pos.y > startPos.y)
                {
                    CreatWallDragUp(pos);
                }
                
            }
            
                
        }
        


        /* если движение пошло вниз, то верхняя остается на прежней позиции, а нижняя опускается за мышью */



    }


    void CreateReadyWall()
    {
        check = true;
        countTowers++;
        GameObject folder = new GameObject("wall: "+countTowers);
        folder.layer = 8;
        folder.tag = "Player";

        if (top == null)
        {
            top = Instantiate(TopBorder, folder.transform) as GameObject;
            deltaLength = top.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            top.transform.position = new Vector2(startPos.x, startPos.y + deltaLength);
            top.layer = 8;
            top.tag = "Field";
            top.name = "top: " + countTowers;
        }


        if (center == null)
        {
            center = Instantiate(centerBorder, folder.transform) as GameObject;
            var length = center.transform.Find("Border").GetComponent<BoxCollider2D>().bounds.size.y / 2;
            center.transform.position = new Vector3(startPos.x, startPos.y - length, .1f);
            center.layer = 8;
            center.tag = "Player";
            center.name = "center: " + countTowers;
            center.transform.Find("Border").tag = "Field";
            center.transform.Find("Border").name = "Border: " + countTowers;
        }

       
        if (bottom == null)
        {
            bottom = Instantiate(BottomBorder, folder.transform, false) as GameObject;
            bottom.layer = 8;
            bottom.tag = "Field";
            bottom.name = "bottom: " + countTowers;
        }
       
        bottom.transform.position = new Vector2(startPos.x, startPos.y-deltaLength);
        distance = Vector2.Distance(top.transform.position, bottom.transform.position);
        startDistance = distance;
        
    }

    


    void CreatWallDragDown(Vector2 pos)
    {
        /*
        * добавлять центральную, которая тянется за нижней башней
        */
        //Debug.Log("work");
        if (check)
        {
            poboch = GameObject.Find("bottom: " + countTowers);
            poboch.transform.position = new Vector2(startPos.x, pos.y - deltaLength);
            // находим объект с именем и задаем ему координаты
            var top = GameObject.Find("top: " + countTowers);

            distance = Vector2.Distance(top.transform.position, poboch.transform.position);// проверяем расстояние между верхней и нижней башней


            if (distance > 0 && poboch.transform.position.y <= top.transform.position.y)
            {
                var center = GameObject.Find("center: " + countTowers).transform.Find("Border: "+countTowers);
                positionCenter = center.GetComponent<BoxCollider2D>().bounds.size.y / 2;
                var borderCenter = GameObject.Find("center: " + countTowers);
                borderCenter.transform.position = new Vector3(startPos.x, top.transform.position.y - distance / 2, .1f);
                borderCenter.transform.localScale = new Vector3(1, 3 * distance, 1);

            }
        }
        else
            poboch = null;
    }

    void CreatWallDragUp(Vector2 pos)
    {

        /*
       * добавлять центральную, которая тянется за верхней башней
       */
        if (check)
        {
            poboch = GameObject.Find("top: " + countTowers);
            poboch.transform.position = new Vector2(startPos.x, pos.y - deltaLength);
            // находим объект с именем и задаем ему координаты
            var bottom = GameObject.Find("bottom: " + countTowers);

            distance = Vector2.Distance(bottom.transform.position, poboch.transform.position);// проверяем расстояние между верхней и нижней башней


            if (distance > 0 && poboch.transform.position.y >= bottom.transform.position.y)
            {
                var center = GameObject.Find("center: " + countTowers).transform.Find("Border: "+countTowers);
                positionCenter = center.GetComponent<BoxCollider2D>().bounds.size.y / 2;
                var borderCenter = GameObject.Find("center: " + countTowers);
                borderCenter.transform.position = new Vector3(startPos.x, bottom.transform.position.y + distance / 2, .1f);
                borderCenter.transform.localScale = new Vector3(1, 3 * distance, 1);

            }
        }
        else
            poboch = null;
    }



    void Center(Vector3 position)
    {
        var center = Instantiate(centerBorder) as GameObject;
        center.transform.position = new Vector2(startPos.x, position.y);
    }

    //заполнение пробелов
     void Fill(Vector3 last, Vector3 current)
    {
        float step = 0.1f;
        bool result = true;

        while(result)
        {
            last = Vector3.MoveTowards(last, current, step);
            Center(last);

            if(last == current)
            {
                Vector2 lastPos = Camera.main.WorldToScreenPoint(last);
                result = false;
            }

        }
    }
     // пробел заполнить между верхней и центром!!!!

    void Clear()
    {
        top = null;
        bottom = null;
        center = null;
        temporaryBottom = null;
        temporaryCenter = null;
        temporaryTop = null;
        
    }

    /* 
 * заполнение массива со стенами 
 */
    void FillingOutArrayBorder()
    {
        for(var i = 0; i < maxCountBorder; i++)
        {
            if(borders[i] == null)
            {
                borders[i] = GameObject.Find("wall: " + (i + 1));
            }
        }

    }

    /*
     * удаление всехстен при нажатии клавиши Cancel
     */

    public void DestroyWall()
    {
        foreach(GameObject container in borders)
        {
            Destroy(container);
        }
        SliderClear();
        countTowers = 0;
    }

    /*
     * обнуление слайдера
     */
     void SliderClear()
    {
        sliderBorder.value = 1;
        for(var i = 1; i <= countTowers; i++)
        {
            bordersLength[i] = 0; //обнуление данных о стенах
            //limitCreateWall = 1;
        }
    }
   
}



