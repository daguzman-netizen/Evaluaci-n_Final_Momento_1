
namespace Evaluación_Final_Momento_1.Views;

public partial class LoginPage : ContentPage
{
    private readonly string[,] usuarios =
    {
        { "estudiante", "moviles" },
        { "suda", "2025" }
    };

    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnIniciarSesion_Clicked(object? sender, EventArgs e)
    {
        string usuario = txtUsuario.Text?.Trim() ?? "";
        string contrasena = txtContrasena.Text ?? "";

        bool accesoPermitido = false;

        for (int i = 0; i < usuarios.GetLength(0); i++)
        {
            if (usuario == usuarios[i, 0] &&
                contrasena == usuarios[i, 1])
            {
                accesoPermitido = true;
                break;
            }
        }

        if (accesoPermitido)
        {
            await DisplayAlertAsync(
                "Bienvenido",
                "Inicio de sesión correcto.",
                "Continuar");

            await Navigation.PushAsync(new RegistroPage(usuario));
        }
        else
        {
            await DisplayAlertAsync(
                "Error",
                "Usuario o contraseña incorrectos.",
                "Aceptar");
        }
    }
}
