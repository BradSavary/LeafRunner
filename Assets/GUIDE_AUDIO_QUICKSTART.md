# 🎵 Configuration Rapide - AudioManager

## ⚡ Configuration en 3 Minutes

### 1️⃣ Créer l'AudioManager

Dans Unity :
```
Hiérarchie → Clic droit → Create Empty → Renommer "AudioManager"
```

### 2️⃣ Ajouter le Script

```
GameObject AudioManager → Inspector → Add Component → AudioManager
```

### 3️⃣ Assigner les Sons

Dans l'Inspector de l'AudioManager :

```
┌─────────────────────────────────────────┐
│ AudioManager (Script)                   │
├─────────────────────────────────────────┤
│ Music De Fond                           │
│ ├─ Background Music: music.mp3         │
│ └─ Music Volume: ▓▓▓▓▓░░░░░ (0.5)     │
│                                         │
│ Effets Sonores                          │
│ ├─ Jump Sound: jump.mp3                │
│ ├─ Running Sound: running.mp3          │
│ └─ SFX Volume: ▓▓▓▓▓▓▓░░░ (0.7)       │
└─────────────────────────────────────────┘
```

## 🎮 Comment Assigner un Clip Audio

1. Dans l'Inspector, cliquer sur le cercle à droite de "Background Music"
2. Dans la fenêtre qui s'ouvre, chercher "music"
3. Double-cliquer sur `music.mp3`
4. Répéter pour Jump Sound et Running Sound

## ✅ Vérification

Après configuration, vous devriez voir dans l'Inspector :

```
AudioManager
├─ 3 AudioSources ajoutés automatiquement
├─ backgroundMusic ✓
├─ jumpSound ✓
└─ runningSound ✓
```

## 🚀 Tester

1. Appuyer sur Play
2. Cliquer sur "Démarrer"
3. La musique démarre → ✅
4. Le son de course démarre → ✅
5. Appuyer sur Espace → Son de saut → ✅

## 🎯 Structure des AudioSources

L'AudioManager crée automatiquement 3 AudioSources :

| AudioSource | Type | Loop | Utilisation |
|-------------|------|------|-------------|
| musicSource | Background | ✅ Oui | Musique continue |
| sfxSource | Sound Effect | ❌ Non | Sons ponctuels (saut) |
| runningSource | Sound Effect | ✅ Oui | Son de course |

## 💡 Astuce

Pour un démarrage rapide, glissez-déposez les fichiers depuis le dossier `Assets/Audio/` directement dans l'Inspector !
