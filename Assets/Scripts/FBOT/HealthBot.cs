using UnityEngine;
using UnityEngine.UI;

public class HealthBot : MonoBehaviour
{
    [SerializeField] private GameObject FirstHeart;
    [SerializeField] private GameObject SecondHeart;
    [SerializeField] private Sprite heart;

    [SerializeField] ShowWinWindow count;

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.name);
        if (collision.name == "BulletPl")
        {
            count.countHealth--;
            if (count.countHealth == 1)
                FirstHeart.GetComponent<Image>().sprite = heart;
            else
                if (count.countHealth == 0)
            {
                SecondHeart.GetComponent<Image>().sprite = heart;
                //win.SetActive(true);
            }
        }
    }
}
