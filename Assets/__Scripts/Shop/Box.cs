using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Box : MonoBehaviour
{
    [SerializeField] GameObject FogTap;

    [SerializeField] GameObject openBox;
    [SerializeField] GameObject buyBox;
    [SerializeField] GameObject animationBox;

    // animation
    [SerializeField] GameObject animationBoxPlay;
    [SerializeField] GameObject lockBox;

    [SerializeField] Text count;


    // variable
    GameObject anim;

    private void Start()
    {
        if(!PlayerPrefs.HasKey(ConstantsList.keyBox))
        {
            PlayerPrefs.SetInt(ConstantsList.keyBox, int.Parse(count.text));
        }
        else
        {
            count.text = PlayerPrefs.GetInt(ConstantsList.keyBox).ToString();
        }
    }


    public void AlertBox()
    {
        FogTap.SetActive(true);
        if (PlayerPrefs.HasKey(ConstantsList.keyBox) && PlayerPrefs.GetInt(ConstantsList.keyBox) > 0)
        {
            openBox.SetActive(true);
            PlayerPrefs.SetInt(ConstantsList.keyBox, PlayerPrefs.GetInt(ConstantsList.keyBox) - 1);
            count.text = PlayerPrefs.GetInt(ConstantsList.keyBox).ToString();
        }
        else
        if(PlayerPrefs.GetInt(ConstantsList.keyBox) == 0)
        {
            buyBox.SetActive(true);
        }
    }

    public void OpenBox(RectTransform parent)
    {
        lockBox.SetActive(false);
        animationBoxPlay.SetActive(true);
        //if (anim != null)
        //Destroy(anim.gameObject, 2.45f);
    }


    public void HideLoot(GameObject loot)
    {
        FogTap.SetActive(false);
        loot.SetActive(false);
        lockBox.SetActive(true);
    }

    public void HideOpenBox(GameObject loot)
    {
        
        loot.SetActive(false);
        lockBox.SetActive(true);
    }


    public void ResetCountBox()
    {
        count.text = PlayerPrefs.GetInt(ConstantsList.keyBox).ToString();
    }
}
