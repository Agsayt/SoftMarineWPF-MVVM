using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;


namespace SoftMarineWPF_MVVM.ViewModels;

public class ShellViewModel : ObservableObject
{
    private ICommand _loadedCommand;
    private ICommand _unloadedCommand;

    public ICommand LoadedCommand => _loadedCommand ?? (_loadedCommand = new RelayCommand(OnLoaded));

    public ICommand UnloadedCommand => _unloadedCommand ?? (_unloadedCommand = new RelayCommand(OnUnloaded));

    public ShellViewModel()
    {

    }

    private void OnLoaded()
    {
    }

    private void OnUnloaded()
    {
    }
}
