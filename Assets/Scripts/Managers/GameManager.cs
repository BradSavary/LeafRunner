using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère l'état global du jeu, le score, la vitesse et les transitions
/// Pattern Singleton : une seule instance dans toute la scène
/// </summary>
public class GameManager : MonoBehaviour
{
    // ===== SINGLETON PATTERN =====
    // Permet d'accéder au GameManager depuis n'importe où : GameManager.Instance
    public static GameManager Instance { get; private set; }

    [Header("État du Jeu")]
    [Tooltip("État actuel du jeu")]
    public GameState currentState = GameState.Menu;

    [Header("Score")]
    [Tooltip("Score actuel de la partie")]
    public int currentScore = 0;
    
    [Tooltip("Points gagnés par unité de distance parcourue")]
    public float scorePerDistance = 1f;
    
    [Tooltip("Points gagnés par collectible ramassé")]
    public int scorePerCollectible = 10;
    
    private float distanceTraveled = 0f;

    [Header("Vitesse")]
    [Tooltip("Référence au PlayerController pour gérer la vitesse")]
    public PlayerController playerController;

    [Header("High Score")]
    private int highScore = 0;
    private const string HIGH_SCORE_KEY = "HighScore"; // Clé pour PlayerPrefs

    [Header("UI")]
    [Tooltip("Référence au UIManager")]
    public UIManager uiManager;

    // ===== INITIALISATION DU SINGLETON =====
    void Awake()
    {
        // Vérifier s'il existe déjà une instance
        if (Instance != null && Instance != this)
        {
            // Détruire ce GameObject car une instance existe déjà
            Destroy(gameObject);
            return;
        }

        // Définir cette instance comme l'instance unique
        Instance = this;
        
        // Optionnel : garder ce GameObject entre les scènes
        // DontDestroyOnLoad(gameObject);

        // Charger le high score sauvegardé
        LoadHighScore();
    }

    void Start()
    {
        // Trouver les références si elles ne sont pas assignées
        if (playerController == null)
        {
            playerController = FindFirstObjectByType<PlayerController>();
        }

        if (uiManager == null)
        {
            uiManager = FindFirstObjectByType<UIManager>();
        }

        // Afficher le menu de démarrage
        if (uiManager != null)
        {
            uiManager.ShowStartMenu();
        }
    }

    void Update()
    {
        // Calculer le score basé sur la distance uniquement pendant le jeu
        if (currentState == GameState.Playing && playerController != null)
        {
            CalculateDistanceScore();
        }
    }

    /// <summary>
    /// Démarre une nouvelle partie
    /// </summary>
    public void StartGame()
    {
        currentState = GameState.Playing;
        currentScore = 0;
        distanceTraveled = 0f;

        // Réinitialiser le joueur
        if (playerController != null)
        {
            playerController.ResetPlayer();
        }

        // Masquer le menu, afficher le HUD
        if (uiManager != null)
        {
            uiManager.ShowGameHUD();
            uiManager.UpdateScore(currentScore);
        }

        Debug.Log("Partie démarrée !");
    }

    /// <summary>
    /// Termine la partie (Game Over)
    /// </summary>
    public void EndGame()
    {
        if (currentState != GameState.Playing) return;

        currentState = GameState.GameOver;

        // Arrêter le joueur
        if (playerController != null)
        {
            playerController.StopPlayer();
        }

        // Vérifier et sauvegarder le high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
        }

        // Afficher l'écran de game over
        if (uiManager != null)
        {
            uiManager.ShowGameOver(currentScore, highScore);
        }

        Debug.Log($"Game Over ! Score final : {currentScore}");
    }

    /// <summary>
    /// Redémarre la partie
    /// </summary>
    public void RestartGame()
    {
        // Recharger la scène actuelle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Calcule le score basé sur la distance parcourue
    /// </summary>
    void CalculateDistanceScore()
    {
        if (playerController != null)
        {
            // Ajouter la distance parcourue cette frame
            float distance = playerController.forwardSpeed * Time.deltaTime;
            distanceTraveled += distance;

            // Calculer le score
            int newScore = Mathf.FloorToInt(distanceTraveled * scorePerDistance);
            
            if (newScore > currentScore)
            {
                currentScore = newScore;
                
                // Mettre à jour l'UI
                if (uiManager != null)
                {
                    uiManager.UpdateScore(currentScore);
                }
            }
        }
    }

    /// <summary>
    /// Ajoute des points au score (appelé par les collectibles)
    /// </summary>
    /// <param name="points">Nombre de points à ajouter</param>
    public void AddScore(int points)
    {
        currentScore += points;
        
        if (uiManager != null)
        {
            uiManager.UpdateScore(currentScore);
        }
    }

    /// <summary>
    /// Récupère le score actuel du jeu
    /// </summary>
    public int GetCurrentScore()
    {
        return currentScore;
    }

    /// <summary>
    /// Récupère le high score sauvegardé
    /// </summary>
    public int GetHighScore()
    {
        return highScore;
    }

    /// <summary>
    /// Charge le high score depuis PlayerPrefs
    /// </summary>
    void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
        Debug.Log($"High Score chargé : {highScore}");
    }

    /// <summary>
    /// Sauvegarde le high score dans PlayerPrefs
    /// </summary>
    void SaveHighScore()
    {
        PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
        PlayerPrefs.Save();
        Debug.Log($"Nouveau High Score sauvegardé : {highScore}");
    }

    /// <summary>
    /// Quitte le jeu (uniquement en build)
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Quitter le jeu");
        Application.Quit();
    }
}

/// <summary>
/// Énumération des différents états du jeu
/// </summary>
public enum GameState
{
    Menu,       // Menu de démarrage
    Playing,    // En jeu
    Paused,     // En pause
    GameOver    // Game Over
}
