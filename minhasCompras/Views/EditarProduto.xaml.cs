using minhasCompras.Models;

namespace minhasCompras.Views;

public partial class EditarProduto : ContentPage
{
    // 1. Mudamos o construtor para receber o Produto clicado
    public EditarProduto(Produto p)
    {
        InitializeComponent();

        // 2. Isso preenche a tela automaticamente (se o XAML estiver com Binding)
        BindingContext = p;
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
      
            Produto produto_anexado = BindingContext as Produto;

           
            Produto p = new Produto
            {
                Id = produto_anexado.Id,
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
            };

          
            await App.Db.Update(p);
            await DisplayAlert("Sucesso!", "Registro Atualizado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message ?? "Erro ao salvar", "OK");
        }
    }
}