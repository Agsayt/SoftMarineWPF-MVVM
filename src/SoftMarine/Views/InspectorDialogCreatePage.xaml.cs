using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class InspectorDialogCreatePage : Page
{
    public InspectorDialogCreatePage(InspectorDialogCreateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
