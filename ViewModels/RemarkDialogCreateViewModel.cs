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

public class RemarkDialogCreateViewModel : ObservableObject, INavigationAware
{
    public RemarkDialogCreateViewModel(IRemarkService remarkService)
    {
        _remarkService = remarkService;
    }

    private readonly IRemarkService _remarkService;

    private ICommand _remarkCreation;
    private string _result;

    private Inspection _inspection;

    private string _title;
    private DateTime? _dayOfElimination;
    private string _commentary;

    public string Title
    {
        get { return _title; }
        set
        {
            SetProperty(ref _title, value);
        }
    }

    public DateTime? DateOfElimination
    {
        get { return _dayOfElimination; }
        set { SetProperty(ref _dayOfElimination, value); }
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

    public ICommand RemarkCreation => _remarkCreation ?? (_remarkCreation = new AsyncRelayCommand(CreateRemark));

    public async Task CreateRemark()
    {
        var newRemark = new Remark()
        {
            Title = this.Title,
            DateOfElimination = this.DateOfElimination is null ? null : ((DateTime)this.DateOfElimination).ToUniversalTime(),
            Commentary = this.Commentary,
            Type = Core.Enums.RemarkType.ISO09,
            Inspection = _inspection,
        };

        var guid = await _remarkService.Create(newRemark);

        if(guid != Guid.Empty)
        {
            Result = "Замечание успешно добавлено!";
        }
        else
        {
            Result = "Произошла непредвиденная ошибка!";
        }
    }

    public void OnNavigatedTo(object parameter)
    {
        _inspection = parameter as Inspection;
    }

    public void OnNavigatedFrom()
    {
    }
}
