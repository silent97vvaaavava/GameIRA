using UnityEngine;

/* AimMask
 * рисование стрелок направления
 */


public class DirectionShot : MonoBehaviour
{
    public GameObject ArrowDr;
    int count = 0;
    public Transform startOb;
    public Rigidbody2D anchorTur;
    [SerializeField] private GameObject arrowPl;
    [SerializeField] private GameObject PrefArr;
    [SerializeField] private Transform Target;
    private GameObject folderAr;
    [SerializeField] private MainMenu ResetFightSet;
    public TimerMain timerR;

    // созадние объекта для хранения стрелок
    private void Start()
    {
        folderAr = Instantiate(PrefArr) as GameObject;
        folderAr.name = "PrefArrow";
        folderAr.transform.SetParent(Target, false);
    }


    private void Update()
    {
       if (arrowPl.activeSelf || ResetFightSet.cancelFight || timerR.resetTur)
        {
            Destroy(GameObject.Find("PrefArrow"));
            count = 0;
            timerR.resetTur = false;
        }
        else if(folderAr == null)
        {
            folderAr = Instantiate(PrefArr) as GameObject;
            folderAr.name = "PrefArrow";
            folderAr.transform.SetParent(Target, false);
        }
        if (folderAr != null)
        {
            startOb = GameObject.Find("PrefArrow").GetComponent<Transform>();
        }
    }

    // создание после покидания предыдущего объекта
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "AimArrow" && count < 8 && transform.localPosition.x >= collision.gameObject.transform.localPosition.x && arrowPl.activeSelf==false)
        {
            //count++;
            //CreatArrowAim(count);

        } else
            if(transform.localPosition.x <= collision.gameObject.transform.localPosition.x && collision.name != "ArrowD")
        {
            //count--;
            //Destroy(collision.gameObject);
        }
    }



    /*Создание стрелок
     * ДОБАВИТЬ ФИКСИРОВАННОЕ РАССТОЯНИЕ
     */ 

    void CreatArrowAim(int count)
    {
            var arrow = Instantiate(ArrowDr);
            arrow.transform.SetParent(startOb, false);
            arrow.transform.position = GameObject.Find("AimMask").transform.position;
            arrow.name = "Arrow" + count;
            arrow.GetComponent<FixedJoint2D>().connectedBody = anchorTur;
      
    }



}
