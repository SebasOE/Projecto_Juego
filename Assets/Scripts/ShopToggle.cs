
using UnityEngine;

public class ShopToggle : MonoBehaviour
{
    public GameObject shopPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            shopPanel.SetActive(!shopPanel.activeSelf);
        }
    }
}
