
using Evaluación_Final_Momento_1.Models;

namespace Evaluación_Final_Momento_1.Views;

public partial class RegistroPage : ContentPage
{
    private readonly string usuarioConectado;

    private const decimal COSTO_CURSO = 1500m;
    private const decimal PORCENTAJE_RECARGO = 0.04m;
    private const int NUMERO_CUOTAS = 4;

    public RegistroPage(string usuario)
    {
        InitializeComponent();

        usuarioConectado = usuario;
        lblUsuario.Text = $"Usuario conectado: {usuarioConectado}";

        pkPais.ItemsSource = new List<string>
        {
            "Ecuador",
            "Colombia",
            "Perú",
            "Argentina"
        };

        dpFecha.Date = DateTime.Today;
    }

    private void PkPais_SelectedIndexChanged(object? sender, EventArgs e)
    {
        pkCiudad.ItemsSource = null;

        switch (pkPais.SelectedItem?.ToString())
        {
            case "Ecuador":
                pkCiudad.ItemsSource = new List<string>
                {
                    "Cuenca", "Quito", "Guayaquil", "Loja"
                };
                break;

            case "Colombia":
                pkCiudad.ItemsSource = new List<string>
                {
                    "Bogotá", "Medellín", "Cali", "Cartagena"
                };
                break;

            case "Perú":
                pkCiudad.ItemsSource = new List<string>
                {
                    "Lima", "Cusco", "Arequipa", "Trujillo"
                };
                break;

            case "Argentina":
                pkCiudad.ItemsSource = new List<string>
                {
                    "Buenos Aires", "Córdoba", "Rosario", "Mendoza"
                };
                break;
        }
    }

    private void TxtMontoInicial_TextChanged(
        object? sender,
        TextChangedEventArgs e)
    {
        txtPagoMensual.Text = "";

        if (decimal.TryParse(e.NewTextValue, out decimal monto))
        {
            if (monto >= 0 && monto < COSTO_CURSO)
            {
                decimal saldo = COSTO_CURSO - monto;
                decimal cuotaBase = saldo / NUMERO_CUOTAS;
                decimal recargo = COSTO_CURSO * PORCENTAJE_RECARGO;

                decimal pagoMensual = cuotaBase + recargo;

                txtPagoMensual.Text = pagoMensual.ToString("F2");
            }
        }
    }

    private async void BtnVerResumen_Clicked(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellido.Text))
        {
            await DisplayAlertAsync(
                "Atención",
                "Ingrese el nombre y apellido del estudiante.",
                "Aceptar");
            return;
        }

        if (!int.TryParse(txtEdad.Text, out int edad) ||
            edad <= 0 || edad > 120)
        {
            await DisplayAlertAsync(
                "Atención",
                "Ingrese una edad válida.",
                "Aceptar");
            return;
        }

        if (pkPais.SelectedIndex == -1 ||
            pkCiudad.SelectedIndex == -1)
        {
            await DisplayAlertAsync(
                "Atención",
                "Seleccione el país y la ciudad.",
                "Aceptar");
            return;
        }

        if (!decimal.TryParse(txtMontoInicial.Text, out decimal montoInicial) ||
            montoInicial < 0 ||
            montoInicial >= COSTO_CURSO)
        {
            await DisplayAlertAsync(
                "Atención",
                "El monto inicial debe ser mayor o igual a 0 y menor a 1500.",
                "Aceptar");
            return;
        }

        decimal saldo = COSTO_CURSO - montoInicial;
        decimal recargo = COSTO_CURSO * PORCENTAJE_RECARGO;
        decimal pagoMensual = (saldo / NUMERO_CUOTAS) + recargo;

        Estudiante estudiante = new Estudiante
        {
            Usuario = usuarioConectado,
            Nombre = txtNombre.Text.Trim(),
            Apellido = txtApellido.Text.Trim(),
            Edad = edad,
            Fecha = (DateTime)dpFecha.Date,

            Pais = pkPais.SelectedItem is string pais ? pais : "",
            Ciudad = pkCiudad.SelectedItem is string ciudad ? ciudad : "",

            MontoInicial = montoInicial,
            PagoMensual = pagoMensual
        };

        await Navigation.PushAsync(new ResumenPage(estudiante));
    }
}
