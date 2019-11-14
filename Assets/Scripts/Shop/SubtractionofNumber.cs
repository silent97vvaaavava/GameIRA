using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SubtractionofNumber : MonoBehaviour
{
    [SerializeField] Text countFirst;
    [SerializeField] Text countSecond;


    int first;
    int second;

    // отнять и добавить ко второму 2/1
    public void TwoToOne()
    {
        first = int.Parse(countFirst.text);
        second = int.Parse(countSecond.text);

        if(second > 1)
        {
            first++;
            second -= 2;
            countFirst.text = first.ToString();
            countSecond.text = second.ToString();
        }

    }
}
