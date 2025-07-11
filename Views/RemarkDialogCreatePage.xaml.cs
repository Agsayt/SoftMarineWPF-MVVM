using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class RemarkDialogCreatePage : Page
{
    public RemarkDialogCreatePage(RemarkDialogCreateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
