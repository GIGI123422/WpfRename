using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using WpfRename.ViewModels;
using WpfRename.Resources;
using WpfRename.Views;

namespace WpfRename;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Wpf.Ui.Controls.FluentWindow
{
    public MainViewModel ViewModel { get; }

    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        DataContext = ViewModel;
    }

    /// <summary>
    /// 드래그 진입 시 처리
    /// </summary>
    private void OnDragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effects = DragDropEffects.Copy;
        }
        else
        {
            e.Effects = DragDropEffects.None;
        }
        e.Handled = true;
    }

    /// <summary>
    /// 드롭 시 파일/폴더 추가
    /// </summary>
    private void OnDrop(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop)!;
            ViewModel.AddItemsCommand.Execute(files);
        }
        e.Handled = true;
    }

    /// <summary>
    /// 프리셋 저장 버튼 클릭
    /// </summary>
    private async void OnSavePresetClick(object sender, RoutedEventArgs e)
    {
        var dialog = new Wpf.Ui.Controls.ContentDialog
        {
            Title = Strings.SavePresetDialogTitle,
            PrimaryButtonText = Strings.DialogSaveButton,
            CloseButtonText = Strings.DialogCancelButton
        };

        var textBox = new Wpf.Ui.Controls.TextBox
        {
            PlaceholderText = Strings.SavePresetDialogPlaceholder,
            Margin = new Thickness(0, 8, 0, 0)
        };

        dialog.Content = new StackPanel
        {
            Children =
            {
                new TextBlock { Text = Strings.SavePresetDialogMessage },
                textBox
            }
        };

        var result = await dialog.ShowAsync();

        if (result == Wpf.Ui.Controls.ContentDialogResult.Primary && !string.IsNullOrWhiteSpace(textBox.Text))
        {
            await ViewModel.SavePresetCommand.ExecuteAsync(textBox.Text);
        }
    }

    /// <summary>
    /// About 메뉴 클릭
    /// </summary>
    private async void OnAboutClick(object sender, RoutedEventArgs e)
    {
        var dialog = new AboutDialog();
        await dialog.ShowAsync();
    }
}