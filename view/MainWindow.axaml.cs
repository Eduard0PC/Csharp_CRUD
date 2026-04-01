using Avalonia.Controls;
using Avalonia.Interactivity;
using System;

namespace tema5_2.view;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void VerMaterias(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Ver Materias");
        var ventana = new VerMateriaWindow();
        ventana.Show();
    }

    private void VerMaestros(object sender, RoutedEventArgs e)
    {
        Console.WriteLine("Ver Maestros");
        var ventana = new VerProfeWindow();
        ventana.Show();
    }
}