using UnityEngine;
using UnityEngine.UI;
public class ShowAccountDate : MonoBehaviour
{
    public Text namePlayer;
    int win;
    int lose;
   
    [SerializeField] GameObject winWindow;
    [SerializeField] GameObject loseWindow;
    [SerializeField] GameObject homeWindow;

    
    void Start()
    {
        namePlayer.text = PlayerPrefs.GetString("Name");
    }

    //private void Update()
    //{
    //    CountWinAndLose();
    //}

    public void CountWinAndLose()
    {
        if(winWindow.activeSelf)
        {
            win = PlayerPrefs.GetInt(ConstantsList.keyWin) + 1;
            PlayerPrefs.SetInt(ConstantsList.keyWin, win);
            
        }
        if(loseWindow.activeSelf || homeWindow.activeSelf)
        {
            lose = PlayerPrefs.GetInt(ConstantsList.keyLose) + 1;
            PlayerPrefs.SetInt(ConstantsList.keyLose, lose);
            
        }
        //Debug.Log(win);
        //Debug.Log(lose);

    }
}
