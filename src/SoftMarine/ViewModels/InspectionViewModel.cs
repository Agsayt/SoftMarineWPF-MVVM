using System.Collections.ObjectModel;
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

public class InspectionViewModel : ObservableObject, INavigationAware
{
    public InspectionViewModel(IInspectionService inspectionService,
                               INavigationService navigationService,
                               IInspectorService inspectorService,
                               IWindowManagerService windowManagerService,
                               IRemarkService remarkService)
    {
        _inspectionService = inspectionService;
        _navigationService = navigationService;
        _inspectorService = inspectorService;
        _windowManagerService = windowManagerService;
        _remarkService = remarkService;
        InspectionCollection = CollectionViewSource.GetDefaultView(Source);

        InspectionCollection.Filter = Filter;
    }

    private readonly IInspectionService _inspectionService;
    private readonly INavigationService _navigationService;
    private readonly IInspectorService _inspectorService;
    private readonly IWindowManagerService _windowManagerService;
    private readonly IRemarkService _remarkService;

    private Inspection _selectedInspection;
    private Inspector _selectedInspector;
    private Remark _selectedRemark;

    private ICommand _toInspectorsCommand;
    private ICommand _inspectionCreation;
    private ICommand _deletingInspection;
    private ICommand _updatingInspection;
    private ICommand _remarkCreation;
    private ICommand _deletingRemark;
    private ICommand _updatingRemark;

    private ICollectionView _inspectionCollection;

    private Inspector _clearInspector;

    private string _name;

    public Inspector SelectedInspector
    {
        get { return _selectedInspector; }
        set
        {
            SetProperty(ref _selectedInspector, value);
            InspectionCollection.Refresh();
        }
    }

    public string NameFilter
    {
        get { return _name; }
        set
        {
            SetProperty(ref _name, value);
            InspectionCollection.Refresh();
        }
    }

    public Inspection SelectedInspection
    {
        get { return _selectedInspection; }
        set { SetProperty(ref _selectedInspection, value); }
    }

    public Remark SelectedRemark
    {
        get { return _selectedRemark; }
        set { SetProperty(ref _selectedRemark, value); }
    }
    
    public ICollectionView InspectionCollection
    {
        get { return _inspectionCollection; }
        set { SetProperty(ref _inspectionCollection, value); }
    }

    public ICommand ToInspectors => _toInspectorsCommand ?? (_toInspectorsCommand = new RelayCommand(NavigateToInspectors));

    public ICommand InspectionCreation => _inspectionCreation ?? (_inspectionCreation = new AsyncRelayCommand(CreateInspection));
    public ICommand UpdatingInspection => _updatingInspection ?? (_updatingInspection = new AsyncRelayCommand(UpdateInspection));
    public ICommand DeletingInspection => _deletingInspection ?? (_deletingInspection = new AsyncRelayCommand(DeleteInspection));

    public ICommand RemarkCreation => _remarkCreation ?? (_remarkCreation = new AsyncRelayCommand(CreateRemark));
    public ICommand UpdatingRemark => _updatingRemark ?? (_updatingRemark = new AsyncRelayCommand(UpdateRemark));
    public ICommand DeletingRemark => _deletingRemark ?? (_deletingRemark = new AsyncRelayCommand(DeleteRemark));

    public ObservableCollection<Inspection> Source { get; } = new ObservableCollection<Inspection>();
    public ObservableCollection<Inspector> InspectorSource { get; } = new ObservableCollection<Inspector>();


    private async Task CreateInspection()
    {
        var result = _windowManagerService.OpenInDialog(typeof(InspectionDialogCreateViewModel).FullName);

        await UpdatePage();
    }
    private void NavigateToInspectors()
    {

        _navigationService.NavigateTo(typeof(InspectorViewModel).FullName);
    }

    private async Task UpdateInspection()
    {
        var result = _windowManagerService.OpenInDialog(typeof(InspectionDialogUpdateViewModel).FullName, SelectedInspection);

        await UpdatePage();
    }
    private async Task DeleteInspection()
    {
        var resutl = await _inspectionService.Remove(SelectedInspection.Id);

        await UpdatePage();
    }

    private async Task CreateRemark()
    {
        var result = _windowManagerService.OpenInDialog(typeof(RemarkDialogCreateViewModel).FullName, SelectedInspection);

        await UpdatePage();
    }

    private async Task UpdateRemark()
    {
        var result = _windowManagerService.OpenInDialog(typeof(RemarkDialogUpdateViewModel).FullName, SelectedRemark);


        await UpdatePage();
    }

    private async Task DeleteRemark()
    {
        var result = await _remarkService.Remove(SelectedRemark.Id);

        await UpdatePage();
    }

    private bool Filter(object filterObject)
    {
        var inspection = filterObject as Inspection;

        if (SelectedInspector is not null && SelectedInspector == _clearInspector)
            return true;
        else if (SelectedInspector is not null && string.IsNullOrWhiteSpace(NameFilter))
            return inspection.Inspector == SelectedInspector;
        else if (SelectedInspector is not null && !string.IsNullOrWhiteSpace(NameFilter))
            return inspection.Name.Contains(NameFilter) && inspection.Inspector == SelectedInspector;
        else if (SelectedInspector is not null && SelectedInspector == _clearInspector && !string.IsNullOrWhiteSpace(NameFilter))
            return inspection.Name.Contains(NameFilter);
        else if (!string.IsNullOrWhiteSpace(NameFilter))
            return inspection.Name.Contains(NameFilter);

        else return true;
    }

    
    public async Task UpdatePage()
    {
        Source.Clear();
        InspectorSource.Clear();

        // Replace this with your actual data
        var inspections = await _inspectionService.GetInspectionsWithRemarks();

        foreach (var item in inspections)
        {
            Source.Add(item);
        }

        if (Source.Count > 0)
            SelectedInspection = Source.First();

        var inspectors = await _inspectorService.GetAll();

        _clearInspector = new Inspector()
        {
            LastName = "Все",
            FirstName = " ",
            UniqueNumber = 0
        };

        InspectorSource.Add(_clearInspector);

        foreach (var item in inspectors)
        {
            InspectorSource.Add(item);
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
