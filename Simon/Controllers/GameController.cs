using Simon.Models;
using Simon.ViewModels;

namespace Simon.Controllers
{
    public class GameController
    {
        private SimonGame _model;
        private SimonViewModel _viewModel;
        
        public GameController(SimonViewModel viewModel)
        {
            _viewModel = viewModel;
            _model = new SimonGame();
            
            // Connecter le modèle et le ViewModel
            _model.OnShowColor += _viewModel.ShowColor;
            _model.OnGameOver += _viewModel.HandleGameOver;
            _model.OnLevelChanged += _viewModel.HandleLevelChanged;
        }
        
        public void StartGame()
        {
            _model.StartNewGame();
        }
        
        public void HandleColorPress(SimonGame.SimonColor color)
        {
            _model.PlayerInput(color);
        }
    }
}