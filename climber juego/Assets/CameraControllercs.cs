using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;            // El objetivo que la cámara sigue (tu personaje)
    public float distance = 5.0f;       // Distancia inicial desde el objetivo
    public float xSpeed = 500.0f;       // Sensibilidad de rotación horizontal
    public float ySpeed = 500.0f;       // Sensibilidad de rotación vertical

    public float yMinLimit = -20f;      // Límite inferior de rotación vertical
    public float yMaxLimit = 80f;       // Límite superior de rotación vertical

    public float distanceMin = 2f;      // Zoom mínimo
    public float distanceMax = 10f;     // Zoom máximo

    private float x = 0.0f;             // Rotación horizontal acumulada
    private float y = 0.0f;             // Rotación vertical acumulada

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Bloquea el cursor para una experiencia inmersiva
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (target)
        {
            // Movimiento del mouse
            x += Input.GetAxis("Mouse X") * xSpeed * Time.deltaTime;
            y -= Input.GetAxis("Mouse Y") * ySpeed * Time.deltaTime;

            // Limita la rotación vertical
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            // Zoom con la rueda del mouse
            distance -= Input.GetAxis("Mouse ScrollWheel") * 2;
            distance = Mathf.Clamp(distance, distanceMin, distanceMax);

            // Calcula la nueva rotación
            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // Calcula la nueva posición
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
