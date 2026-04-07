using Avalonia.Controls;
using tema5_2.controller;
using tema5_2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace tema5_2.view;

public partial class VerProfeWindow : Window
{
    public List<Materia> Materias { get; set; } = new List<Materia>();
    private List<Profesor> lista = new List<Profesor>();

    public VerProfeWindow()
    {
        InitializeComponent();

        var materiaController = new MateriaController();
        Materias = materiaController.ObtenerMaterias();
        DataContext = this;
        CargarDatos();
    }

    private void CargarDatos()
    {
        var controller = new ProfesorController();
        lista = controller.ObtenerProfesores();

        dataGridMaestros.ItemsSource = lista;
    }

    private async void Eliminar_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var profesorSeleccionado = dataGridMaestros.SelectedItem as Profesor;

        if (profesorSeleccionado == null)
        {
            await MostrarMensaje("Selecciona un maestro para eliminar");
            return;
        }

        var controller = new ProfesorController();

        bool eliminado = controller.EliminarProfesor(profesorSeleccionado.ID_Profe);

        if (eliminado)
        {
            CargarDatos();
            await MostrarMensaje("Maestro eliminado correctamente");
        }
        else
        {
            await MostrarMensaje("Error al eliminar maestro");
        }
    }
    private async void GuardarCambios_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var listaLocal = dataGridMaestros.ItemsSource as List<Profesor>;

        if (listaLocal == null) return;

        foreach (var p in listaLocal)
        {
            if (string.IsNullOrWhiteSpace(p.ID_Profe) || 
                string.IsNullOrWhiteSpace(p.Nombre) || 
                string.IsNullOrWhiteSpace(p.Apellido) || 
                string.IsNullOrWhiteSpace(p.Email))
            {
                await MostrarMensaje("Todos los campos son obligatorios en los maestros");
                return;
            }

            if (!p.Email.Contains("@") || !p.Email.Contains("."))
            {
                await MostrarMensaje($"El correo electrónico de {p.Nombre} no es válido");
                return;
            }

            if (string.IsNullOrWhiteSpace(p.ID_Materia))
            {
                await MostrarMensaje($"Debe asignar una materia al maestro {p.Nombre}");
                return;
            }
        }

        var controller = new ProfesorController();

        int actualizados = listaLocal.Count(p => controller.ActualizarProfesor(p));
        int errores = listaLocal.Count - actualizados;

        await MostrarMensaje($"Cambios guardados. Actualizados: {actualizados}, Errores: {errores}");
        CargarDatos();
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

    private async void Agregar_Click(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var ventana = new AddProfeWindow();

        await ventana.ShowDialog(this); 

        CargarDatos(); 
    }

    private void Buscar(object sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        string texto = txtBuscar.Text?.ToLower() ?? "";

        var filtrados = lista.Where(p =>
            (p.Nombre != null && p.Nombre.ToLower().Contains(texto)) ||
            (p.Apellido != null && p.Apellido.ToLower().Contains(texto)) ||
            (p.Email != null && p.Email.ToLower().Contains(texto))
        ).ToList();

        dataGridMaestros.ItemsSource = filtrados;
    }
}