# 🏃‍♂️ Leaf Runner — Feuille de Route / Guide de Développement

## 🎯 Concept Général
Jeu **endless runner 3D** dans un environnement **low poly nature**.  
Le joueur court automatiquement sur un chemin composé de **segments modulaires** (prefabs).  
Il doit éviter des obstacles, changer de voie (3 lanes), sauter, glisser et ramasser des collectibles.

---

## 🎨 Direction Artistique (DA)
- Style **low poly stylisé** (forêt / nature).
- Pas ou peu de textures : couleurs unies, formes propres.
- Assets requis :
  - arbres, rochers, buissons, troncs, herbes, ponts simples
  - collectibles : feuilles / baies / cristaux
  - personnage low poly simple
- Éclairage léger :
  - 1 directional light (soleil)
  - post-processing minimal (bloom léger, vignette douce)

---

## 🧱 Structure du Monde (Segments)
Le jeu utilise des **segments de route** (~10–15m) instanciés en continu.

### Types de segments
1. Segment simple
2. Segment avec obstacles
3. Segment avec collectibles
4. Segment décoratif
5. Segment spécial (rivière + pont, croisement simple)

### Caractéristiques d’un segment
- Prefab contenant :
  - point d’entrée
  - point de sortie
  - emplacements vides (empty objects) pour obstacles et items
- Système continuel :
  - spawn d’un segment devant le joueur
  - destruction (ou pooling) des segments derrière

---

## 👤 Joueur
- Avance automatique (vitesse croissante avec le temps).
- Contrôles :
  - **Gauche / Centre / Droite** (système de 3 lanes)
  - **Saut**
  - **Glissade**
- Peut être un modèle low poly simple, animations optionnelles au début.

---

## ⛔ Obstacles
Tous en prefabs, avec colliders simples (Box ou Capsule) :
- rocher
- tronc tombé
- souche
- arbre incliné
- petit mur ou pierre haute

Collision → Game Over.

---

## 💎 Collectibles
- feuilles, baies, cristaux
- rotation visuelle facultative
- collision → score+
- serviront à ajouter des power-ups dans l’avenir (aimant, invincibilité, boost)

---

## 🧠 Gameplay / Systèmes
- **Score** = distance parcourue + collectibles ramassés.
- **Vitesse augmente** au fil du temps.
- **Game Over** si collision obstacle.
- **UI** :
  - score courant
  - meilleur score
  - bouton restart

---

## 📦 Assets Requis
- 5–10 arbres low poly
- rochers / souches / troncs
- buissons + herbes
- modèle joueur simple
- collectibles low poly
- sol / chemin stylisé low poly

---

## 🗂️ Hiérarchie Unity Recommandée

### Hiérarchie Unity recommandée

- Player/
- Environment/
    - Segments/
    - Obstacles/
    - Collectibles/
- Managers/
    - GameManager
    - SegmentSpawner
    - UIManager
---

## 🧩 Scripts Principaux à Implémenter

### `PlayerController.cs`
- mouvement automatique
- gestion des lanes (gauche / centre / droite)
- saut + glissade
- collisions joueur

### `SegmentSpawner.cs`
- liste de segments
- spawn continu
- alignement entrée/sortie
- destruction ou pooling

### `Obstacle.cs`
- collision → game over

### `Collectible.cs`
- rotation visuelle
- collision → score+ et disable

### `GameManager.cs`
- états du jeu (start / run / end)
- scoring
- gestion vitesse
- restart

### `UIManager.cs`
- affichage score / highscore

---

## 🚀 Étapes du Développement
1. Prototype : terrain + joueur + caméra.
2. Implémenter lanes + saut + slide.
3. Créer 2–3 segments simples.
4. Ajouter obstacles + collectibles.
5. Créer le spawner infini.
6. UI basique (score, highscore).
7. Ajouter DA, décor, polish visuel.
8. Optimisations (pooling, LOD).

---

## 🪵 Nom du Jeu
**Leaf Runner**

---

## 📁 État Actuel du Projet

### ✅ Scripts Créés et Fonctionnels

Tous les scripts principaux ont été créés avec commentaires détaillés :

1. **PlayerController.cs** - Contrôle complet du joueur (lanes, saut, glissade)
2. **CameraFollow.cs** - Caméra qui suit le joueur
3. **GameManager.cs** - Gestion globale du jeu (Singleton)
4. **SegmentSpawner.cs** - Génération infinie avec pooling
5. **Obstacle.cs** - Gestion des obstacles
6. **Collectible.cs** - Gestion des collectibles
7. **UIManager.cs** - Interface utilisateur complète

### 📚 Guides Disponibles

- **GUIDE_CONFIGURATION_UNITY.md** - Guide complet étape par étape pour configurer Unity
- **BONNES_PRATIQUES_UNITY.md** - Bonnes pratiques pour débutants
- **RECAPITULATIF_PROJET.md** - Vue d'ensemble complète du projet

### 🎯 Prochaine Étape

**Suivez le fichier `GUIDE_CONFIGURATION_UNITY.md`** pour configurer votre scène Unity et tester le jeu !

Temps estimé : ~45 minutes pour un prototype jouable complet.