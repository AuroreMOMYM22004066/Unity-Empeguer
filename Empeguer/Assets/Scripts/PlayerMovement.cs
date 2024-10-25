using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f; // Vitesse de déplacement
    public float climbForce = 5f; // Force supplémentaire pour grimper les rampes
    public float slopAngle = 45f;
    public Transform cameraTransform; // Référence à la caméra
    private Rigidbody rb;

    void Start()
    {
        // Récupération du Rigidbody du joueur
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gestion des déplacements uniquement avec Z, Q, S, D (AZERTY)
        float moveHorizontal = Input.GetKey(KeyCode.A) ? -1 : Input.GetKey(KeyCode.D) ? 1 : 0;
        float moveVertical = Input.GetKey(KeyCode.W) ? 1 : Input.GetKey(KeyCode.S) ? -1 : 0;

        // Calcul de la direction du mouvement relatif à la caméra
        Vector3 forward = cameraTransform.forward; // Direction "avant" de la caméra
        Vector3 right = cameraTransform.right; // Direction "droite" de la caméra

        // On ignore la composante verticale (Y) pour garder le mouvement sur le plan horizontal
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Direction finale du déplacement
        Vector3 movementDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        // Déplacement du joueur
        Vector3 newPosition = rb.position + movementDirection * speed * Time.deltaTime;
        rb.MovePosition(newPosition);

        // Ajouter une force vers le haut si le joueur est sur une pente
        if (IsOnSlope())
        {
            rb.AddForce(Vector3.up * climbForce, ForceMode.Acceleration);
        }

        // Si le joueur se déplace, on met à jour sa rotation pour qu'il regarde dans la direction du mouvement
        if (movementDirection != Vector3.zero)
        {
            Quaternion newRotation = Quaternion.LookRotation(movementDirection);
            rb.MoveRotation(newRotation);
        }
    }

    // Méthode pour vérifier si le joueur est sur une pente
    private bool IsOnSlope()
    {
        RaycastHit hit;
        // Raycast vers le bas pour détecter la surface sous le joueur
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Vérifie l'angle de la surface (inclinaison)
            return Vector3.Angle(hit.normal, Vector3.up) > slopAngle; // Ajuste 5f selon tes besoins
        }
        return false;
    }
}
