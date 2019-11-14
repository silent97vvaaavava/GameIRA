using UnityEngine;
using UnityEngine.UI;

public class ChoiceBooster : MonoBehaviour
{
    GameObject boost;
    GameObject booster;

    // игровые объекты
    [SerializeField] GameObject enemy;
    [SerializeField] Slider slider;

    // подключение сриптов для бустов
    [SerializeField] HealthPlayer heartAdd;
    [SerializeField] NumberOfBooster num;
    [SerializeField] WallBot add;

    //переменые бустов
    [HideInInspector] public bool shield = false;
    [HideInInspector] public bool strike = false;
    [HideInInspector] public bool misunderstood = false;
    [HideInInspector] public bool ammunition = false;
    [HideInInspector] public bool ammunitionReady = false;
    [HideInInspector] public float thief = 0;
    [HideInInspector] public int destroyWall = 0;
    [HideInInspector] public bool steal = false;

    [SerializeField] GameObject choiceBooster;

    public void ActiveBooster(GameObject back)
    {
        
            boost = back;
        //Debug.Log(boost);
    }

    


    public void UseBooster(GameObject value)
    {
        if (boost != null)
        {
            string key = boost.name;

            switch (key)
            {
                case "Healing":
                    RedCross(boost);
                    break;
                case "Shield":
                    Shield(boost);
                    break;
                case "Location":
                    Location(boost);
                    break;
                case "Strike":
                    Strike(boost);
                    break;
                case "Misunderstood":
                    Misunderstood(boost);
                    break;
                case "Ammunition":
                    Ammunition(boost);
                    break;
                case "Lottery":
                    Lottery(boost);
                    break;
                case "Thief":
                    Thief(boost);
                    break;
                case "Hypnosis":
                    Hypnosis(boost);
                    break;


            }
            value.SetActive(false);
        }
    }

    /*
     * бустер на красный крест
     * добавляет жизнь игроку
     */

    void RedCross(GameObject name)
    {
        if(heartAdd.countHealth < 2)
        {
            heartAdd.countHealth++;
        }
        SubtractNumBooster(name);
    }

    void Shield(GameObject name)
    {
        if(!shield)
        shield = true;
        SubtractNumBooster(name);
    }

    void Location(GameObject name)
    {
        if(!enemy.activeSelf)
        enemy.SetActive(true);
        SubtractNumBooster(name);
    }


    void Strike(GameObject name)
    {
        if(!strike)
        strike = true;
        //Debug.Log("Strike");
        SubtractNumBooster(name);
    }

   void Misunderstood(GameObject name)
    {
        if(!misunderstood)
        misunderstood = true;
        SubtractNumBooster(name);
        Debug.Log("Misunderstood");
    }


    void Ammunition(GameObject name)
    {
        if (!ammunition && !ammunitionReady)
        {
            Debug.Log("Ammunition");
            ammunition = true;
            ammunitionReady = true;
        }
        SubtractNumBooster(name);
        //Debug.Log("Ammunition");
    }

    void Lottery(GameObject name)
    {
        Debug.Log("Lottery");
        int count = Random.Range(0, 9);
        Debug.Log(count);
        var numberBoost = PlayerPrefs.GetInt(num.boostName[count].name)+1;
        PlayerPrefs.SetInt(num.boostName[count].name, numberBoost);

        SubtractNumBooster(name);
    }

    void Thief(GameObject name)
    {

        steal = true;
        thief = 2*PlayerPrefs.GetFloat("LengthWallBot"); 
        destroyWall = 2;
        //PlayerPrefs.SetInt("Thief", 3);
        SubtractNumBooster(name);
    }

    void Hypnosis(GameObject name)
    {
        Debug.Log("Hypnosis");
        SubtractNumBooster(name);
    }


    void SubtractNumBooster(GameObject name)
    {
        if (PlayerPrefs.GetInt(name.name) > 0)
        {
            var num = PlayerPrefs.GetInt(name.name) - 1;
            PlayerPrefs.SetInt(name.name, num);

            name.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text = "";
            name.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text += PlayerPrefs.GetInt(name.name);
        }
        else
        {
            PlayerPrefs.SetInt(name.name, 0);
            name.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text += PlayerPrefs.GetInt(name.name);
        }
    }

    public void ShowAlert()
    {
        if(boost != null && !choiceBooster.activeSelf)
        {
            choiceBooster.transform.GetChild(0).GetComponent<Text>().text = boost.name;
            choiceBooster.SetActive(true);
        }
    } 
}
