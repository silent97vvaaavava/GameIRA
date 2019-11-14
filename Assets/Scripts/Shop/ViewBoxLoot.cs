using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ViewBoxLoot : MonoBehaviour
{
    [SerializeField] string[] boostKey = new string[10];
    [SerializeField] Sprite[] boostImage = new Sprite[10];
    [SerializeField] Image[] boostView = new Image[5];
    [SerializeField] int[] numBoost = new int[5];

    public void RandomLoot()
    {
        for(int i = 0; i < boostView.Length; i++)
        {
            for(int j =0; j < numBoost.Length; j++)
            {
                var num = Random.Range(0, 10);
                if(num != numBoost[j])
                {
                    numBoost[j] = num;
                }
            }
            Debug.Log(numBoost[i]);
            //boostView[i].sprite = boostImage[numBoost[i]];
        }


    }
}
