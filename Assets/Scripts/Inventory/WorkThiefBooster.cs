using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkThiefBooster : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] WallBot count;

    void Update()
    {
        if(slider.value*100 < 80 && count.CountWall() >= 40)
        {
            gameObject.GetComponent<Button>().enabled = true;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            //Debug.Log(count.CountWall());

        }
        else
        {
            gameObject.GetComponent<Button>().enabled = false;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            //Debug.Log(count.CountWall());
        }
    }



}
