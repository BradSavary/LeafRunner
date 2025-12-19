using UnityEngine;

/// <summary>
/// Contrôle le joueur dans l'endless runner
/// Gère le mouvement automatique, les changements de voie, le saut et la glissade
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Mouvement")]
    [Tooltip("Vitesse de déplacement automatique vers l'avant")]
    public float forwardSpeed = 10f;
    
    [Tooltip("Vitesse d'accélération progressive")]
    public float speedIncreaseRate = 0.5f;
    
    [Tooltip("Vitesse maximum que le joueur peut atteindre")]
    public float maxSpeed = 30f;

    [Header("Système de Voies (Lanes)")]
    [Tooltip("Distance entre chaque voie (gauche, centre, droite)")]
    public float laneDistance = 3f;
    
    [Tooltip("Vitesse de déplacement latéral entre les voies")]
    public float laneChangeSpeed = 10f;
    
    private int currentLane = 1; // 0 = gauche, 1 = centre, 2 = droite
    private Vector3 targetPosition;

    [Header("Saut")]
    [Tooltip("Force du saut")]
    public float jumpForce = 8f;
    
    [Tooltip("Gravité appliquée au joueur")]
    public float gravity = -20f;
    
    private float verticalVelocity = 0f;
    private bool isGrounded = true;

    [Header("Glissade")]
    [Tooltip("Durée de la glissade en secondes")]
    public float slideDuration = 1f;
    
    [Tooltip("Hauteur du collider pendant la glissade")]
    public float slideColliderHeight = 0.5f;
    
    private bool isSliding = false;
    private float slideTimer = 0f;
    private float originalColliderHeight;
    private Vector3 originalColliderCenter;

    [Header("Références")]
    [Tooltip("CharacterController du joueur (à assigner)")]
    public CharacterController characterController;
    
    [Tooltip("Transform du modèle 3D (pour les rotations visuelles)")]
    public Transform playerModel;
    
    [Tooltip("Animator du personnage (pour les animations)")]
    public Animator animator;

    private bool isGameActive = false; // Désactivé au démarrage, activé par GameManager

    void Start()
    {
        // Initialiser la position cible au centre
        targetPosition = transform.position;
        
        // Sauvegarder les dimensions originales du collider
        if (characterController != null)
        {
            originalColliderHeight = characterController.height;
            originalColliderCenter = characterController.center;
        }
        else
        {
            Debug.LogError("CharacterController non assigné sur le PlayerController !");
        }
        
        // Désactiver l'animation au démarrage
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    void Update()
    {
        if (!isGameActive)
        {
            Debug.Log("PlayerController: isGameActive = false");
            return;
        }

        // Gérer les inputs
        HandleInput();
        
        // Mouvement automatique vers l'avant
        MoveForward();
        
        // Mouvement latéral (changement de voie)
        MoveLaterally();
        
        // Gestion du saut et de la gravité
        HandleJumpAndGravity();
        
        // Gestion de la glissade
        HandleSlide();
        
        // Augmenter la vitesse progressivement
        IncreaseSpeed();
    }

    /// <summary>
    /// Gère les entrées clavier du joueur
    /// </summary>
    void HandleInput()
    {
        // Changement de voie : A/Q ou Flèche Gauche
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Input détecté : Gauche");
            MoveLane(-1); // Aller à gauche
        }
        
        // Changement de voie : D ou Flèche Droite
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log("Input détecté : Droite");
            MoveLane(1); // Aller à droite
        }
        
        // Saut : Espace ou W ou Z ou Flèche Haut
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || 
            Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Input détecté : Saut");
            Jump();
        }
        
        // Glissade : S ou Flèche Bas
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartSlide();
        }
    }

    /// <summary>
    /// Déplace le joueur automatiquement vers l'avant
    /// </summary>
    void MoveForward()
    {
        Vector3 moveDirection = transform.forward * forwardSpeed * Time.deltaTime;
        characterController.Move(moveDirection);
    }

    /// <summary>
    /// Déplace le joueur latéralement vers la voie cible
    /// </summary>
    void MoveLaterally()
    {
        // Calculer la position X cible en fonction de la voie
        float targetX = (currentLane - 1) * laneDistance; // -3, 0, ou 3
        targetPosition = new Vector3(targetX, transform.position.y, transform.position.z);
        
        // Interpoler vers la position cible
        Vector3 lateralMove = new Vector3(targetPosition.x - transform.position.x, 0, 0);
        lateralMove = Vector3.ClampMagnitude(lateralMove, laneChangeSpeed * Time.deltaTime);
        
        characterController.Move(lateralMove);
    }

    /// <summary>
    /// Change la voie du joueur
    /// </summary>
    /// <param name="direction">-1 pour gauche, 1 pour droite</param>
    void MoveLane(int direction)
    {
        // Permettre le changement de voie même pendant slide/jump
        currentLane += direction;
        
        // Limiter entre 0 (gauche) et 2 (droite)
        currentLane = Mathf.Clamp(currentLane, 0, 2);
    }

    /// <summary>
    /// Fait sauter le joueur
    /// </summary>
    void Jump()
    {
        if (isGrounded && !isSliding)
        {
            verticalVelocity = jumpForce;
            isGrounded = false;
            
            // Jouer le son du saut
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayJumpSound();
            }
            
            // // Animation de saut
            // if (animator != null)
            // {
            //     animator.SetTrigger("Jump");
            // }
        }
    }

    /// <summary>
    /// Gère la gravité et la détection du sol
    /// </summary>
    void HandleJumpAndGravity()
    {
        // Appliquer la gravité
        verticalVelocity += gravity * Time.deltaTime;
        
        // Déplacer verticalement
        Vector3 verticalMove = new Vector3(0, verticalVelocity * Time.deltaTime, 0);
        CollisionFlags collisionFlags = characterController.Move(verticalMove);
        
        // Vérifier si on touche le sol
        if ((collisionFlags & CollisionFlags.Below) != 0)
        {
            isGrounded = true;
            verticalVelocity = 0f;
        }
    }

    /// <summary>
    /// Démarre une glissade
    /// </summary>
    void StartSlide()
    {
        if (isGrounded && !isSliding)
        {
            isSliding = true;
            slideTimer = 0f;
            
            // Réduire la hauteur du collider
            characterController.height = slideColliderHeight;
            characterController.center = new Vector3(
                originalColliderCenter.x,
                slideColliderHeight / 4f,
                originalColliderCenter.z
            );
            
            // Animation de glissade
            if (animator != null)
            {
                animator.SetTrigger("Slide");
            }
            
            // Ne plus modifier le scale du modèle (l'animation Roll s'en charge)
        }
    }

    /// <summary>
    /// Gère le timer de la glissade
    /// </summary>
    void HandleSlide()
    {
        if (isSliding)
        {
            slideTimer += Time.deltaTime;
            
            // Fin de la glissade
            if (slideTimer >= slideDuration)
            {
                EndSlide();
            }
        }
    }

    /// <summary>
    /// Termine la glissade
    /// </summary>
    void EndSlide()
    {
        isSliding = false;
        
        // Restaurer le collider
        characterController.height = originalColliderHeight;
        characterController.center = originalColliderCenter;
        
        // L'animation retournera automatiquement à Run
    }

    /// <summary>
    /// Augmente la vitesse progressivement
    /// </summary>
    void IncreaseSpeed()
    {
        if (forwardSpeed < maxSpeed)
        {
            forwardSpeed += speedIncreaseRate * Time.deltaTime;
        }
    }

    /// <summary>
    /// Appelé lors d'une collision avec un obstacle
    /// </summary>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        // Vérifier si c'est un obstacle
        if (hit.gameObject.CompareTag("Obstacle"))
        {
            // Déclencher l'animation de mort IMMÉDIATEMENT au contact
            if (animator != null)
            {
                animator.SetTrigger("Die");
            }
            
            GameOver();
        }
        
        // Vérifier si c'est un collectible
        if (hit.gameObject.CompareTag("Collectible"))
        {
            Collectible collectible = hit.gameObject.GetComponent<Collectible>();
            if (collectible != null)
            {
                collectible.Collect();
            }
        }
    }

    /// <summary>
    /// Gère le Game Over
    /// </summary>
    void GameOver()
    {
        isGameActive = false;
        Debug.Log("Game Over !");
        
        // Arrêter le son de course
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.StopRunningSound();
        }
        
        // Animation de mort déjà déclenchée dans OnControllerColliderHit
        // pour une réponse plus rapide
        
        // Informer le GameManager
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager != null)
        {
            gameManager.EndGame();
        }
    }

    /// <summary>
    /// Permet d'arrêter le joueur (appelé par le GameManager)
    /// </summary>
    public void StopPlayer()
    {
        isGameActive = false;
        forwardSpeed = 0f;
    }

    /// <summary>
    /// Réinitialise le joueur (appelé par le GameManager au restart)
    /// </summary>
    public void ResetPlayer()
    {
        Debug.Log("ResetPlayer appelé - Activation du joueur");
        isGameActive = true;
        forwardSpeed = 10f;
        currentLane = 1;
        transform.position = new Vector3(0, transform.position.y, transform.position.z);
        verticalVelocity = 0f;
        isGrounded = true;
        
        if (isSliding)
        {
            EndSlide();
        }
        
        // Activer l'animation Run
        if (animator != null)
        {
            animator.enabled = true;
        }
        
        // Démarrer le son de course
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.PlayRunningSound();
        }
        
        Debug.Log($"ResetPlayer terminé - isGameActive = {isGameActive}");
    }
}
