using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalMenu : MonoBehaviour
{
    [SerializeField] GameObject localMenu;
    [SerializeField] Image avatarIcon;
    [SerializeField] Text nickname;
    [SerializeField] SelectAvatar avatarImage;
    [SerializeField] Text countWin;
    [SerializeField] Text counLose;

    private void Start()
    {
        avatarIcon.sprite = avatarImage.avatar[PlayerPrefs.GetInt(ConstantsList.keyAvatar)];
        nickname.text = PlayerPrefs.GetString(ConstantsList.keyName);
        counLose.text = ""+ PlayerPrefs.GetInt(ConstantsList.keyLose);
        countWin.text = "" +PlayerPrefs.GetInt(ConstantsList.keyWin);

    }

   
    public void LocalMenuEnable()
    {
        localMenu.SetActive(true);
        if(PlayerPrefs.HasKey("Name"))
        {
            avatarIcon.sprite = avatarImage.avatar[PlayerPrefs.GetInt(ConstantsList.keyAvatar)];
            nickname.text = PlayerPrefs.GetString(ConstantsList.keyName);
            counLose.text = "" + PlayerPrefs.GetInt(ConstantsList.keyLose);
            countWin.text = "" + PlayerPrefs.GetInt(ConstantsList.keyWin);
        }
    }

    public void LocalMenuDisable()
    {
        localMenu.SetActive(false);
    }

    
}
