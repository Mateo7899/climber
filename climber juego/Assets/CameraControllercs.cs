using UnityEngine;

public class VerticalCameraScroll : MonoBehaviour
{
    [Header("Velocidad de subida (usa valores bajos como 0.5 - 2.0)")]
    public float scrollSpeed = 0.5f;

    [Header("Jugador y lógica de muerte")]
    public Transform player;
    public float deathOffset = -6f;

    [Header("Altura máxima de la cámara")]
    public float maxY = 3f;

    private float lockedX;
    private float lockedZ;

    void Start()
    {
        // Guardamos posición X y Z para mantenerlas fijas
        lockedX = transform.position.x;
        lockedZ = transform.position.z;
    }

    void Update()
    {
        // Movimiento vertical controlado hasta llegar a maxY
        if (transform.position.y < maxY)
        {
            float newY = Mathf.Min(transform.position.y + scrollSpeed * Time.deltaTime, maxY);
            transform.position = new Vector3(lockedX, newY, lockedZ);
        }
        else
        {
            // Al llegar a maxY, fijamos la posición
            transform.position = new Vector3(lockedX, maxY, lockedZ);
        }

        // Verificar si el jugador queda abajo
        if (player != null && player.position.y < transform.position.y + deathOffset)
        {
            Debug.Log("¡El jugador ha muerto por quedarse atrás!");
            // Aquí puedes reiniciar nivel o mostrar pantalla de derrota
        }
    }
}
