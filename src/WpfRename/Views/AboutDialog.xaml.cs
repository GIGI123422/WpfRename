using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;
using WpfRename.Resources;

namespace WpfRename.Views;

/// <summary>
/// AboutDialog.xaml에 대한 상호 작용 논리
/// </summary>
public partial class AboutDialog : Wpf.Ui.Controls.ContentDialog
{
    public string VersionText { get; }

    public AboutDialog()
    {
        InitializeComponent();

        // 버전 정보 가져오기
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        VersionText = string.Format(Strings.AboutVersion, version?.ToString(3) ?? "1.0.0");

        DataContext = this;
    }

    /// <summary>
    /// 하이퍼링크 클릭 처리
    /// </summary>
    private void OnHyperlinkNavigate(object sender, RequestNavigateEventArgs e)
    {
        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = e.Uri.AbsoluteUri,
                UseShellExecute = true
            });
            e.Handled = true;
        }
        catch
        {
            // 브라우저 실행 실패 무시
        }
    }
}
