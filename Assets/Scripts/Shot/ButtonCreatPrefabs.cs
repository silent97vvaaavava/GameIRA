using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* MainCamera
 * создание направляющей
 * пробник УДАЛИТЬ
 */ 


public class ButtonCreatPrefabs : MonoBehaviour
{
    public GameObject myPref;
    int n = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var arr =  Instantiate(myPref);
            arr.name = "Arrow" + n;
            n++;
        } 
    }
}
