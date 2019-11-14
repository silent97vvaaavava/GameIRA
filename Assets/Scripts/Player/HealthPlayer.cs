using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField]  GameObject FirstHeart;
    [SerializeField]  GameObject SecondHeart;
    [SerializeField]  Sprite heartWasted;
    [SerializeField] Sprite heartAdd;

    [SerializeField] GameObject lose;
    [SerializeField] CreateWall workWall;
    [SerializeField] ChoiceBooster boosterShield;

    [HideInInspector] public int countHealth = 2;


    private void Update()
    {


        if(countHealth == 2)
        {
            FirstHeart.GetComponent<Image>().sprite = heartAdd;
            SecondHeart.GetComponent<Image>().sprite = heartAdd;
        }
        if(countHealth == 0)
        {
            lose.SetActive(true);
            workWall.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if(collision.name == "BulletE" && !boosterShield.shield)
        {
            countHealth--;
            if(countHealth == 1)
            FirstHeart.GetComponent<Image>().sprite = heartWasted;
            else
                if(countHealth == 0)
            {
                SecondHeart.GetComponent<Image>().sprite = heartWasted;
                //lose.SetActive(true);
            }
        }
    }
}
