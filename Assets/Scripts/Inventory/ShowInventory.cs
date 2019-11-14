using UnityEngine;

public class ShowInventory : MonoBehaviour
{
    public GameObject Inventory;

    public void ShowMenuInventory()
    {
        Inventory.SetActive(true);
    }

    public void HideMenuInventory()
    {
        Inventory.SetActive(false);
    } 

}
