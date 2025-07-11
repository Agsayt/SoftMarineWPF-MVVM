using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class InspectorDialogUpdatePage : Page
{
    public InspectorDialogUpdatePage(InspectorDialogUpdateViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
