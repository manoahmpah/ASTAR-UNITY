# ASTAR-UNITY

Bienvenue dans **ASTAR-UNITY**, une implémentation de l'algorithme de recherche de chemin **A* (A-Star)** réalisée avec le moteur Unity. Ce projet démontre comment calculer le chemin le plus court entre deux points sur une grille, tout en contournant des obstacles.

## 📋 Description

L'algorithme A* est l'un des algorithmes de recherche de chemin les plus populaires dans le développement de jeux vidéo en raison de sa performance et de sa précision. Ce projet fournit une base fonctionnelle pour comprendre et visualiser cet algorithme en C#.

Le système repose généralement sur une grille de nœuds (Nodes) où chaque nœud possède :
* Un coût **gCost** (distance du point de départ).
* Un coût **hCost** (distance estimée jusqu'à la cible, l'heuristique).
* Un coût **fCost** (gCost + hCost).

## ✨ Fonctionnalités

* **Recherche de chemin optimale :** Trouve le chemin le plus court d'un point A à un point B.
* **Gestion des obstacles :** Détection des zones infranchissables (murs, obstacles) et calcul d'un itinéraire alternatif.
* **Visualisation (Gizmos) :** Affichage visuel dans l'éditeur Unity du chemin calculé et de la grille (nécessite d'activer les Gizmos).
* **Grille dynamique :** Le système quadrille le monde pour transformer la scène en nœuds de navigation.

## 🛠 Prérequis

* **Unity** (Version recommandée : 2020.3 LTS ou supérieure).
* Un éditeur de code (Visual Studio, JetBrains Rider ou VS Code).

## 🚀 Installation

1.  **Cloner le dépôt :**
    ```bash
    git clone [https://github.com/manoahmpah/ASTAR-UNITY.git](https://github.com/manoahmpah/ASTAR-UNITY.git)
    ```
2.  **Ouvrir avec Unity Hub :**
    * Lancez Unity Hub.
    * Cliquez sur `Add` (Ajouter).
    * Sélectionnez le dossier `ASTAR-UNITY` que vous venez de cloner.
3.  **Lancer le projet :**
    * Ouvrez le projet dans Unity.
    * Allez dans le dossier `Assets` (ou `Assets/Scenes`) et ouvrez la scène principale (souvent nommée `SampleScene` ou `Main`).

## 🎮 Utilisation

1.  Appuyez sur le bouton **Play** ▶️ dans l'éditeur Unity.
2.  Le système devrait générer une grille (visible via les Gizmos dans la vue "Scene" ou "Game").
3.  *Selon l'implémentation spécifique :*
    * Vous pouvez déplacer la cible ("Target") et le chercheur ("Seeker") dans la vue Scène pour voir le chemin se mettre à jour en temps réel.
    * Des obstacles peuvent être placés sur la grille pour bloquer le chemin.

## 📂 Structure du projet (Supposée)

* `Assets/Scripts/` : Contient les scripts C# (ex: `Pathfinding.cs`, `Grid.cs`, `Node.cs`).
* `Assets/Scenes/` : Contient la scène de démonstration.
* `Assets/Materials/` : Matériaux pour la visualisation de la grille et du chemin.

## 🤝 Contribution

Les contributions sont les bienvenues ! Si vous souhaitez améliorer l'algorithme, ajouter de nouvelles heuristiques ou optimiser le code :

1.  Forkez le projet.
2.  Créez votre branche (`git checkout -b feature/AmazingFeature`).
3.  Commitez vos changements (`git commit -m 'Add some AmazingFeature'`).
4.  Push sur la branche (`git push origin feature/AmazingFeature`).
5.  Ouvrez une Pull Request.

## 👤 Auteur

**manoahmpah**
* GitHub : [manoahmpah](https://github.com/manoahmpah)

---
*Ce README a été généré automatiquement pour documenter le projet ASTAR-UNITY.*
