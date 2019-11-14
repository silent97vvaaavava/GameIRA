using UnityEngine;

public class ShowWinWindow : MonoBehaviour
{
    public int countHealth = 2;
    [SerializeField] GameObject win;
    [SerializeField] CreateWall workWall;

    private void Update()
    {
        //Debug.Log(countHealth);
        if (countHealth == 0)
        {

            win.SetActive(true);
            workWall.enabled = false;
        }
    }
}
