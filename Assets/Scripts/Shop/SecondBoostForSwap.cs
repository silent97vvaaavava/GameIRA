using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SecondBoostForSwap : MonoBehaviour
{
    [SerializeField] GameObject acceptSwap;
    [SerializeField] GameObject cancelSwap;
    [SerializeField] GameObject text;
    [SerializeField] GameObject boostSecond;
    [SerializeField] Text count;

    [HideInInspector] public GameObject Second;

    //sripts
    [SerializeField] InventoryInformation Boost;
   


    public void ClickBoost(GameObject boost)
    {
        if(Boost.choosedBoost != null && Boost.firstBoost != boost)
        {
            text.SetActive(false);
            boostSecond.SetActive(true);
            boostSecond.GetComponent<Image>().sprite = boost.GetComponent<Image>().sprite;
            count.text = boost.transform.GetChild(1).transform.GetChild(0).GetComponent<Text>().text;
            Second = boost;
        }
       // Debug.Log("swap");

    }

    
}
