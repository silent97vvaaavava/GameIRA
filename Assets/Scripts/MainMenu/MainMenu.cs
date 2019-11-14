using UnityEngine;
using UnityEngine.SceneManagement;

/* MainCamera
 * следит за переходами между сценами 
 * отвечает за отображение кнопок
 */ 


public class MainMenu : MonoBehaviour
{

    [HideInInspector] public bool timerZero;
    [HideInInspector] public bool cancelFight;

    public GameObject ArrowPlayer;
    GameObject ArrowDir;
    [HideInInspector] public GameObject WallPlayer;
    [SerializeField] private GameObject Ready;
    [SerializeField] private GameObject Cancel;

    



    [SerializeField] private Transform directPosition;

    // scripts 
    [SerializeField] private DirectionGun ResetDirection;
    [SerializeField] CreateWall destroy;
    [SerializeField] ChoiceBooster booster;


    [SerializeField] GameObject goHome;

    [SerializeField] GameObject onlineMode;



    int goReady = 0;
    int goCancel = 0;

    private void Update()
    {
        if(ArrowDir == null)
        {
            ArrowDir = GameObject.Find("directionArrow: 0");
        }

        if (WallPlayer == null)
        {
            for (int i = 1; i <= 8; i++)
            {
                if (GameObject.Find("wall: " + i))
                    WallPlayer = GameObject.Find("wall: " + i); // придумать что-то другое
            }

        }
        //Debug.Log(WallPlayer);

        if (WallPlayer != null)
        {
            goCancel++;
        }
        else
            goCancel = 0;

        if (ArrowDir.GetComponent<SpriteRenderer>().color.a != 0)
        {
            goReady++;
        }
        else
        {
            goReady = 0;
        }

        if((goReady > 0 || goCancel > 0) && timerZero == false)
            Cancel.SetActive(true);
        else
            Cancel.SetActive(false);

        if (goReady > 0 && timerZero == false)
        {
            Ready.SetActive(true);

        }
        else
        {
            Ready.SetActive(false);
        }
    }



    public void PauseScene()
    {
        Invoke("LocalGameFight", .5f);
    }

    public void LocalGameFight()
    {
        
        SceneManager.LoadScene("Fight");
    }

    public void LocalGameMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void HelpMenuPause()
    {
        Invoke("HelpMenu", .5f);
    }

    public void HelpMenu()
    {
       
        SceneManager.LoadScene("HelpMenu");
    }


    public void EndOfTurn()
    {
        if (booster.ammunition)
        {
            booster.ammunition = false;
        }
        else
        timerZero = true;
        
    }

    public void ResetFight()
    {
        cancelFight = true;
        directPosition.position = new Vector3(directPosition.position.x, 0, 0);
        directPosition.rotation = new Quaternion(0, 0, 0, 0);
        ResetDirection.ResetDirection();
       //destroy.DestroyWall();
    }


    public void ShowLoseWindowExit()
    {
        goHome.SetActive(true);
        destroy.enabled = false;
    }

    public void HideLoseWindowExit() 
    {
        destroy.enabled = true;
        goHome.SetActive(false);
    }


    public void PauseShop()
    {
        Invoke("GoShop", .5f);
    }
    public void GoShop()
    {
        SceneManager.LoadScene("Shop");
    }


    public void OnlineMode()
    {
        if(!onlineMode.activeSelf)
        {
            onlineMode.SetActive(true);
        }
        else
            onlineMode.SetActive(false);

    }


}
