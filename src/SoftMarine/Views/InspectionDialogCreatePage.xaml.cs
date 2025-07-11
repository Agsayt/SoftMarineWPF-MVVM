using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class InspectionDialogCreatePage : Page
{
    public InspectionDialogCreatePage(InspectionDialogCreateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
