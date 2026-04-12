using minhasCompras.Models;
using System.Collections.ObjectModel;

namespace minhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista_produtos = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista_produtos;
    }

    protected override async void OnAppearing()
    {
        var tmp = await App.Db.GetAll();
        lista_produtos.Clear();
        tmp.ForEach(i => lista_produtos.Add(i));
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new NovoProduto());
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var btn = (ImageButton)sender;
        int id = (int)btn.CommandParameter;

        if (await DisplayAlert("Confirmação", "Deseja excluir?", "Sim", "Não"))
        {
            await App.Db.Delete(id);
            OnAppearing(); // Atualiza a lista
        }
    }

    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        var tmp = await App.Db.Search(txt_procurar.Text);
        lista_produtos.Clear();
        tmp.ForEach(i => lista_produtos.Add(i));
    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista_produtos.Sum(i => i.Total);
        await DisplayAlert("Total da Lista", $"O valor total é {soma:C}", "OK");
    }

    private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var p = (minhasCompras.Models.Produto)e.SelectedItem;

        if (p != null)
        {
            // Aqui passamos o 'p' para resolver o erro CS7036
            await Navigation.PushAsync(new EditarProduto(p));
        }
    }
}