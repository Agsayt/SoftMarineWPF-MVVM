﻿using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SoftMarineWPF_MVVM.Contracts.Services;
using SoftMarineWPF_MVVM.Contracts.ViewModels;
using SoftMarineWPF_MVVM.Contracts.Views;
using SoftMarineWPF_MVVM.Core.Contracts.Services;
using SoftMarineWPF_MVVM.Core.Models;
using SoftMarineWPF_MVVM.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SoftMarineWPF_MVVM.ViewModels;

public class InspectionDialogCreateViewModel : ObservableObject, INavigationAware
{
    public InspectionDialogCreateViewModel(IInspectionService inspectionService,
                               IInspectorService inspectorService)
    {
        _inspectionService = inspectionService;
        _inspectorService = inspectorService;
    }

    private readonly IInspectionService _inspectionService;
    private readonly IInspectorService _inspectorService;

    private ICommand _inspectionCreation;
    private string _result;

    private Inspector _inspector;
    private Inspector _clearInspector;

    private string _name;
    private DateTime _inspectionDate = DateTime.UtcNow;
    private string _commentary;

    public Inspector SelectedInspector
    {
        get { return _inspector; }
        set
        {
            SetProperty(ref _inspector, value);
        }
    }
    public string Name
    {
        get { return _name; }
        set
        {
            SetProperty(ref _name, value);
        }
    }

    public DateTime InspectionDate
    {
        get { return _inspectionDate; }
        set { SetProperty(ref _inspectionDate, value); }
    }

    public string Commentary
    {
        get { return _commentary; }
        set { SetProperty(ref _commentary, value); }
    }

    public string Result
    {
        get { return _result; }
        set { SetProperty(ref _result, value); }
    }

    public ICommand InspectionCreation => _inspectionCreation ?? (_inspectionCreation = new AsyncRelayCommand(CreateInspection));
    public ObservableCollection<Inspector> InspectorSource { get; } = new ObservableCollection<Inspector>();

    public async Task CreateInspection()
    {
        var newInspection = new Inspection()
        {
            Name = this.Name,
            Date = InspectionDate.ToUniversalTime(),
            Inspector = SelectedInspector,
            Commentary = Commentary ?? null,
            Type = Core.Enums.InspectionType.Main, // Лучше в целом будет, возможно, использовать заранее определённые типы инспекций
            Remarks = new(),
            RemarkValue = 0,
        };

        var guid = await _inspectionService.Create(newInspection);

        if(guid != Guid.Empty)
        {
            Result = "Инспекция успешно добавлена!";
        }
        else
        {
            Result = "Произошла непредвиденная ошибка!";
        }
    }

    public async void OnNavigatedTo(object parameter)
    {
        var inspectors = await _inspectorService.GetAll();

        InspectorSource.Add(_clearInspector);

        foreach (var item in inspectors)
        {
            InspectorSource.Add(item);
        }

    }

    public void OnNavigatedFrom()
    {
    }
}
