using UnityEngine;

/// <summary>
/// Types de collectibles disponibles
/// </summary>
public enum CollectibleType
{
    Coin,      // Pièce normale (feuille)
    BigCoin,   // Grosse pièce (vaut plus de points)
    PowerUp    // Power-up spécial
}

/// <summary>
/// Types de power-ups disponibles
/// </summary>
public enum PowerUpType
{
    Magnet,    // Aimant - attire les pièces
    Shield,    // Bouclier - protection contre un obstacle
    ScoreBoost // Double les points pendant un temps
}
