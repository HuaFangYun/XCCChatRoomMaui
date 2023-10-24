using MauiPopup;
using MauiPopup.Views;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.Controls;

public partial class LoadingPopup : BasePopupPage
{
    public LoadingPopup(string title, int closeTime = 3)
    {
        InitializeComponent();
        this.Shadow = new Shadow
        {
            Opacity = 0.5f,
            Brush = Color.FromArgb("#512BD4"),
            Radius = 5,
            Offset = new Point(4, 4),
        };
        DisplayTitleLabel.Text = title;
        DisplayContentLabel.IsVisible = false;
        CloseTimer(closeTime);
    }
    public LoadingPopup(string title, string content, int closeTime = 3)
    {
        InitializeComponent();
        this.Shadow = new Shadow
        {
            Opacity = 0.5f,
            Brush = Color.FromArgb("#512BD4"),
            Radius = 5,
            Offset = new Point(4, 4),
        };
        DisplayTitleLabel.Text = title;
        DisplayContentLabel.Text = content;
        CloseTimer(closeTime);
    }
    private void CloseTimer(int second)
    {
        Timer timer = new Timer(second * 1000);
        timer.Elapsed += (sender, e) =>
        {
            this.Dispatcher.Dispatch(async () =>
            {
                await PopupAction.ClosePopup();
            });
        };
        timer.Start();
    }
}