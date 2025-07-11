using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class InspectionDialogUpdatePage : Page
{
    public InspectionDialogUpdatePage(InspectionDialogUpdateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
