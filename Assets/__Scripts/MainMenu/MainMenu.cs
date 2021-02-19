using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/* MainCamera
 * следит за переходами между сценами 
 * отвечает за отображение кнопок
 */ 


public class MainMenu : MonoBehaviour
{

    [Header("Set in Inspector")]
    [SerializeField] GameObject goHome;
    //[SerializeField] GameObject onlineMode;
    [SerializeField] GameObject loseWindow;
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject drawWindow;





    private void LateUpdate()
    {
        ShowResultFight();
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


    
   


    public void ShowLoseWindowExit()
    {
        goHome.SetActive(true);
    }

    public void HideLoseWindowExit() 
    {
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


    //public void OnlineMode()
    //{
    //    if(!onlineMode.activeSelf)
    //    {
    //        onlineMode.SetActive(true);
    //    }
    //    else
    //        onlineMode.SetActive(false);

    //}

    // конец боя
    void ShowResultFight()
    {
        if(_Hero.bullet == null && _Enemy.bullet == null)
        {
            switch (ChangeHealth.numberWindow)
            {
                case 1: // если все жизни игрока уничтожены 
                    _Timer.ENDFIGHT = true;
                    loseWindow.SetActive(true);
                    PlayerPrefs.SetInt(loseWindow.name, PlayerPrefs.GetInt(loseWindow.name) + 1);
                    break;
                case 2: // все жизни врага
                    _Timer.ENDFIGHT = true;
                    winWindow.SetActive(true);
                    PlayerPrefs.SetInt(winWindow.name, PlayerPrefs.GetInt(winWindow.name) + 1);
                    break;
                case 3: // и врага и игрока
                    _Timer.ENDFIGHT = true;
                    drawWindow.SetActive(true);
                    PlayerPrefs.SetInt(drawWindow.name, PlayerPrefs.GetInt(drawWindow.name) + 1);
                    break;
            }
        }

        Debug.Log(ChangeHealth.numberWindow);
    }

}
