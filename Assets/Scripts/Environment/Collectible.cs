using UnityEngine;

/// <summary>
/// Représente un objet collectible (feuille, baie, cristal, etc.)
/// Ajoute des points au score quand ramassé
/// </summary>
public class Collectible : MonoBehaviour
{
    [Header("Type")]
    [Tooltip("Type de collectible")]
    public CollectibleType collectibleType = CollectibleType.Coin;
    
    [Tooltip("Si c'est un power-up, quel type ?")]
    public PowerUpType powerUpType = PowerUpType.Magnet;
    
    [Header("Score")]
    [Tooltip("Nombre de points donnés par ce collectible")]
    public int pointValue = 10;

    [Header("Visuel")]
    [Tooltip("Faire tourner le collectible sur lui-même")]
    public bool rotate = true;
    
    [Tooltip("Vitesse de rotation")]
    public float rotationSpeed = 100f;
    
    [Tooltip("Faire léviter le collectible")]
    public bool levitate = true;
    
    [Tooltip("Amplitude du mouvement de lévitation")]
    public float levitationHeight = 0.5f;
    
    [Tooltip("Vitesse de lévitation")]
    public float levitationSpeed = 2f;

    private Vector3 startPosition;

    void Start()
    {
        // Sauvegarder la position de départ pour la lévitation
        startPosition = transform.position;
    }

    void Update()
    {
        // Rotation visuelle
        if (rotate)
        {
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

        // Lévitation
        if (levitate)
        {
            float newY = startPosition.y + Mathf.Sin(Time.time * levitationSpeed) * levitationHeight;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }

    /// <summary>
    /// Appelé quand le joueur ramasse le collectible
    /// </summary>
    public void Collect()
    {
        // Traiter selon le type
        switch (collectibleType)
        {
            case CollectibleType.Coin:
            case CollectibleType.BigCoin:
                // Ajouter des points au score
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.AddScore(pointValue);
                    Debug.Log($"Collectible ramassé ! +{pointValue} points");
                }
                break;
                
            case CollectibleType.PowerUp:
                // Activer le power-up
                ActivatePowerUp();
                break;
        }

        // Effet sonore (à ajouter plus tard)
        // AudioManager.Instance.PlayCollectSound();

        // Effet de particules (à ajouter plus tard)
        // Instantiate(collectParticles, transform.position, Quaternion.identity);

        // Désactiver le collectible
        gameObject.SetActive(false);
    }
    
    /// <summary>
    /// Active un power-up
    /// </summary>
    void ActivatePowerUp()
    {
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player == null) return;
        
        switch (powerUpType)
        {
            case PowerUpType.Magnet:
                Debug.Log("Power-up Aimant activé !");
                // TODO: Implémenter l'aimant
                break;
                
            case PowerUpType.Shield:
                Debug.Log("Power-up Bouclier activé !");
                // TODO: Implémenter le bouclier
                break;
                
            case PowerUpType.ScoreBoost:
                Debug.Log("Power-up Score x2 activé !");
                // TODO: Implémenter le multiplicateur
                break;
        }
    }

    /// <summary>
    /// Détection par Trigger (recommandé pour les collectibles)
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Collect();
        }
    }

    /// <summary>
    /// Alternative : détection par CharacterController
    /// </summary>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Player"))
        {
            Collect();
        }
    }
}
