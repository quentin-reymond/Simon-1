using System.Collections.ObjectModel;

namespace Simon.Models
{
    public class SimonGame
    {
        // Les couleurs du jeu Simon
        public enum SimonColor
        {
            Red,
            Green,
            Blue,
            Yellow
        }

        // Séquence actuelle que le joueur doit reproduire
        private List<SimonColor> _sequence = new List<SimonColor>();
        
        // Séquence entrée par le joueur
        private List<SimonColor> _playerSequence = new List<SimonColor>();
        
        // Générateur de nombres aléatoires pour les séquences
        private Random _random = new Random();
        
        // Score actuel
        public int Score { get; private set; } = 0;
        
        // Niveau actuel
        public int Level { get; private set; } = 1;
        
        // État du jeu
        public bool IsGameOver { get; private set; } = false;
        
        // Indique si c'est au tour du joueur
        public bool IsPlayerTurn { get; private set; } = false;

        // Événement déclenché lorsque la séquence doit être affichée
        public event Action<SimonColor>? OnShowColor;
        
        // Événement déclenché lorsque le jeu est terminé
        public event Action? OnGameOver;
        
        // Événement déclenché lorsque le niveau change
        public event Action<int>? OnLevelChanged;

        // Démarre une nouvelle partie
        public void StartNewGame()
        {
            _sequence.Clear();
            _playerSequence.Clear();
            Score = 0;
            Level = 1;
            IsGameOver = false;
            
            // Ajoute la première couleur à la séquence
            AddColorToSequence();
            
            // Notifie du changement de niveau
            OnLevelChanged?.Invoke(Level);
            
            // Joue la séquence
            PlaySequence();
        }

        // Ajoute une nouvelle couleur aléatoire à la séquence
        private void AddColorToSequence()
        {
            SimonColor newColor = (SimonColor)_random.Next(0, 4);
            _sequence.Add(newColor);
        }

        // Joue la séquence actuelle
        public async void PlaySequence()
        {
            IsPlayerTurn = false;
            
            // Pause avant de commencer la séquence
            await Task.Delay(1000);
            
            // Joue chaque couleur de la séquence
            foreach (var color in _sequence)
            {
                OnShowColor?.Invoke(color);
                
                // Pause entre chaque couleur
                await Task.Delay(500);
                
                // Pause avec les couleurs éteintes
                await Task.Delay(300);
            }
            
            // C'est maintenant au tour du joueur
            IsPlayerTurn = true;
            _playerSequence.Clear();
        }

        // Traite l'entrée du joueur
        public void PlayerInput(SimonColor color)
        {
            if (!IsPlayerTurn || IsGameOver)
                return;
                
            _playerSequence.Add(color);
            
            // Vérifie si l'entrée est correcte
            int index = _playerSequence.Count - 1;
            if (_playerSequence[index] != _sequence[index])
            {
                // Erreur - fin de partie
                IsGameOver = true;
                OnGameOver?.Invoke();
                return;
            }
            
            // Vérifie si la séquence complète a été reproduite
            if (_playerSequence.Count == _sequence.Count)
            {
                // Séquence correcte - passe au niveau suivant
                Score += Level * 10;
                Level++;
                
                // Notifie du changement de niveau
                OnLevelChanged?.Invoke(Level);
                
                // Ajoute une nouvelle couleur
                AddColorToSequence();
                
                // Rejoue la séquence
                PlaySequence();
            }
        }
    }
}