using UnityEngine;

/// <summary>
/// Types d'obstacles avec différentes façons de les éviter
/// </summary>
public enum ObstacleType
{
    Low,       // Bas - il faut sauter par-dessus
    High,      // Haut - il faut glisser en-dessous
    Wide       // Large - il faut esquiver sur le côté (changement de lane)
}

/// <summary>
/// Obstacle amélioré avec différents types
/// </summary>
public class ObstacleAdvanced : MonoBehaviour
{
    [Header("Type d'Obstacle")]
    [Tooltip("Type d'obstacle déterminant comment l'éviter")]
    public ObstacleType obstacleType = ObstacleType.Low;
    
    [Header("Visuel")]
    [Tooltip("Rotation visuelle de l'obstacle (optionnel)")]
    public bool rotateVisual = false;
    
    [Tooltip("Vitesse de rotation")]
    public float rotationSpeed = 50f;
    
    [Header("Debug")]
    [Tooltip("Afficher le type dans l'éditeur")]
    public bool showDebugInfo = true;

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
    /// </summary>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Collision avec obstacle {obstacleType} !");
        }
    }

    /// <summary>
    /// Alternative avec Trigger
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"Collision avec obstacle {obstacleType} (Trigger) !");
            
            
            // Informer le GameManager
            if (GameManager.Instance != null)
            {
                GameManager.Instance.EndGame();
            }
        }
    }

    /// <summary>
    /// Affiche le type d'obstacle dans l'éditeur
    /// </summary>
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;

        // Couleur selon le type
        switch (obstacleType)
        {
            case ObstacleType.Low:
                Gizmos.color = Color.red; // Sauter
                break;
            case ObstacleType.High:
                Gizmos.color = Color.blue; // Glisser
                break;
            case ObstacleType.Wide:
                Gizmos.color = Color.yellow; // Esquiver
                break;
        }

        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
