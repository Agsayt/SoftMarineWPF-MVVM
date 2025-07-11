using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class InspectionPage : Page
{
    public InspectionPage(InspectionViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
