
using UnityEngine;

public class EventCrumb : MonoBehaviour
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

            // Buscar al jugador
            PlayerMovement player =
                FindFirstObjectByType<PlayerMovement>();

            // Aumentar la velocidad del jugador
            if (player != null)
            {
                player.IncreaseSpeed();
            }

            // Destruir la migaja recogida
            Destroy(gameObject);
        }
    }

    private void OnGUI()
    {
        if (hormigaCerca)
        {
            GUI.Label(
                new Rect(
                    Screen.width / 2 - 70,
                    Screen.height - 100,
                    200,
                    30
                ),
                "E para recoger"
            );
        }
    }
}
