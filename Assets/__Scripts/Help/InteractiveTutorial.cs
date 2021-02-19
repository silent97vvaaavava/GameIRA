using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveTutorial : MonoBehaviour
{
    [SerializeField] GameObject[] step;


    void Start()
    {
        if (PlayerPrefs.HasKey(ConstantsList.keyTutorial) && PlayerPrefs.GetString(ConstantsList.keyTutorial) == "true")
        {
            step[0].SetActive(true);
            for (int i = 1; i < step.Length; i++)
            {
                step[i].SetActive(false);
            }
        }
    }



    public void stepFirst()
    {
        if (step[0].activeSelf)
        {
            step[0].SetActive(false);
            step[1].SetActive(true);
        }
    }

    public void stepSecond()
    {
        if (step[1].activeSelf)
        {
            step[1].SetActive(false);
            step[2].SetActive(true);
        }
    }


    public void StepThree()
    {
        if (step[2].activeSelf)
        {
            step[2].SetActive(false);
            step[3].SetActive(true);
        }
    }

    public void StepFour()
    {
        if (step[3].activeSelf)
        {
            step[3].SetActive(false);
            step[4].SetActive(true);
        }
    }

    public void StepFive()
    {
        if (step[4].activeSelf)
        {
            step[4].SetActive(false);
            step[5].SetActive(true);
        }
    }

    public void StepSix()
    {
        if (step[5].activeSelf)
        {
            step[5].SetActive(false);
            step[6].SetActive(true);
        }
    }

    public void StepSeven()
    {
        if (step[6].activeSelf)
        {
            step[6].SetActive(false);
            step[7].SetActive(true);
        }
    }

    public void StepEight()
    {
        if (step[7].activeSelf)
        {
            step[7].SetActive(false);
            step[8].SetActive(true);
        }
    }

    public void StepNine()
    {
        if (step[8].activeSelf)
        {
            step[8].SetActive(false);
           
        }

        if(PlayerPrefs.HasKey(ConstantsList.keyTutorial))
        {
            PlayerPrefs.SetString(ConstantsList.keyTutorial, "false");
        }
    }

    public void skipTutorial()
    {
        if(PlayerPrefs.HasKey(ConstantsList.keyTutorial))
        {
            PlayerPrefs.SetString(ConstantsList.keyTutorial, "false");
            for(int i = 0; i < step.Length; i++)
            {
                if(step[i].activeSelf)
                {
                    step[i].SetActive(false);
                    break;
                }
            }
        }
    }
}
