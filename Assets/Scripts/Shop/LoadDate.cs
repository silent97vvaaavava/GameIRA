using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadDate : MonoBehaviour
{
    [SerializeField] Text namePlayer;
    [SerializeField] Text winCount;
    [SerializeField] Text loseCount;
    
    //[SerializeField] Text nameBooster;

    //scripts
    [SerializeField] SelectAvatar spriteAvatar;

    private void Start()
    {
        namePlayer.text = PlayerPrefs.GetString(ConstantsList.keyName);
        winCount.text = PlayerPrefs.GetInt(ConstantsList.keyWin).ToString();
        loseCount.text = PlayerPrefs.GetInt(ConstantsList.keyLose).ToString();
        

    }
}
