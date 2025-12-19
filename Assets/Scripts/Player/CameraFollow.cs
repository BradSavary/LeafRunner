using UnityEngine;

/// <summary>
/// Caméra qui suit le joueur de manière fluide
/// Position typique pour un endless runner
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Cible")]
    [Tooltip("Transform du joueur à suivre")]
    public Transform target;

    [Header("Position")]
    [Tooltip("Offset de la caméra par rapport au joueur")]
    public Vector3 offset = new Vector3(0, 5, -7);
    
    [Tooltip("Vitesse de suivi de la caméra (plus bas = plus fluide)")]
    public float smoothSpeed = 0.125f;
    
    [Tooltip("Hauteur minimale du joueur pour suivre en Y (pour ignorer le slide)")]
    public float minHeightToFollow = 1f;

    [Header("Rotation")]
    [Tooltip("Angle de rotation de la caméra (regarder vers le bas)")]
    public Vector3 rotation = new Vector3(30, 0, 0);
    
    private float fixedY; // Position Y fixe de la caméra

    void Start()
    {
        // Trouver le joueur automatiquement si non assigné
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogError("Joueur non trouvé ! Ajoutez le tag 'Player' au joueur.");
            }
        }

        // Appliquer la rotation
        transform.rotation = Quaternion.Euler(rotation);
        
        // Sauvegarder la hauteur initiale
        if (target != null)
        {
            fixedY = target.position.y + offset.y;
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Calculer la position désirée
        Vector3 desiredPosition = target.position + offset;
        
        // Si le joueur est proche du sol (slide), garder la hauteur fixe
        // Si le joueur saute (hauteur > min), suivre en Y
        float targetY;
        if (target.position.y > minHeightToFollow)
        {
            // Joueur en l'air (saut) : suivre en Y
            targetY = desiredPosition.y;
        }
        else
        {
            // Joueur au sol ou en slide : revenir à la hauteur fixe initiale
            targetY = fixedY;
        }
        
        // Position finale avec Y ajusté
        Vector3 finalDesiredPosition = new Vector3(desiredPosition.x, targetY, desiredPosition.z);

        // Interpoler vers la position désirée
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalDesiredPosition, smoothSpeed);

        // Appliquer la position
        transform.position = smoothedPosition;

        // Optionnel : toujours regarder le joueur
        // transform.LookAt(target);
    }

    /// <summary>
    /// Visualise l'offset de la caméra dans l'éditeur
    /// </summary>
    void OnDrawGizmos()
    {
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(target.position, target.position + offset);
            Gizmos.DrawWireSphere(target.position + offset, 0.5f);
        }
    }
}
