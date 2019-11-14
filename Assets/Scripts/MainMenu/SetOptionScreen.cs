using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOptionScreen : MonoBehaviour
{
    
    // GameObjects
    [SerializeField] GameObject top;
    [SerializeField] GameObject bottom;
    [SerializeField] GameObject wallStandart;

    [SerializeField] GameObject L1;
    [SerializeField] GameObject L2;


    void Start()
    {
        if(!PlayerPrefs.HasKey(ConstantsList.keyTutorial))
        {
            PlayerPrefs.SetString(ConstantsList.keyTutorial, "true");
        }

        if(!PlayerPrefs.HasKey(ConstantsList.keyWallBot))
        {
            float length = Vector2.Distance(top.transform.position, bottom.transform.position);
            float delta = top.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            length += delta;
            length = Mathf.Round(length * 100) / 100;
            PlayerPrefs.SetFloat(ConstantsList.keyWallBot, length);
            wallStandart.SetActive(false);
        }
        else
        {
            Debug.Log(PlayerPrefs.GetFloat(ConstantsList.keyWallBot));
            wallStandart.SetActive(false);
        }

        if(!PlayerPrefs.HasKey(ConstantsList.keyField))
        {
            var lengthField = Vector2.Distance(L1.transform.position, L2.transform.position);
            var delta = L1.GetComponent<BoxCollider2D>().bounds.size.y / 2 + L2.GetComponent<BoxCollider2D>().bounds.size.y / 2;
            lengthField += delta;
            lengthField = Mathf.Round(lengthField *100) / 100;

            PlayerPrefs.SetFloat(ConstantsList.keyField, lengthField);
        }
        else
        {
            Debug.Log(PlayerPrefs.GetFloat(ConstantsList.keyField));
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
