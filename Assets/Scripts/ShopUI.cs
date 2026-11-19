
using UnityEngine;
using TMPro;

public class ShopUI : MonoBehaviour
{
    [Header("Textos")]
    public TMP_Text textoMigajas;
    public TMP_Text textoPrecioCaminar;
    public TMP_Text textoPrecioAlmacenamiento;

    [Header("Migajas")]
    public int migajas = 0;
    public int capacidadMaxima = 20;

    [Header("Precio caminar")]
    public int precioCaminar = 100;
    public int aumentoPrecioCaminar = 50;

    [Header("Precio almacenamiento")]
    public int precioAlmacenamiento = 20;
    public int aumentoPrecioAlmacenamiento = 20;
    public int aumentoCapacidad = 5;

    private PlayerMovement jugador;

    void Start()
    {
        jugador = FindFirstObjectByType<PlayerMovement>();
        ActualizarTextos();
    }

    public bool AgregarMigaja()
    {
        if (migajas >= capacidadMaxima)
        {
            Debug.Log("¡Almacenamiento lleno!");
            return false;
        }

        migajas++;
        ActualizarTextos();
        return true;
    }

    public void ComprarCaminar()
    {
        if (migajas < precioCaminar)
        {
            Debug.Log("No tienes suficientes migajas.");
            return;
        }

        if (jugador == null)
            jugador = FindFirstObjectByType<PlayerMovement>();

        if (jugador == null)
            return;

        migajas -= precioCaminar;

        // Aumento de velocidad del 1,10 %
        jugador.currentSpeed *= 1.011f;

        precioCaminar += aumentoPrecioCaminar;

        ActualizarTextos();

        Debug.Log("¡Velocidad mejorada!");
    }

    public void ComprarAlmacenamiento()
    {
        if (migajas < precioAlmacenamiento)
        {
            Debug.Log("No tienes suficientes migajas.");
            return;
        }

        migajas -= precioAlmacenamiento;

        capacidadMaxima += aumentoCapacidad;

        precioAlmacenamiento += aumentoPrecioAlmacenamiento;

        ActualizarTextos();

        Debug.Log("¡Almacenamiento mejorado! Capacidad: " + capacidadMaxima);
    }

    void ActualizarTextos()
    {
        if (textoMigajas != null)
            textoMigajas.text = "Migajas: " + migajas + " / " + capacidadMaxima;

        if (textoPrecioCaminar != null)
            textoPrecioCaminar.text = "Mejorar caminar: " + precioCaminar + " migajas";

        if (textoPrecioAlmacenamiento != null)
            textoPrecioAlmacenamiento.text = "Mejorar almacenamiento: " + precioAlmacenamiento + " migajas";
    }
}
