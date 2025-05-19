using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Simon.Models;
using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;

namespace Simon.ViewModels
{
    public class SimonViewModel : INotifyPropertyChanged
    {
        private readonly SimonGame _game;
        
        // Propriétés observables
        private int _score;
        public int Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private int _level;
        public int Level
        {
            get => _level;
            set
            {
                if (_level != value)
                {
                    _level = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool _isGameOver;
        public bool IsGameOver
        {
            get => _isGameOver;
            set
            {
                if (_isGameOver != value)
                {
                    _isGameOver = value;
                    OnPropertyChanged();
                    
                    if (value)
                    {
                        // Déclencher l'animation de Game Over
                        AnimateGameOver();
                    }
                }
            }
        }
        
        private bool _isGameActive;
        public bool IsGameActive
        {
            get => _isGameActive;
            set
            {
                if (_isGameActive != value)
                {
                    _isGameActive = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool _isStartScreenVisible = true;
        public bool IsStartScreenVisible
        {
            get => _isStartScreenVisible;
            set
            {
                if (_isStartScreenVisible != value)
                {
                    _isStartScreenVisible = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _startScreenOpacity = 1.0;
        public double StartScreenOpacity
        {
            get => _startScreenOpacity;
            set
            {
                if (_startScreenOpacity != value)
                {
                    _startScreenOpacity = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _startScreenTranslation = 0;
        public double StartScreenTranslation
        {
            get => _startScreenTranslation;
            set
            {
                if (_startScreenTranslation != value)
                {
                    _startScreenTranslation = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _gameBoardScale = 0.8;
        public double GameBoardScale
        {
            get => _gameBoardScale;
            set
            {
                if (_gameBoardScale != value)
                {
                    _gameBoardScale = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _gameBoardOpacity = 0;
        public double GameBoardOpacity
        {
            get => _gameBoardOpacity;
            set
            {
                if (_gameBoardOpacity != value)
                {
                    _gameBoardOpacity = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _gameOverOpacity = 0;
        public double GameOverOpacity
        {
            get => _gameOverOpacity;
            set
            {
                if (_gameOverOpacity != value)
                {
                    _gameOverOpacity = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _gameOverScale = 0.5;
        public double GameOverScale
        {
            get => _gameOverScale;
            set
            {
                if (_gameOverScale != value)
                {
                    _gameOverScale = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _titleOpacity = 1.0;
        public double TitleOpacity
        {
            get => _titleOpacity;
            set
            {
                if (_titleOpacity != value)
                {
                    _titleOpacity = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private double _instructionsOpacity = 0;
        public double InstructionsOpacity
        {
            get => _instructionsOpacity;
            set
            {
                if (_instructionsOpacity != value)
                {
                    _instructionsOpacity = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool _isRedActive;
        public bool IsRedActive
        {
            get => _isRedActive;
            set
            {
                if (_isRedActive != value)
                {
                    _isRedActive = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool _isGreenActive;
        public bool IsGreenActive
        {
            get => _isGreenActive;
            set
            {
                if (_isGreenActive != value)
                {
                    _isGreenActive = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool _isBlueActive;
        public bool IsBlueActive
        {
            get => _isBlueActive;
            set
            {
                if (_isBlueActive != value)
                {
                    _isBlueActive = value;
                    OnPropertyChanged();
                }
            }
        }
        
        private bool _isYellowActive;
        public bool IsYellowActive
        {
            get => _isYellowActive;
            set
            {
                if (_isYellowActive != value)
                {
                    _isYellowActive = value;
                    OnPropertyChanged();
                }
            }
        }
        
        // Commandes
        public ICommand StartGameCommand { get; }
        public ICommand RestartGameCommand { get; }
        public ICommand ColorPressedCommand { get; }
        
        
        
        
        
        // Démarre une nouvelle partie avec animation
        private async void StartGameWithAnimation()
        {
           
            
            // Animation de disparition de l'écran de démarrage
            await Task.WhenAll(
                AnimateStartScreenOpacity(),
                AnimateStartScreenTranslation()
            );
            
            IsStartScreenVisible = false;
            IsGameActive = true;
            
            // Animation d'apparition du plateau de jeu
            await Task.WhenAll(
                AnimateGameBoardScale(),
                AnimateGameBoardOpacity(),
                AnimateInstructionsOpacity()
            );
            
            // Démarrer le jeu
            StartGame();
        }
        
        // Animations individuelles
        private async Task AnimateStartScreenOpacity()
        {
            for (double i = 1.0; i >= 0; i -= 0.05)
            {
                StartScreenOpacity = i;
                await Task.Delay(25);
            }
        }
        
        private async Task AnimateStartScreenTranslation()
        {
            for (double i = 0; i >= -50; i -= 2.5)
            {
                StartScreenTranslation = i;
                await Task.Delay(25);
            }
        }
        
        private async Task AnimateGameBoardScale()
        {
            for (double i = 0.8; i <= 1.0; i += 0.01)
            {
                GameBoardScale = i;
                await Task.Delay(25);
            }
        }
        
        private async Task AnimateGameBoardOpacity()
        {
            for (double i = 0; i <= 1.0; i += 0.05)
            {
                GameBoardOpacity = i;
                await Task.Delay(25);
            }
        }
        
        private async Task AnimateInstructionsOpacity()
        {
            await Task.Delay(300); // Délai avant de commencer cette animation
            for (double i = 0; i <= 1.0; i += 0.05)
            {
                InstructionsOpacity = i;
                await Task.Delay(40);
            }
        }
        
        private async Task AnimateGameOverOpacityOut()
        {
            for (double i = 1.0; i >= 0; i -= 0.05)
            {
                GameOverOpacity = i;
                await Task.Delay(15);
            }
        }
        
        private async Task AnimateGameBoardOpacityOut()
        {
            for (double i = 1.0; i >= 0; i -= 0.05)
            {
                GameBoardOpacity = i;
                await Task.Delay(15);
            }
        }
        
        private async Task AnimateGameOverOpacityIn()
        {
            for (double i = 0; i <= 1.0; i += 0.05)
            {
                GameOverOpacity = i;
                await Task.Delay(25);
            }
        }
        
        private async Task AnimateGameOverScale()
        {
            for (double i = 0.5; i <= 1.0; i += 0.025)
            {
                GameOverScale = i;
                await Task.Delay(25);
            }
        }
        
        // Démarre une nouvelle partie
        private void StartGame()
        {
            IsGameOver = false;
            _game.StartNewGame();
        }
        
        // Redémarre le jeu après un Game Over
        private async void RestartGame()
        {
            // Jouer le son du bouton
            
            
            // Animation de disparition de l'écran Game Over
            await AnimateGameOverOpacityOut();
            
            IsGameOver = false;
            
            // Animation d'apparition du plateau de jeu
            await AnimateGameBoardOpacity();
            
            // Démarrer le jeu
            StartGame();
        }
        
        // Gère l'appui sur une couleur
        private async void ColorPressed(string colorName)
        {
            if (Enum.TryParse<SimonGame.SimonColor>(colorName, out var color))
            {
                // Jouer le son correspondant à la couleur
               
                
                // Active visuellement la couleur
                ActivateColor(color);
                
                // Envoie l'entrée au modèle
                _game.PlayerInput(color);
            }
        }
        
        // Affiche une couleur de la séquence
        public async void ShowColor(SimonGame.SimonColor color)
        {
            ;
                
                // Activer visuellement la couleur
                ActivateColor(color);
        }
        
        // Active visuellement une couleur
        private async void ActivateColor(SimonGame.SimonColor color)
        {
            switch (color)
            {
                case SimonGame.SimonColor.Red:
                    IsRedActive = true;
                    await Task.Delay(300);
                    IsRedActive = false;
                    break;
                case SimonGame.SimonColor.Green:
                    IsGreenActive = true;
                    await Task.Delay(300);
                    IsGreenActive = false;
                    break;
                case SimonGame.SimonColor.Blue:
                    IsBlueActive = true;
                    await Task.Delay(300);
                    IsBlueActive = false;
                    break;
                case SimonGame.SimonColor.Yellow:
                    IsYellowActive = true;
                    await Task.Delay(300);
                    IsYellowActive = false;
                    break;
            }
        }
        
        // Gère la fin de partie
        public async void HandleGameOver()
        {
            // Jouer le son de game over
            
            
            IsGameOver = true;
        }
        
        // Animation de Game Over
        private async void AnimateGameOver()
        {
            // Animation de disparition du plateau de jeu
            await AnimateGameBoardOpacityOut();
            
            // Animation d'apparition de l'écran Game Over
            await Task.WhenAll(
                AnimateGameOverOpacityIn(),
                AnimateGameOverScale()
            );
        }
        
        // Gère le changement de niveau
        public async void HandleLevelChanged(int level)
        {
            Level = level;
            Score = _game.Score;
            
            // Si ce n'est pas le premier niveau, jouer le son de succès
            if (level > 1)
            {
               
            }
        }
        
        // Implémentation de INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}