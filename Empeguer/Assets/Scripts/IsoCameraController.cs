using UnityEngine;

public class IsoCameraController : MonoBehaviour
{
    public Transform target; // Le joueur ou l'objet à suivre
    public float distance = 10f; // Distance initiale de la caméra
    public float zoomSpeed = 2f; // Vitesse de zoom/dézoom avec la molette
    public float minDistance = 5f; // Distance minimale de la caméra
    public float maxDistance = 20f; // Distance maximale de la caméra
    public float rotationSpeed = 100f; // Vitesse de rotation de la caméra (flèches et souris)
    public float pitch = 45f; // Inclinaison verticale fixe pour la vue isométrique

    private float yaw = 0f; // Rotation horizontale de la caméra (angle Y)
    private float currentDistance; // Distance actuelle de la caméra

    void Start()
    {
        currentDistance = distance;
    }

    void LateUpdate()
    {
        HandleInput(); // Gérer l'entrée utilisateur (flèches ou clic souris)

        // Calcul de la position de la caméra en fonction de la distance, du yaw (rotation) et du pitch (inclinaison)
        Vector3 direction = new Vector3(0, 0, -currentDistance);
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        Vector3 position = target.position + rotation * direction;

        transform.position = position; // Positionner la caméra autour du personnage
        transform.LookAt(target.position); // Toujours regarder vers le personnage
    }

    void HandleInput()
    {
        // Zoom/Dézoom avec la molette de la souris
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Rotation avec les flèches directionnelles
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            yaw -= rotationSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            yaw += rotationSpeed * Time.deltaTime;
        }

        // Rotation avec la souris (si clic droit maintenu)
        // if (Input.GetMouseButton(1)) // Clic droit
        // {
        //     yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        // }

        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
    }
}
