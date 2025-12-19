using UnityEngine;

/// <summary>
/// Représente un obstacle dans le jeu
/// Provoque un Game Over si le joueur entre en collision avec
/// </summary>
public class Obstacle : MonoBehaviour
{
    [Header("Visuel")]
    [Tooltip("Rotation visuelle de l'obstacle (optionnel)")]
    public bool rotateVisual = false;
    
    [Tooltip("Vitesse de rotation")]
    public float rotationSpeed = 50f;

    void Update()
    {
        // Rotation visuelle optionnelle
        if (rotateVisual)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Appelé quand le joueur entre en collision
    /// La détection se fait via le CharacterController du joueur
    /// </summary>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Vérifier que c'est bien le joueur
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log("Collision avec un obstacle !");
            
            // Le PlayerController gère déjà le Game Over
            // Pas besoin de code supplémentaire ici
        }
    }

    /// <summary>
    /// Alternative avec Trigger si vous utilisez des colliders en Trigger
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Collision avec un obstacle (Trigger) !");
            
            // Informer le GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndGame();
            }
        }
    }
}
