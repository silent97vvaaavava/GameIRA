using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInformation : MonoBehaviour
{
    public Text[] boosters = new Text[9];
    public string[] keyBooster = new string[9];
    public Sprite[] imageInventory = new Sprite[9];
    public string[] textInventory = new string[9];

    [SerializeField] Text information;
    [SerializeField] Image icon;
    [SerializeField] GameObject more;
    [SerializeField] Text count;


    [HideInInspector] public GameObject choosedBoost;
    [HideInInspector] public GameObject firstBoost;


    [HideInInspector] public GameObject First;

    private void Start()
    {
        for(int i = 0; i < keyBooster.Length; i++)
        {
            //if (PlayerPrefs.HasKey(keyBooster[i]))
            //{
            //    PlayerPrefs.DeleteAll();
            //}
            //Debug.Log(PlayerPrefs.GetInt(keyBooster[i]));
            if (!PlayerPrefs.HasKey(keyBooster[i]))
            {
                PlayerPrefs.SetInt(keyBooster[i], int.Parse(boosters[i].text));    
            }
            else
            {
                    //boosters[i].text = "";
                    boosters[i].text = PlayerPrefs.GetInt(keyBooster[i]).ToString();
            }

        }
    }

    public void ShowInformationBaff(GameObject value)
    {
        if (choosedBoost == null)
        {
            int num = int.Parse(FindNum(value.name));
            more.SetActive(true);
            information.text = textInventory[num];
            icon.sprite = imageInventory[num];
            count.text = PlayerPrefs.GetInt(FindName(value)).ToString();
            //Debug.Log(imageInventory[num]);
            //Debug.Log(value.name);       
            firstBoost = value;
            First = value;
        }


    }


    public void HidenInformationBaff()
    {
        more.SetActive(false);
    }

    string FindNum(string nameBoost)
    {
        string s = nameBoost;
        string pattern = @"\D*";
        string target = "";
        Regex regex = new Regex(pattern);
        string result = regex.Replace(s, target);
        return result;
    }
   
    public string FindName(GameObject nameBoost)
    {
        string s = nameBoost.name;
        string pattern = @"\W+\s.";
        string target = "";
        Regex regex = new Regex(pattern);
        string result = regex.Replace(s, target);
        return result;
    }
   

    string CreateNameBooster(string name, int num)
    {
        string boostName = name + ": " + num;
        return boostName;
    }


    public void NullChangedBoost()
    {
        choosedBoost = null;
    }


    public void ResetCountBoost()
    {
        for(int i =0; i < keyBooster.Length; i++)
        {
            boosters[i].text = PlayerPrefs.GetInt(keyBooster[i]).ToString();
        }
    }
}
