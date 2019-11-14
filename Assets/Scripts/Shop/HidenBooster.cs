using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HidenBooster : MonoBehaviour
{
    [SerializeField] GameObject hiden;
    [SerializeField] GameObject boost;
    [SerializeField] GameObject swapMenu;
    [SerializeField] GameObject cancelSwap;


    [SerializeField] InventoryInformation firstBoost;

    //private void Start()
    //{
    //    this.enabled = false;
    //}

    void Update()
    {
        var count = int.Parse(this.GetComponent<Text>().text);
        if(count < 2 && !swapMenu.activeSelf)
        {
            this.enabled = true;
            boost.GetComponent<Button>().enabled = false;
            hiden.SetActive(true);
        }
        else
        if((count >=2 && firstBoost.firstBoost != boost) || !cancelSwap.activeSelf)
        {
            hiden.SetActive(false);
            boost.GetComponent<Button>().enabled = true;
        }

        //if(firstBoost.firstBoost != null && firstBoost.firstBoost == boost.gameObject)
        //{
        //    boost.GetComponent<Button>().enabled = false;
        //    hiden.SetActive(true);
        //} else
        //{
        //    hiden.SetActive(false);
        //    boost.GetComponent<Button>().enabled = true;
        //}
    }

    public void Hiden()
    {
        hiden.SetActive(false);
    } 
}
