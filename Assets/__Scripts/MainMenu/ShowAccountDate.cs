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


}
