using UnityEngine;


/* MainCamera
 * Задает случайное положение врага 
 * выстрел в случайном направлении
 */

public class FireEnemy : MonoBehaviour
{
    public Rigidbody2D bulletE;
    public Transform turPoint;
    public float speed;
    public TimerMain timeR;
    float timeWait;
    [HideInInspector] public Rigidbody2D clone;
    public Observer existsEnemy;
    public GameObject positionEnemy;
    [SerializeField] private RectTransform limitUp;
    [SerializeField] private RectTransform limitDown;

    [SerializeField] ChoiceBooster boost;

    public int countVisible = 0;
    public int count = 0;
    Vector3 limitUpPos;
    Vector3 limitDownPos;


    [HideInInspector] public Vector3 positionRandom = Vector3.zero;
    //public int numBullet = 0;

    private void Start()
    {
        limitUpPos = Camera.main.ScreenToWorldPoint(limitUp.position);
        limitDownPos = Camera.main.ScreenToWorldPoint(limitDown.position);
    }

    private void Update()
    {
        limitUpPos = Camera.main.ScreenToWorldPoint(limitUp.position);
        limitDownPos = Camera.main.ScreenToWorldPoint(limitDown.position);
        //Debug.Log(positionEnemy.transform.position);

        
    }

    private void FixedUpdate()
    {
        if (((timeR.timeRound <= 0 && countVisible == 0) || positionEnemy.activeSelf) && positionRandom == Vector3.zero)
        {
            positionRandom = new Vector3(positionEnemy.transform.position.x, Random.Range(-8f, 8f), positionEnemy.transform.position.z);
            SetEnemyPosition(positionRandom);
            //Debug.Log(positionRandom);
        }

        if (timeR.timeRound <= 0 && count == 0 && clone == null)
        {
            //Debug.Log("Fire");
            //SetEnemyPosition(positionRandom);
            if(!boost.strike)
            Fire(positionRandom);
            

        }

    }

    /*
     * выстрел врага
     */ 
    void Fire(Vector3 position) // найти куда ее я всунул!!!
    {
        clone = Instantiate(bulletE, StartPosition(position), Quaternion.identity) as Rigidbody2D;
        clone.name = "BulletE";
        clone.velocity = new Vector3(-1*speed, Random.Range(-6f, 6f), -1);
        clone.transform.right = turPoint.right;
        count++;
    }

    /*
     * положение базы врага 
     */
     void SetEnemyPosition(Vector3 position)
    {
        
        positionEnemy.SetActive(true);
        positionEnemy.transform.position = position;
        //Debug.Log(position);
        positionEnemy.transform.position = StartPosition(position);
        //if (positionEnemy.transform.position.y >= limitUpPos.y) //  ограничение передвежения по полю
        //{
        //    positionEnemy.transform.position = new Vector3(positionEnemy.transform.position.x, limitUpPos.y, positionEnemy.transform.position.z);
        //}
        //if (positionEnemy.transform.position.y <= limitDownPos.y)
        //{
        //    positionEnemy.transform.position = new Vector3(positionEnemy.transform.position.x, limitDownPos.y, positionEnemy.transform.position.z);
        //}
        countVisible++;
    }

    Vector3 StartPosition(Vector3 position)
    {
        Vector3 startPosition = Vector3.zero;
        if (position.y >= limitUpPos.y) //  ограничение передвежения по полю
        {
           return startPosition = new Vector3(positionEnemy.transform.position.x, limitUpPos.y, positionEnemy.transform.position.z);
        }
        else
        if (position.y <= limitDownPos.y)
        {
            return startPosition = new Vector3(positionEnemy.transform.position.x, limitDownPos.y, positionEnemy.transform.position.z);
        }
        else
        {
            return startPosition = new Vector3(positionEnemy.transform.position.x, position.y, positionEnemy.transform.position.z);
        }
    }
    
}
