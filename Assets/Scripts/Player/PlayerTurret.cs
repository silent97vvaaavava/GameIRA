using UnityEngine;
using UnityEngine.EventSystems;

/*
 * Player
 * определяет можно ли ворочать башню
 */ 


public class PlayerTurret : MonoBehaviour, IDragHandler
{
    [SerializeField] GameObject arrow;
    [HideInInspector] public GameObject turretDir;

    //scripts
    public PlayerShot PlayFire;
    [SerializeField]  RotaterDirectionLine Line;
    [SerializeField]  MovingAimArrow MoveArrow;
    [SerializeField] ChoiceBooster booster;

    [SerializeField] Transform gunTurret;
    [SerializeField] Transform ammunitionBooster;
    [SerializeField] GameObject directionArrow;



     

    private void Start()
    {
        if(arrow==null)
        {
            arrow = GameObject.Find("Arrow");
        }

        if(turretDir == null)
        {
            turretDir = GameObject.Find("gunTurret");
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (arrow.activeSelf == false && !booster.ammunition)
        {
            PlayFire.SetRotation(gunTurret);
            Line.SetRotationDir();
            MoveArrow.Moving();
        }
        else
        {
            PlayFire.SetRotation(ammunitionBooster);
           // directionArrow.SetActive(true);
            Line.SetRotationDir();
            MoveArrow.Moving();
        }
    }

    //void AddArrow()
    //{

    //}
}
