using UnityEngine;

public class ShotBulletPL : MonoBehaviour
{
    public Rigidbody2D bulletP;
    public Transform turPoint;
    [SerializeField] Transform ammunitionBooster;

    public float speed;

    public TimerMain timeR;
    float timeWait;
    [HideInInspector] public Rigidbody2D clone;
    [SerializeField] private DirectionGun Line;
    [SerializeField] private GameObject Turret;

    //scripts
    [SerializeField] ChoiceBooster booster;
    [SerializeField] private Observer countBullet;
    public int numBullet = 0;

    private void FixedUpdate()
    {

        if (timeR.timeRound <= 0 &&  clone == null)
        {
            Line.ResetDirection();
            //Turret.transform.rotation = new Quaternion(0, 0, 0, 0);
            if (numBullet == 0 && !booster.ammunitionReady)
            { 
                Fire(turPoint);
            }
            else 
                if(numBullet == 0 && booster.ammunitionReady)
            {
                Fire(turPoint);
                Fire(ammunitionBooster);
            }
        }
    }


    void Fire(Transform turret) 
    {
        clone = Instantiate(bulletP, turret.position, Quaternion.identity) as Rigidbody2D;
        clone.name = "BulletPl";
        clone.velocity = transform.TransformDirection(turret.right * speed);
        clone.transform.right = turret.right;
        numBullet++;
        //Debug.Log("ShotBulletPL");

    }

}
