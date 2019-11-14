using UnityEngine;
using UnityEngine.UI;




public class ViewLoot : MonoBehaviour
{
    [SerializeField] GameObject nextBoost;
     

    public void ViewBoost()
    {
        if(nextBoost != null)
        {
            if(!nextBoost.activeSelf)
            nextBoost.SetActive(true);
            nextBoost.GetComponent<Animator>().enabled = true;  
        }
    }

    public void ShowAlert()
    {
        if(nextBoost != null && nextBoost.activeSelf)
        {
            nextBoost.SetActive(false);
        }
    }
}
