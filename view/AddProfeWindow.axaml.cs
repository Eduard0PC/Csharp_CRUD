using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Threading.Tasks;
using tema5_2.controller;
using tema5_2.Models;

namespace tema5_2.view;

public partial class AddProfeWindow : Window
{
    private MateriaController materiaController = new MateriaController();
    private ProfesorController profesorController = new ProfesorController();

    public AddProfeWindow()
    {
        InitializeComponent();
        CargarMaterias();
    }

    private void CargarMaterias()
    {
        var materias = materiaController.ObtenerMaterias();
        cbMaterias.ItemsSource = materias;
    }

    private async void Agregar_Click(object sender, RoutedEventArgs e)
    {
        var materia = cbMaterias.SelectedItem as Materia;

        if (materia == null)
        {
            await MostrarMensaje("Selecciona una materia");
            return;
        }

        var profesor = new Profesor
        {
            ID_Profe = txtId.Text ?? "",
            Nombre = txtNombre.Text ?? "",
            Apellido = txtApellido.Text ?? "",
            Email = txtEmail.Text ?? "",
            ID_Materia = materia.ID_Materia
        };

        bool resultado = profesorController.AgregarProfesor(profesor);

        if (resultado)
        {
            txtId.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtEmail.Text = "";
            cbMaterias.SelectedIndex = -1;

            await MostrarMensaje("Maestro agregado correctamente");
        }
        else
        {
            await MostrarMensaje("Error al agregar maestro");
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
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center
            }
        };

        await dialog.ShowDialog(this);
    }
}