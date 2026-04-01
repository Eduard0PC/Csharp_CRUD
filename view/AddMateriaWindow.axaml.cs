using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using tema5_2.controller;
using tema5_2.Models;

namespace tema5_2.view;

public partial class AddMateriaWindow : Window
{
    private MateriaController materiaController = new MateriaController();

    public AddMateriaWindow()
    {
        InitializeComponent();
    }

    private async void Agregar_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtId.Text) || 
            string.IsNullOrWhiteSpace(txtNombre.Text) || 
            string.IsNullOrWhiteSpace(txtCreditos.Text))
        {
            await MostrarMensaje("Todos los campos son obligatorios");
            return;
        }

        if (!int.TryParse(txtCreditos.Text, out int creditos))
        {
            await MostrarMensaje("Los créditos deben ser un número entero");
            return;
        }

        var materia = new Materia
        {
            ID_Materia = txtId.Text,
            Nombre_Materia = txtNombre.Text,
            Creditos = creditos
        };

        bool resultado = materiaController.AgregarMateria(materia);

        if (resultado)
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtCreditos.Text = "";

            await MostrarMensaje("Materia agregada correctamente");
        }
        else
        {
            await MostrarMensaje("Error al agregar materia");
        }
    }

    private async Task MostrarMensaje(string mensaje)
    {
        var dialog = new Window
        {
            Width = 300,
            Height = 150,
            Content = new TextBlock
            {
                Text = mensaje,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                TextWrapping = Avalonia.Media.TextWrapping.Wrap
            },
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        await dialog.ShowDialog(this);
    }
}
