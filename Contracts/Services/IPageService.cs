using System.Windows.Controls;

namespace SoftMarineWPF_MVVM.Contracts.Services;

public interface IPageService
{
    Type GetPageType(string key);

    Page GetPage(string key);
}
