using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro - meilleure qualité de texte

/// <summary>
/// Gère tous les éléments d'interface utilisateur
/// Menu de démarrage, HUD de jeu, écran de Game Over
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Panneaux")]
    [Tooltip("Panel du menu de démarrage")]
    public GameObject startMenuPanel;
    
    [Tooltip("Panel du HUD en jeu")]
    public GameObject gameHUDPanel;
    
    [Tooltip("Panel de Game Over")]
    public GameObject gameOverPanel;

    [Header("Textes du HUD")]
    [Tooltip("Texte affichant le score actuel")]
    public TextMeshProUGUI scoreText;
    
    [Tooltip("Texte affichant le high score")]
    public TextMeshProUGUI highScoreText;

    [Header("Textes Game Over")]
    [Tooltip("Texte du score final")]
    public TextMeshProUGUI finalScoreText;
    
    [Tooltip("Texte du meilleur score")]
    public TextMeshProUGUI bestScoreText;

    [Header("Boutons")]
    [Tooltip("Bouton pour démarrer le jeu")]
    public Button startButton;
    
    [Tooltip("Bouton pour redémarrer")]
    public Button restartButton;
    
    [Tooltip("Bouton pour quitter")]
    public Button quitButton;

    void Start()
    {
        // Assigner les fonctions aux boutons
        if (startButton != null)
        {
            startButton.onClick.AddListener(OnStartButtonClicked);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitButtonClicked);
        }

        // Afficher le menu de démarrage par défaut
        ShowStartMenu();
    }

    /// <summary>
    /// Affiche le menu de démarrage
    /// </summary>
    public void ShowStartMenu()
    {
        if (startMenuPanel != null) startMenuPanel.SetActive(true);
        if (gameHUDPanel != null) gameHUDPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Afficher le high score dans le menu
        if (highScoreText != null && GameManager.Instance != null)
        {
            highScoreText.text = "Meilleur Score: " + GameManager.Instance.GetHighScore();
        }
    }

    /// <summary>
    /// Affiche le HUD pendant le jeu
    /// </summary>
    public void ShowGameHUD()
    {
        if (startMenuPanel != null) startMenuPanel.SetActive(false);
        if (gameHUDPanel != null) gameHUDPanel.SetActive(true);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // Initialiser le score à 0
        UpdateScore(0);
    }

    /// <summary>
    /// Affiche l'écran de Game Over
    /// </summary>
    /// <param name="finalScore">Score final de la partie</param>
    /// <param name="highScore">Meilleur score</param>
    public void ShowGameOver(int finalScore, int highScore)
    {
        if (startMenuPanel != null) startMenuPanel.SetActive(false);
        if (gameHUDPanel != null) gameHUDPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(true);

        // Afficher les scores
        if (finalScoreText != null)
        {
            finalScoreText.text = "Score: " + finalScore;
        }

        if (bestScoreText != null)
        {
            bestScoreText.text = "Meilleur Score: " + highScore;
            
            // Mettre en évidence si c'est un nouveau record
            if (finalScore >= highScore)
            {
                bestScoreText.text = "NOUVEAU RECORD: " + highScore + " !";
                bestScoreText.color = Color.yellow;
            }
            else
            {
                bestScoreText.color = Color.white;
            }
        }
    }

    /// <summary>
    /// Met à jour l'affichage du score
    /// </summary>
    /// <param name="score">Score actuel</param>
    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    /// <summary>
    /// Appelé quand on clique sur le bouton Start
    /// </summary>
    void OnStartButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartGame();
        }
        else
        {
            Debug.LogError("GameManager introuvable !");
        }
    }

    /// <summary>
    /// Appelé quand on clique sur le bouton Restart
    /// </summary>
    void OnRestartButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
        else
        {
            Debug.LogError("GameManager introuvable !");
        }
    }

    /// <summary>
    /// Appelé quand on clique sur le bouton Quit
    /// </summary>
    void OnQuitButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.QuitGame();
        }
        else
        {
            Debug.LogError("GameManager introuvable !");
        }
    }
}
