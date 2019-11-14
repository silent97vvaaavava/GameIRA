using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveData : MonoBehaviour
{
    public GameObject namePan;

    //public Save sv = new Save();
   

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if(!PlayerPrefs.HasKey(ConstantsList.keyName))
        {
            namePan.SetActive(true);
        }
        else
        {
            //sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("Name"));
            //Debug.Log(sv.name);
        }
    }


    public void CheckName(Text name)
    {
        if(!string.IsNullOrEmpty(name.text) && name.text.Length >= 3)
        {
            //sv.name = name;
            PlayerPrefs.SetString(ConstantsList.keyName, name.text);
            //Debug.Log("your name: " + PlayerPrefs.GetString(keyName));
            namePan.SetActive(false);
        }
        else
        {

        }
    }


//#if UNITY_ANDROID && !UNITY_EDITOR
//   private void OnApplicationPause(bool pause)
//    {
//        PlayerPrefs.SetString("Name", sv.name); 
//    }
//#endif
    //private void OnApplicationQuit()
    //{
    //    PlayerPrefs.SetString(keyName, sv.name);
    //}

    //[SerializeField] public class Save
    //{
    //    public string name;

    //    //public int win;
    //    //public int lose;



    //    //public int[] inventoryBaff = new int[9];
    //}


}
