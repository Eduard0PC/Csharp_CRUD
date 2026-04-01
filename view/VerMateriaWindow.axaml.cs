using Avalonia.Controls;
using Avalonia.Interactivity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tema5_2.controller;
using tema5_2.Models;

namespace tema5_2.view;

public partial class VerMateriaWindow : Window
{
    private MateriaController materiaController = new MateriaController();
    private List<Materia> todasLasMaterias = new List<Materia>();

    public VerMateriaWindow()
    {
        InitializeComponent();
        CargarMaterias();
    }

    private void CargarMaterias()
    {
        todasLasMaterias = materiaController.ObtenerMaterias();
        dataGridMaterias.ItemsSource = todasLasMaterias;
    }

    private void Buscar(object? sender, Avalonia.Controls.TextChangedEventArgs e)
    {
        var busqueda = txtBuscar.Text?.ToLower() ?? "";
        
        if (string.IsNullOrWhiteSpace(busqueda))
        {
            dataGridMaterias.ItemsSource = todasLasMaterias;
            return;
        }

        var filtradas = todasLasMaterias.Where(m => 
            m.Nombre_Materia.ToLower().Contains(busqueda) || 
            m.ID_Materia.ToLower().Contains(busqueda)
        ).ToList();

        dataGridMaterias.ItemsSource = filtradas;
    }

    private void Agregar_Click(object sender, RoutedEventArgs e)
    {
        var ventanaAdd = new AddMateriaWindow();
        ventanaAdd.Closed += (s, args) => CargarMaterias();
        ventanaAdd.ShowDialog(this);
    }

    private async void Eliminar_Click(object sender, RoutedEventArgs e)
    {
        var materiaSeleccionada = dataGridMaterias.SelectedItem as Materia;

        if (materiaSeleccionada == null)
        {
            await MostrarMensaje("Selecciona una materia para eliminar");
            return;
        }

        bool resultado = materiaController.EliminarMateria(materiaSeleccionada.ID_Materia);

        if (resultado)
        {
            await MostrarMensaje("Materia eliminada correctamente");
            CargarMaterias();
        }
        else
        {
            await MostrarMensaje("Error al eliminar la materia");
        }
    }

    private async void GuardarCambios_Click(object sender, RoutedEventArgs e)
    {
        int actualizados = 0;
        int errores = 0;

        foreach (var materia in todasLasMaterias)
        {
            bool resultado = materiaController.ActualizarMateria(materia);
            if (resultado) actualizados++;
            else errores++;
        }

        await MostrarMensaje($"Cambios guardados. Actualizados: {actualizados}, Errores: {errores}");
        CargarMaterias();
    }

    private async Task MostrarMensaje(string mensaje)
    {
        var dialog = new Window
        {
            Width = 350,
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
