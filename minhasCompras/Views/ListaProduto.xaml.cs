using minhasCompras.Models;
using System.Collections.ObjectModel;

namespace minhasCompras.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto> { };

    public ListaProduto()
    {
        InitializeComponent();
        lst_produtos.ItemsSource = lista;
    }

    // MANTIVEMOS APENAS ESTE OnAppearing (O Corrigido!)
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            lista.Clear();
            List<Produto> tmp = await App.Db.GetAll();
            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message ?? "Erro ao carregar", "OK");
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            await Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string q = e.NewTextValue;
            lista.Clear();

            List<Produto> tmp = await App.Db.Search(q);

            tmp.ForEach(i => lista.Add(i));
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "OK");
        }
    }

    private async void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"o total é {soma:C}";

        await DisplayAlert("total dos produtos", msg ?? "", "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            if (sender is MenuItem selecionado && selecionado.BindingContext is Produto p)
            {
                bool confirm = await DisplayAlert("Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

                if (confirm)
                {
                    await App.Db.Delete(p.Id);
                    lista.Remove(p);
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message, "OK");
        }
    }

    private async void lst_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
        {
            if (e.SelectedItem == null) return;

            Produto p = e.SelectedItem as Produto;

            await Navigation.PushAsync(new Views.EditarProduto(p));

            lst_produtos.SelectedItem = null;
        }
        catch (Exception ex)
        {
            await DisplayAlert("ops", ex.Message ?? "Erro ao abrir", "OK");
        }
    }
}