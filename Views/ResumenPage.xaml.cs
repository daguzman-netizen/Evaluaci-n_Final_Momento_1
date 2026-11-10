
using Evaluación_Final_Momento_1.Models;

namespace Evaluación_Final_Momento_1.Views;

public partial class ResumenPage : ContentPage
{
    public ResumenPage(Estudiante estudiante)
    {
        InitializeComponent();

        lblUsuario.Text = $"Usuario conectado: {estudiante.Usuario}";

        lblNombre.Text = estudiante.Nombre;
        lblApellido.Text = estudiante.Apellido;
        lblEdad.Text = estudiante.Edad.ToString();

        lblFecha.Text = estudiante.Fecha.ToString("dd/MM/yyyy");
        lblCiudad.Text = estudiante.Ciudad;
        lblPais.Text = estudiante.Pais;

        lblMontoInicial.Text = $"${estudiante.MontoInicial:F2}";
        lblPagoMensual.Text = $"${estudiante.PagoMensual:F2}";
        lblPagoTotal.Text = $"${estudiante.PagoTotal:F2}";
    }

    private async void BtnVolver_Clicked(object? sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}
