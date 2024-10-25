using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; // Le joueur ou l'objet à suivre
    public float distance = 5f; // Distance entre la caméra et le joueur
    public float zoomSpeed = 4f; // Vitesse de zoom avec la molette
    public float minDistance = 2f; // Distance minimale de zoom
    public float maxDistance = 10f; // Distance maximale de zoom
    public float rotationSpeed = 100f; // Vitesse de rotation de la caméra
    public Vector2 pitchMinMax = new Vector2(-40, 85); // Limites pour l'angle vertical (pitch)
    public float smoothTime = 0.2f; // Temps de lissage pour les mouvements de la caméra

    private float yaw = 0f; // Rotation horizontale (gauche/droite)
    private float pitch = 0f; // Rotation verticale (haut/bas)
    private float currentDistance; // Distance actuelle de la caméra
    private Vector3 currentVelocity; // Vitesse utilisée pour le lissage des mouvements

    void Start()
    {
        currentDistance = distance;
    }

    void LateUpdate()
    {
        // Gestion du zoom avec la molette de la souris
        currentDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);

        // Rotation horizontale et verticale avec la souris (sans clic droit)
        yaw += Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime;

        // Limiter l'angle vertical (pitch)
        pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);

        // Calcul de la position de la caméra
        Vector3 targetPosition = target.position - GetCameraOffset();
        Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
        transform.position = smoothedPosition;

        // Faire regarder la caméra vers le joueur
        transform.LookAt(target.position + Vector3.up * 2f); // Ajustement pour regarder légèrement au-dessus
    }

    // Calcul de l'offset de la caméra par rapport à la rotation et la distance
    Vector3 GetCameraOffset()
    {
        // Calcul du décalage basé sur la distance, l'angle horizontal (yaw) et vertical (pitch)
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        return rotation * Vector3.forward * currentDistance;
    }

    // Méthode pour obtenir l'angle horizontal de la caméra (yaw)
    public float GetCameraYaw()
    {
        return yaw;
    }
}
