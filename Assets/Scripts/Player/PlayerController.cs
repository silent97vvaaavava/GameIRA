using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IPointerClickHandler, IDragHandler
{
    public GameObject aim;
    public GameObject arrow;
    GameObject aimArrow;
    GameObject turret;
    int count = 0;
    private Vector3 positionTurret;
    private Vector3 startPos;
    GameObject TurretDir;

    private void Start()
    {
        if(turret == null)
        {
            turret = GameObject.Find("Turret");
        }
        if (arrow == null)
        {
            arrow = GameObject.Find("Arrow");
        }
        arrow.SetActive(false);
        positionTurret = turret.transform.position;

        startPos = transform.position;
        if (TurretDir == null)
        {
            TurretDir = GameObject.Find("turretDir");
        }

    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (count < 1)
        {
            count++;
            arrow.SetActive(true);
            turret.transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        //else
        //{
        //    arrow.SetActive(false);
        //    count = 0;
        //}

        
    }

    private void OnMouseDrag()
    {
        if (arrow.activeSelf)
        {
            DragTank();
            
        }
    }



    public void DragTank()
    {
        Vector2 tankPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, tankPos.y,transform.position.z);
        if (Input.GetMouseButtonUp(0))
        {
            arrow.SetActive(false);
        }


    }



    public void OnDrag(PointerEventData eventData)
    {
        if (arrow.activeSelf == false)
        {
            Vector3 positionDrag = Camera.main.ScreenToWorldPoint(eventData.position);
            if (aimArrow == null)
            
            {
                aimArrow = Instantiate(aim);
                aimArrow.name = "AimMask";
                aimArrow.transform.SetParent(GameObject.Find("turretDir").transform, false);
            }
            aimArrow = GameObject.Find("AimMask");
            float positionAim = -Mathf.Sqrt(positionDrag.x * positionDrag.x + positionDrag.y * positionDrag.y);
            //print(positionAim);
            aimArrow.transform.position = new Vector3(positionDrag.x, turret.transform.position.y, 0);

            LookAtMouse();
            
        }
    }


    void LookBM()
    {
        if(Input.GetMouseButtonDown(0))
        {
            var mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10));
            var offset = mPos - startPos;
        }
    }


    void LookAtMouse()
    {
        var positionMouse =Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var angle = Vector2.Angle(Vector2.right, positionMouse - transform.position);
        TurretDir.transform.eulerAngles = new Vector3(0, 0, TurretDir.transform.position.y < positionMouse.y ? angle : -angle);
    }
}
