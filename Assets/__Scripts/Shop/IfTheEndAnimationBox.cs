using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class IfTheEndAnimationBox : MonoBehaviour
{
    [SerializeField] GameObject tap;
    [SerializeField] GameObject animationBox;


    public string[] boostKey = new string[10];
    public Sprite[] boostImage = new Sprite[10];
    //[SerializeField] Image[] boostView = new Image[5];
    [SerializeField] GameObject[] boostView = new GameObject[5];


    public void OpenBox()
    {
        tap.SetActive(false);
        animationBox.SetActive(true);
    }

   int[] numBoost = new int[5];

    public void RandomLoot()
    {
        System.Random rand = new System.Random((int)DateTime.Now.Ticks);
        


        for (int i = 0; i < boostView.Length; i++)
        {
           
                numBoost[i] = rand.Next(0, 10);
                boostView[i].SetActive(true);
                boostView[i].GetComponent<Image>().sprite = boostImage[numBoost[i]];
                int count = PlayerPrefs.GetInt(boostKey[numBoost[i]]) + 1;
                PlayerPrefs.SetInt(boostKey[numBoost[i]], count);
                //boostView[i].SetActive(false);
           


        }


    }

    public void HidenLootBoost(GameObject button)
    {
        button.SetActive(false);
        for (int i = 0; i < boostView.Length; i++)
        {
            
            boostView[i].SetActive(false);
        }
    }
  
}
