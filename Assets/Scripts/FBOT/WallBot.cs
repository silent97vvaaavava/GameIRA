using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBot : MonoBehaviour
{
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject bottom;
    [SerializeField] private GameObject center;
    [SerializeField] private GameObject positionBase;

    //scripts
    [SerializeField] private Observer Field;
    [SerializeField] ChoiceBooster booster;

    [SerializeField] Transform limit1;
    [SerializeField] Transform limit2;


    GameObject folder;
    GameObject deleteFolder;

    private GameObject[] Wall;
    private Vector3[] position;
    private float lengthWall;
    private float limitLength = 0;
 
    int countWall = 12;
    int numberOfWall = 1;

    GameObject clone;

    string tagEnemy = "Enemy";
    string tagPlayer = "Player";

    void Start()
    {
        Wall = new GameObject[countWall];
        position = new Vector3[countWall];
        // countWall = Random.Range(1, 8);
    }

  
    void Update()
    {
        WritePosition();
        StealBoost();
        //if(booster.destroyWall == 2 && booster.steal)
        //{
        //    limitLength += 2 * PlayerPrefs.GetFloat("LengthWallBot");
        //    Debug.Log(limitLength);
        //    Debug.Log( PlayerPrefs.GetFloat("Field"));
        //    booster.destroyWall = 0;
        //}
        //else 
        //if(!booster.steal)
        //{
        //    limitLength = 0;
        //}

        if (positionBase.activeSelf && numberOfWall <= countWall && limitLength <= PlayerPrefs.GetFloat("Field"))
        {            
            CreateWall();
            // BasePosition();
            PositionWall();
            numberOfWall++;
            ArrayWall();
            limitLength += lengthWall; 
        }
             
        HideWall();
        ShowWall();
    }

    /*создает стены относительно базы врага
     * далеко не отходит
     * пустой объект получает координаты базы по оси У
     * имеет свое положение по оси Х, относительно которого ставятся стены
     * длина стен определяется рондомно, но конечна
     * никогда не ставит на все поля стену
     * а маленькими кусочками 
     * сделать проверку на совпадение положений
     */
     
    /*
     * создание стены
     */
     void CreateWall()
    {
        folder = new GameObject("wallEnemy: " + numberOfWall);
        folder.tag = tagEnemy;
      

        var topClone = Instantiate(top, folder.transform);
        topClone.name = "topEnemy: " + numberOfWall;

        var centerClone = Instantiate(center, folder.transform);
        centerClone.name = "centerEnemy: " + numberOfWall;

        var bottomClone = Instantiate(bottom, folder.transform);
        bottomClone.name = "bottomEnemy: " + numberOfWall;
        lengthWall = Vector2.Distance(GameObject.Find("topEnemy: " + numberOfWall).transform.position, GameObject.Find("bottomEnemy: " + numberOfWall).transform.position);
    }

    /*
     * рандомное положение стен
     */
     void PositionWall()
    {
        folder.transform.position = new Vector3(positionBase.transform.position.x + Random.Range(-4, 4), positionBase.transform.position.y + Random.Range(-4, 4), 0) - new Vector3(5, 0, 0);
        if (folder.transform.position.y >= limit1.position.y)
        {
            folder.transform.position = new Vector3(folder.transform.position.x, limit1.position.y, folder.transform.position.z);
        }
        else
        if (folder.transform.position.y <= limit2.position.y)
        {
            folder.transform.position = new Vector3(folder.transform.position.x, limit2.position.y, folder.transform.position.z);
        }

    }

    /*
     * помещаем все стены в массив для хренения
     */
     void ArrayWall()
    {
        for(int i = 0; i < countWall; i++)
        {
                Wall[i] = GameObject.Find("wallEnemy: "+(i+1));
        }
        
    }

    /*
     * исчезновение стен после окончания раунда
     */
    void HideWall()
    {
        if (!positionBase.activeSelf) 
        {
            //for (int i = 0; i < countWall; i++)
            //{
            //        Wall[i].SetActive(false);
            //}
            foreach (GameObject wall in Wall)
            {
                if (wall != null)
                {
                    //wall.transform.position = new Vector3(positionBase.transform.position.x + Random.Range(-4, 4), positionBase.transform.position.y + Random.Range(-4, 4), 0) - new Vector3(5, 0, 0);
                    wall.SetActive(false);
                }
            }

        } 
    }

    /*
     * меняется позиция стен 
     * отображаюся
     */
     void ShowWall()
    {
        if (positionBase.activeSelf)
        {
            var rand = Random.Range(1, 8);
            for (var i = 1; i < rand; i++)
            {
                if (Wall[i] != null)
                {
                    Wall[i].SetActive(true);
                    Wall[i].transform.position = position[i];
                    FieldWall(i);
                    ChangeTag(booster.misunderstood, i);
                }
            }
        }
        //for(int i = 0; i < countWall; i++)
        //{
        //    if(Wall[i] != null)
        //    {
        //        Wall[i].SetActive(true);
        //        Wall[i].transform.position = position[i];
        //    }
        //}
    }

    void WritePosition()
    {
       if(positionBase.activeSelf)
        for(int i = 0; i < countWall; i++)
        {
            if(position[i] == Vector3.zero)
            {
                position[i] = new Vector3(positionBase.transform.position.x + Random.Range(-4, 4), positionBase.transform.position.y + Random.Range(-4, 4), 0) - new Vector3(5, 0, 0);
            }
        }
       else
        {
            for (int i = 0; i < countWall; i++)
            {
                if (position[i] != Vector3.zero)
                {
                    position[i] = Vector3.zero;
                }
            }
        }
    }

    /*
     * проверяет не вышли ли стены на грань поля боя
     */

    void FieldWall(int i)
    {
        if (Wall[i].transform.position.y >= limit1.position.y)
        {
            Wall[i].transform.position = new Vector3(Wall[i].transform.position.x, limit1.position.y, Wall[i].transform.position.z);  
        }
        else
        if (Wall[i].transform.position.y <= limit2.position.y)
        {
            Wall[i].transform.position = new Vector3(Wall[i].transform.position.x, limit2.position.y, Wall[i].transform.position.z);
        }
    }


    /*
     * изменение тега стен для буста
     */
    void ChangeTag(bool boost, int i)
    {
        if(boost && Wall[i].tag == tagEnemy)
        {
            Wall[i].tag = tagPlayer;
            Wall[i].gameObject.transform.GetChild(0).tag = tagPlayer;
            Wall[i].gameObject.transform.GetChild(1).tag = tagPlayer;
            Wall[i].gameObject.transform.GetChild(2).tag = tagPlayer;

         

        }
        else
        {
            Wall[i].tag = tagEnemy;
            Wall[i].gameObject.transform.GetChild(0).tag = tagEnemy;
            Wall[i].gameObject.transform.GetChild(1).tag = tagEnemy;
            Wall[i].gameObject.transform.GetChild(2).tag = tagEnemy;
        }
    }


    //счет существуеших стен
    public float CountWall()
    {
        float percent = 0;
        int count = 0;
        for (int i=0; i < countWall; i++)
        {
            if(Wall[i] != null)
            {
                count++; 
            }   
        }
        if(count == 0)
        {
            return percent = PlayerPrefs.GetFloat("Field") / PlayerPrefs.GetFloat("Field");
        }
        else
            return percent = Mathf.Round(lengthWall * count/ PlayerPrefs.GetFloat("Field")*100);

    }

    //удаление лишних стен
    public void LengthCount()
    {
        for(int i = 0; i < countWall; i++)
        {
            if(Wall[i] != null && booster.destroyWall > 0)
            {
                booster.destroyWall--;
                Destroy(Wall[i]);
            }
        }
        //float addLength = Mathf.Round((2 * PlayerPrefs.GetFloat("LengthWallBot") / PlayerPrefs.GetFloat("Field"))*100)/100;
        //return addLength;
    }

    void StealBoost()
    {
        if(CountWall() >= 40 && booster.steal)
        {
            LengthCount();
            //Debug.Log("Destroy");
        }
    }
}
