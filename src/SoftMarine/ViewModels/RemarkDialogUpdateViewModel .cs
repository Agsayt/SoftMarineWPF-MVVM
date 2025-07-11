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
using SoftMarineWPF_MVVM.Core.Services;
using SoftMarineWPF_MVVM.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace SoftMarineWPF_MVVM.ViewModels;

public class RemarkDialogUpdateViewModel : ObservableObject, INavigationAware
{
    public RemarkDialogUpdateViewModel(IRemarkService remarkService)
    {
        _remarkService = remarkService;
    }

    private readonly IRemarkService _remarkService;

    private ICommand _remarkUpdate;
    private string _result;

    private Remark _remark;

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

    public ICommand RemarkUpdate => _remarkUpdate ?? (_remarkUpdate = new AsyncRelayCommand(UpdateRemark));

    public async Task UpdateRemark()
    {
        _remark.Inspection.RemarkValue = ++_remark.Inspection.RemarkValue;

        var updatedRemark = new Remark()
        {
            Title = this.Title,
            DateOfElimination = this.DateOfElimination == DateTime.MinValue ? null : ((DateTime)this.DateOfElimination).ToUniversalTime(),
            Commentary = this.Commentary,
            Type = Core.Enums.RemarkType.ISO09,
            Inspection = _remark.Inspection,
        };

        var success = await _remarkService.Update(_remark.Id, updatedRemark);

        if (success)
        {
            Result = "Замечание успешно обновлено!";
        }
        else
        {
            Result = "Произошла непредвиденная ошибка!";
        }
    }

    public void OnNavigatedTo(object parameter)
    {
        _remark = parameter as Remark;

        Title = _remark.Title;
        DateOfElimination = _remark.DateOfElimination?? null;
        Commentary = _remark.Commentary;
    }

    public void OnNavigatedFrom()
    {
    }
}
