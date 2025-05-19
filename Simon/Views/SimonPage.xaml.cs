using Simon.ViewModels;

namespace Simon.Views
{
    public partial class SimonPage : ContentPage
    {
        public SimonPage(SimonViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
        
        // Constructeur pour le design time
        public SimonPage()
        {
            InitializeComponent();
            BindingContext = new SimonViewModel();
        }
    }
}