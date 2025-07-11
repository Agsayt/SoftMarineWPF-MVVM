using System.Windows.Controls;

using SoftMarineWPF_MVVM.ViewModels;

namespace SoftMarineWPF_MVVM.Views;

public partial class InspectorPage : Page
{
    public InspectorPage(InspectorViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
