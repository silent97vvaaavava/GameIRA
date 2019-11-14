using UnityEngine;
using UnityEngine.UI;

public class NumberOfBooster : MonoBehaviour
{
    public Text[] numberOfBooster = new Text[9];
    public GameObject[] boostName = new GameObject[9];

    [SerializeField] GameObject win;
    [SerializeField] GameObject lose;
    [SerializeField] Image boostWin;
    [SerializeField] GameObject boostLose;
    [SerializeField] Text moreLose;



    int losecount;


    // scripts
    //[SerializeField] InventoryInformation key;

    private void Start()
    {
        if (!PlayerPrefs.HasKey(ConstantsList.keyGetBoost))
        {
            PlayerPrefs.SetInt(ConstantsList.keyGetBoost, 3);
        }
        else
            losecount = PlayerPrefs.GetInt(ConstantsList.keyGetBoost);




        for(int i = 0; i < numberOfBooster.Length; i++)
        {
            if(PlayerPrefs.HasKey(boostName[i].name))
            {
                numberOfBooster[i].text = "";
                numberOfBooster[i].text += PlayerPrefs.GetInt(boostName[i].name);
            }
        }


    }

    private void Update()
    {
        for (int i = 0; i < numberOfBooster.Length; i++)
        {
            if (PlayerPrefs.HasKey(boostName[i].name) && PlayerPrefs.GetInt(boostName[i].name) > 0)
            {
                numberOfBooster[i].text = "";
                numberOfBooster[i].text += PlayerPrefs.GetInt(boostName[i].name);
            }
        }

        GetRandomBoost();
    }


    // Random Boost

    public void GetRandomBoost()
    {
        if(win.activeSelf && !boostWin.sprite)
        {
            int m = Random.Range(0, boostName.Length);
            boostWin.sprite = boostName[m].GetComponent<Image>().sprite;
            PlayerPrefs.SetInt(boostName[m].name, PlayerPrefs.GetInt(boostName[m].name) + 1);
            //Debug.Log(PlayerPrefs.GetInt(boostName[m].name));
        }
        else 
            if(lose.activeSelf && losecount > 0 && boostLose.activeSelf)
            {
            boostLose.SetActive(false);
            moreLose.text = "..." + losecount + " more loses to recieve bonus.";
            PlayerPrefs.SetInt(ConstantsList.keyGetBoost, PlayerPrefs.GetInt(ConstantsList.keyGetBoost) - 1);
            }
        else
        if (!boostLose.GetComponent<Image>().sprite && losecount == 0)
        {
            boostLose.SetActive(true);
            int m = Random.Range(0, boostName.Length);
            moreLose.text = "...and receive bonus:";
            boostLose.GetComponent<Image>().sprite = boostName[m].GetComponent<Image>().sprite;
            PlayerPrefs.SetInt(boostName[m].name, PlayerPrefs.GetInt(boostName[m].name) + 1);
            PlayerPrefs.SetInt(ConstantsList.keyGetBoost, 3);
            //Debug.Log(PlayerPrefs.GetInt(boostName[m].name));

        }


    }

}
