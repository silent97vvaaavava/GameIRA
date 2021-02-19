using UnityEngine;
using UnityEngine.UI;




public class ViewLoot : MonoBehaviour
{
    [SerializeField] GameObject nextBoost;

    public static string nameBoost;

    private void LateUpdate()
    {
        transform.GetChild(0).GetComponent<Text>().text = nameBoost;
    }


    

    public void ShowAlert()
    {
        if(nextBoost != null && nextBoost.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
}
