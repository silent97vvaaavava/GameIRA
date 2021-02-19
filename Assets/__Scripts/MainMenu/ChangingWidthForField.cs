using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ChangingWidthForField : MonoBehaviour
{
    [Header("Set in Inspector")]
    [SerializeField] GameObject field;


    private float widthStart;


    private void Awake()
    {
        widthStart = 18f/9f * Camera.main.orthographicSize;
    }

    private void Update()
    {
        WidthAspect();
    
    }

    // высчитвает отношение дефолтного размера и текущего 
    void WidthAspect()
    {

        var widthNow = Camera.main.aspect * Camera.main.orthographicSize;
        var ratio = widthNow / widthStart;
        field.transform.localScale = new Vector3(ratio, 1, 1);
    }

}
