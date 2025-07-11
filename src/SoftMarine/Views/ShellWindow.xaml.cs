using System.Windows;
using System.Windows.Controls;

using MahApps.Metro.Controls;

using SoftMarineWPF_MVVM.Contracts.Services;
using SoftMarineWPF_MVVM.Contracts.Views;
using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class ShellWindow : MetroWindow, IShellWindow
{
    public ShellWindow(IPageService pageService, ShellViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    public Frame GetNavigationFrame()
        => shellFrame;


    public void ShowWindow()
        => Show();

    public void CloseWindow()
        => Close();

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
    }

}
