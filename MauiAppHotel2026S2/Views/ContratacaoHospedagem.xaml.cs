using MauiAppHotel2026S2.Models;

namespace MauiAppHotel2026S2.Views;

public partial class ContratacaoHospedagem : ContentPage
{
    // Lista de suítes (array de objetos com tamanho dinâmico, como na Agenda 14)
    List<Quarto> lista_quartos = new()
    {
        new Quarto { Nome = "Suíte Master",   ValorAdultos = 225.5,  ValorCriancas = 200.3 },
        new Quarto { Nome = "Suíte Premium",  ValorAdultos = 180.30, ValorCriancas = 110.30 },
        new Quarto { Nome = "Suíte Viajante", ValorAdultos = 140,    ValorCriancas = 75 },
        new Quarto { Nome = "Suíte Crise",    ValorAdultos = 100.3,  ValorCriancas = 50.0 }
    };

    public ContratacaoHospedagem()
    {
        InitializeComponent();

        pck_quarto.ItemsSource = lista_quartos;

        // Validação das datas: check-in de hoje até 3 meses, check-out sempre depois do check-in
        dtpck_checkin.MinimumDate = DateTime.Today;
        dtpck_checkin.MaximumDate = DateTime.Today.AddMonths(3);

        dtpck_checkout.MinimumDate = DateTime.Today.AddDays(1);
        dtpck_checkout.MaximumDate = DateTime.Today.AddMonths(4);
    }

    private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
    {
        dtpck_checkout.MinimumDate = e.NewDate?.AddDays(1);
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Evita erro quando nenhuma suíte foi escolhida
            if (pck_quarto.SelectedItem is not Quarto quarto)
            {
                await DisplayAlertAsync("Atenção", "Selecione uma suíte para continuar.", "OK");
                return;
            }

            Hospedagem h = new()
            {
                QntAdultos = Convert.ToInt32(stp_adultos.Value),
                QntCriancas = Convert.ToInt32(stp_criancas.Value),
                QuartoEscolhido = quarto,
                DataCheckin = dtpck_checkin.Date.Value,
                DataCheckout = dtpck_checkout.Date.Value,
            };

            await Navigation.PushAsync(new HospedagemContratada
            {
                BindingContext = h,
            });
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops!", ex.Message, "OK");
        }
    }
}
