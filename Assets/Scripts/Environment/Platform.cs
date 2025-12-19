using UnityEngine;

/// <summary>
/// Plateforme sur laquelle le joueur peut monter et se déplacer
/// Permet de varier le gameplay en hauteur
/// </summary>
public class Platform : MonoBehaviour
{
    [Header("Configuration")]
    [Tooltip("Hauteur de la plateforme")]
    public float height = 2f;
    
    [Tooltip("La plateforme doit-elle bloquer le joueur en collision ?")]
    public bool isSolid = true;
    
    [Header("Visuel")]
    [Tooltip("Couleur de la plateforme dans l'éditeur")]
    public Color gizmoColor = Color.green;

    void Start()
    {
        // S'assurer que le collider est configuré correctement
        Collider col = GetComponent<Collider>();
        if (col != null && !isSolid)
        {
            // Si la plateforme n'est pas solide, la rendre trigger
            col.isTrigger = true;
        }
    }

    /// <summary>
    /// Visualisation dans l'éditeur
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}
