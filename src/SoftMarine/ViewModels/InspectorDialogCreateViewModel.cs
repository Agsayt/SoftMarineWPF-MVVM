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

public class InspectorDialogCreateViewModel : ObservableObject, INavigationAware
{
    public InspectorDialogCreateViewModel(IInspectorService inspectorService)
    {
        _inspectorService = inspectorService;
    }

    private readonly IInspectorService _inspectorService;

    private ICommand _inspectorCommand;

    private Inspector _existedInspector;
    private string _lastName;
    private string _firstName;
    private string _middleName;
    private int _uniqueNumber;

    private string _title;
    private string _result;
    public Inspector ExistedInspector
    {
        get { return _existedInspector; }
        set { SetProperty(ref _existedInspector, value); }
    }
    public string LastName 
    {
        get { return _lastName; }
        set { SetProperty(ref _lastName, value); } 
    }
    public string FirstName 
    {
        get { return _firstName; }
        set { SetProperty(ref _firstName, value); } 
    }
    public string MiddleName 
    {
        get { return _middleName; }
        set { SetProperty(ref _middleName, value); } 
    }

    public int UniqueNumber 
    {
        get { return _uniqueNumber; }
        set { SetProperty(ref _uniqueNumber, value); } 
    }

    public string Title
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }

    public string Result
    {
        get { return _result; }
        set { SetProperty(ref _result, value); }
    }

    public ICommand InspectorCommand => _inspectorCommand ?? (_inspectorCommand = new AsyncRelayCommand(Create));

    private async Task Create()
    {
        var newInspector = new Inspector()
        {
            LastName = this.LastName,
            FirstName = this.FirstName,
            MiddleName = this.MiddleName ?? null,
            UniqueNumber = this.UniqueNumber,
            IsActive = true,
        };

        var guid = await _inspectorService.Create(newInspector);

        if (guid != Guid.Empty)
        {
            Result = "Инспектор успешно добавлен!";

        }
        else
        {
            Result = "Произошла непредвиденная ошибка!";
        }
    }
    

    public async void OnNavigatedTo(object parameter)
    {
        Title = "Добавление инспектора";

    }

    public void OnNavigatedFrom()
    {
    }
}
