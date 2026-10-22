using Unity.VisualScripting;
using UnityEngine;

public class Migaja : MonoBehaviour
{
    private bool hormigaCerca = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hormigaCerca = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            hormigaCerca = false;
        }
    }

    private void Update()
    {
        if (hormigaCerca && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("¡Has recogido una migaja!");
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        if (hormigaCerca)
        {
            GUI.Label(
                new Rect(Screen.width / 2 - 70, Screen.height - 100, 200, 30),
                "E para recoger"
            );
        }
    }
}
