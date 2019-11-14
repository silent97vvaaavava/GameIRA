
using UnityEngine;

public class EditingBattlefield : MonoBehaviour
{
    public GameObject borderField1;
    public GameObject borderField2;

    public GameObject realL1;
    public GameObject realL2;

    public BoxCollider2D MarginBorder;

    float positionX1;

    float size1;
    float size2;

    float distance;

    private void Start()
    {
        size1 = realL1.GetComponent<BoxCollider2D>().bounds.size.y/2;
        size2 = realL2.GetComponent<BoxCollider2D>().bounds.size.y/2;
    }


    private void Update()
    {
        DistanceLimit();
        ChangedField();

    }


    /*устанавливает стенки отскока на нужном расстоянии
     */ 
    void DistanceLimit()
    {
        var position1 = Camera.main.ScreenToWorldPoint(borderField1.transform.position);
        var position2 = Camera.main.ScreenToWorldPoint(borderField2.transform.position);

        realL1.transform.position = new Vector3(realL1.transform.position.x, position1.y + size1, realL1.transform.position.z);
        realL2.transform.position = new Vector3(realL2.transform.position.x, position2.y - size2, realL2.transform.position.z);
    }

    /*
     * устанавливает размер поля для создания стен
     */
     
    void ChangedField()
    {
        distance = Vector2.Distance(realL1.transform.position, realL2.transform.position);
        MarginBorder.size = new Vector2(MarginBorder.size.x, distance - size1*4.8f);
        MarginBorder.offset = new Vector2(MarginBorder.offset.x,  - size1*1.1f);
    }
}

