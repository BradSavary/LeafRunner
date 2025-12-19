using UnityEngine;

/// <summary>
/// Composant à ajouter sur chaque prefab de segment
/// Permet de définir sa longueur personnalisée
/// </summary>
public class SegmentInfo : MonoBehaviour
{
    [Header("Dimensions")]
    [Tooltip("Longueur du segment en unités Unity")]
    public float length = 10f;
    
    [Header("Points de Spawn")]
    [Tooltip("Position de départ du segment (optionnel)")]
    public Transform startPoint;
    
    [Tooltip("Position de fin du segment (optionnel)")]
    public Transform endPoint;
    
    [Header("Debug")]
    [Tooltip("Afficher les gizmos dans l'éditeur")]
    public bool showGizmos = true;

    void Start()
    {
        // Si des points sont définis, calculer la longueur automatiquement
        if (startPoint != null && endPoint != null)
        {
            length = Vector3.Distance(startPoint.position, endPoint.position);
        }
    }

    /// <summary>
    /// Récupère la longueur du segment
    /// </summary>
    public float GetLength()
    {
        return length;
    }

    /// <summary>
    /// Visualisation dans l'éditeur
    /// </summary>
    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // Dessiner la zone du segment
        Gizmos.color = Color.cyan;
        Vector3 center = transform.position + Vector3.forward * (length / 2f);
        Vector3 size = new Vector3(10f, 0.1f, length);
        Gizmos.DrawWireCube(center, size);

        // Points de départ et fin
        if (startPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(startPoint.position, 0.5f);
        }

        if (endPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(endPoint.position, 0.5f);
        }
    }
}
