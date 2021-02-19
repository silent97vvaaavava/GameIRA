using UnityEngine;
using UnityEngine.UI;

public class WhenZeroNum : MonoBehaviour
{

    void Update()
    {
        if (PlayerPrefs.GetInt(gameObject.name) == 0)
        {
            gameObject.GetComponent<Button>().enabled = false;
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f); 
        }
        else
        {
            gameObject.GetComponent<Button>().enabled = true;
        }

    }
}
