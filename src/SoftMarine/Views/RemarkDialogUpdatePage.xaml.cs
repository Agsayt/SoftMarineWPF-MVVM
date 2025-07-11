using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class RemarkDialogUpdatePage : Page
{
    public RemarkDialogUpdatePage(RemarkDialogUpdateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
