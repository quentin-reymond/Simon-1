namespace Simon
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Utilisation de la méthode standard pour définir la page principale
            MainPage = new Simon.Views.SimonPage();
        }

        // La méthode CreateWindow n'est pas correctement implémentée dans la version actuelle de MAUI
        // Nous allons donc utiliser l'approche standard avec MainPage
    }
}