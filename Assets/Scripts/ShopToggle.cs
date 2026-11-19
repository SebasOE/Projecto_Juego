
using UnityEngine;

public class ShopToggle : MonoBehaviour
{
    public GameObject shopPanel;

    private bool tiendaAbierta = false;

    void Start()
    {
        shopPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            CambiarTienda();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && tiendaAbierta)
        {
            CambiarTienda();
        }
    }

    void CambiarTienda()
    {
        tiendaAbierta = !tiendaAbierta;

        shopPanel.SetActive(tiendaAbierta);

        if (tiendaAbierta)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
