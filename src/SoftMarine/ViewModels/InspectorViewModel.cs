using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftMarineWPF_MVVM.Contracts.Services;
using SoftMarineWPF_MVVM.Contracts.ViewModels;
using SoftMarineWPF_MVVM.Core.Contracts.Services;
using SoftMarineWPF_MVVM.Core.Models;

namespace SoftMarineWPF_MVVM.ViewModels;

public class InspectorViewModel : ObservableObject, INavigationAware
{
    private readonly IInspectorService _inspectorService;
    private readonly INavigationService _navigationService;
    private readonly IWindowManagerService _windowManagerService;

    private ICommand _toMain;
    private ICommand _deleteInspector;
    private ICommand _updateInspectorDialog;
    private ICommand _createInspectorDialog;

    private Inspector _selectedInspector;

    public Inspector SelectedInspector
    {
        get { return _selectedInspector; }
        set { SetProperty(ref _selectedInspector, value); }
    }

    public ObservableCollection<Inspector> Source { get; } = new ObservableCollection<Inspector>();

    public ICommand ToMain => _toMain ?? (_toMain = new RelayCommand(NavigateToMain));
    public ICommand DeleteInspector => _deleteInspector ?? (_deleteInspector = new AsyncRelayCommand(DeleteSelectedInspector));
    public ICommand UpdateInspectorDialog => _updateInspectorDialog ?? (_updateInspectorDialog = new AsyncRelayCommand(UpdateInspectorDialogCall));
    public ICommand CreateInspectorDialog => _createInspectorDialog ?? (_createInspectorDialog = new AsyncRelayCommand(CreateInspectorDialogCall));
    public InspectorViewModel(IInspectorService inspectionService, INavigationService navigationService, IWindowManagerService windowManagerService)
    {
        _inspectorService = inspectionService;
        _navigationService = navigationService;
        _windowManagerService = windowManagerService;

    }

    private void NavigateToMain()
    {
        _navigationService.NavigateTo(typeof(InspectionViewModel).FullName);
    }

    private async Task DeleteSelectedInspector()
    {
        var success = await _inspectorService.Deactivate(SelectedInspector.Id);

        if (success)
            Source.Remove(SelectedInspector);
    }

    private async Task UpdateInspectorDialogCall()
    {
        var result = _windowManagerService.OpenInDialog(typeof(InspectorDialogUpdateViewModel).FullName, SelectedInspector);

        await UpdatePage();
    }
    private async Task CreateInspectorDialogCall()
    {
        var result = _windowManagerService.OpenInDialog(typeof(InspectorDialogCreateViewModel).FullName);

        await UpdatePage();
    }

    private async Task UpdatePage()
    {
        Source.Clear();

        // Replace this with your actual data
        var data = await _inspectorService.GetAll();

        foreach (var item in data)
        {
            Source.Add(item);
        }
    }

    public async void OnNavigatedTo(object parameter)
    {
        await UpdatePage();
    }

    public void OnNavigatedFrom()
    {
    }
}
