using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class SwapButton : MonoBehaviour
{
    [SerializeField] GameObject cancel;
    [SerializeField] GameObject ok;
    [SerializeField] InventoryInformation inf;
    [SerializeField] GameObject boxSwap;
    [SerializeField] GameObject text;
    [SerializeField] GameObject boost;
    [SerializeField] GameObject swapButton;
    [SerializeField] GameObject textSwap;

    GameObject timeVue;

    public void CancelSwap(GameObject menu)
    {
        swapButton.SetActive(true);

        menu.SetActive(false);
        cancel.SetActive(false);
        ok.SetActive(false);
        boxSwap.SetActive(false);
        text.SetActive(true);
        boost.SetActive(false);

        

        inf.choosedBoost = null;

        if (timeVue != null)
        {
            timeVue.SetActive(false);
            timeVue = null;
        }

        if(inf.firstBoost != null)
        {
            inf.firstBoost = null;
        }

        if (textSwap.activeSelf)
        {
            textSwap.SetActive(false);
        }


    }


    public void TextSwap()
    {
        textSwap.SetActive(true);
    }
    

    public void StartSwap(GameObject swap)
    {
        inf.choosedBoost = swap;
        inf.firstBoost.GetComponent<Button>().enabled = false;
        swap.SetActive(true);
        cancel.SetActive(true);
        ok.SetActive(true);
        timeVue.SetActive(true);
        //Debug.Log(timeVue.transform.position);
        swapButton.SetActive(false);
    }

    public void PauseMenu()
    {
        Invoke("BackMainMenu", .5f);
    }


    public void BackMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HidenBoost(GameObject vue)
    {
        if (swapButton.activeSelf)
        {

            timeVue = vue;
            Debug.Log(timeVue.activeSelf);
        }
           
    }

    

    [SerializeField] Text countFirst;
    [SerializeField] Text countSecond;
    [SerializeField] SecondBoostForSwap keySecond;
    [SerializeField] InventoryInformation keyFirst;

    int first;
    int second;

    // отнять и добавить ко второму 2/1
    public void TwoToOne(GameObject value)
    {
        first = int.Parse(countFirst.text);
        second = int.Parse(countSecond.text);

       


        if (second > 1 && value.name == "Boost: 1")
        {
            first++;
            second -= 2;
            countFirst.text = first.ToString();
            countSecond.text = second.ToString();
        }
        else
        if(first > 1 && value.name == "Boost: 0" && second < PlayerPrefs.GetInt(keyFirst.FindName(keySecond.Second)))
        {
            second += 2;
            first--;
            countFirst.text = first.ToString();
            countSecond.text = second.ToString();
        }

    }

    

    public void Agree(GameObject menu)
    {
        PlayerPrefs.SetInt(keyFirst.FindName(keySecond.Second), second);
        PlayerPrefs.SetInt(keyFirst.FindName(keyFirst.First), first);
        keySecond.Second.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = second.ToString();
        keyFirst.First.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = first.ToString();

        swapButton.SetActive(true);

        menu.SetActive(false);
        cancel.SetActive(false);
        ok.SetActive(false);
        boxSwap.SetActive(false);
        text.SetActive(true);
        boost.SetActive(false);


        inf.choosedBoost = null;

        if (timeVue != null)
        {
            timeVue.SetActive(false);
            timeVue = null;
        }

        if (inf.firstBoost != null)
        {
            inf.firstBoost = null;
        }

        if(textSwap.activeSelf)
        {
            textSwap.SetActive(false);
        }
    }
}
