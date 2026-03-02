using Microsoft.Extensions.DependencyInjection;

namespace minhasCompras
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.ListaProduto());
        }

       
        
    }
}