using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogScroll : MonoBehaviour
{
    public Text logScroll;
    public Slider scroll;
    string value = "value: ";

    void Start()
    {
        
        logScroll.text +=  scroll.value; 
    }

    // Update is called once per frame
    void Update()
    {
        var number = Mathf.Round(scroll.value * 100)/100;
        logScroll.text = value + number;
      //  Debug.Log(logScroll.text);
    }
}
/*
     * функция включающая стрелки движения
     */




//void ActiveDrag()
//{
//    distance = Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

//    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
//    if (hit.transform && hit.transform.name == "Player" && Input.GetMouseButtonDown(0))
//    {
//        if (count == 0 && distance <= 0.2f)
//        {
//            arrow.SetActive(true);
//            if (arrowD.activeSelf == false)
//            {
//                arrowD.SetActive(true);
//            }
//            if (arrowU.activeSelf == false)
//            {
//                arrowU.SetActive(true);
//            }
//            count++;
//        }
//        else
//        if (count == 1)
//        {
//            count++;
//        }
//    }
//    else
//        if (Input.GetMouseButtonUp(0) && count == 2)
//    {
//        arrow.SetActive(false);
//        count = 0;
//    }
//    Debug.Log(count);
//}

///*
// * включает тень преследования
// */
//void ActiveShadow()
//{
//    if (count == 1)
//    {
//        positionShadow = new Vector2(shadow.transform.position.x, this.transform.position.y);
//    }
//    if (count == 2)
//    {
//        shadow.transform.position = positionShadow;
//        shadow.SetActive(true);
//    }
//    else
//        shadow.SetActive(false);
//}




//public void OnPointerClick(PointerEventData eventData)
//{
//    Debug.Log(eventData.pointerPressRaycast.gameObject.name);
//    if (count == 0) 
//    {
//        arrow.SetActive(true);
//        turret.zRotate.rotation = Quaternion.Euler(0,0,0);
//        positionShadow = new Vector2(shadow.transform.position.x, this.transform.position.y);
//        shadow.SetActive(true);
//        count++;
//    }
//    else

//    {
//        arrow.SetActive(false);
//        shadow.SetActive(false);
//        count = 0;
//    }
//    shadow.transform.position = positionShadow;
//}