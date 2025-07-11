using System.Windows.Controls;

using MahApps.Metro.Controls;


namespace SoftMarineWPF_MVVM.Contracts.Views;

public interface IShellWindow
{
    Frame GetNavigationFrame();

    void ShowWindow();

    void CloseWindow();

}
