using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SelectAvatar : MonoBehaviour
{
    public Sprite[] avatar = new Sprite[4];
    int numAvatar = 0;
    [SerializeField] Image avatarPlayer;
    // Start is called before the first frame update

    

    private void Start()
    {
        if(PlayerPrefs.HasKey(ConstantsList.keyAvatar))
        avatarPlayer.sprite = avatar[PlayerPrefs.GetInt(ConstantsList.keyAvatar)];
    }

    public void NextAvatarImage()
    {
        if (numAvatar < 3)
        {
            numAvatar++;
            avatarPlayer.sprite = avatar[numAvatar];
        }
    }

    public void BackAvatarImage()
    {
        if (numAvatar > 0 )
        {
            numAvatar--;
            avatarPlayer.sprite = avatar[numAvatar];
        }
    }


    public void WriteNumAvatars()
    {
        PlayerPrefs.SetInt(ConstantsList.keyAvatar, numAvatar);
        //Debug.Log(PlayerPrefs.GetInt(keyAvatar));
    }
}
