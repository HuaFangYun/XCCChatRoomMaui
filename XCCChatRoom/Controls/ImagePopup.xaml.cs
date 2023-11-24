using MauiPopup;
using MauiPopup.Views;
using XCCChatRoom.AllImpl;

namespace XCCChatRoom.Controls;

public partial class ImagePopup : BasePopupPage
{
    public byte[] ImageBuffer { get; set; }
    public string ImageId { get; set; }
    public ImagePopup(byte[] imageBuffer, string imageId)
    {
        ImageBuffer = imageBuffer;
        ImageId = imageId;
        InitializeComponent();
        imageView.Source = ImageSource.FromStream(() => new MemoryStream(imageBuffer));
    }

    private async void SaveButton_Clicked(object sender, EventArgs e)
    {
        var result = await Permissions.RequestAsync<Permissions.StorageWrite>();
        switch (result)
        {
            case PermissionStatus.Unknown:
                await DisplayAlert("����ʧ��", "δ֪����״̬", "����");
                break;
            case PermissionStatus.Denied:
                await DisplayAlert("����ʧ��", "���󱻾ܾ�", "ȷ��");
                break;
            case PermissionStatus.Disabled:
                await DisplayAlert("����ʧ��", "���治�����󣬵�ȥ�ֶ�����Ȩ��", "OK");
                break;
            case PermissionStatus.Granted:
                if (Directory.Exists(AppPath.ChatImageSavePath))
                    Directory.CreateDirectory(AppPath.ChatImageSavePath);
                var savePath = $"{AppPath.ChatImageSavePath}/{ImageId}.png";
                try
                {
                    await File.WriteAllBytesAsync(savePath, ImageBuffer);
                    await PopupAction.DisplayPopup(new TipPopup("����ɹ�", $"ͼƬ�ѱ��浽��{savePath}", 1.5));
                }
                catch (Exception ex)
                {
                    await PopupAction.DisplayPopup(new ErrorPopup("����ʧ��", $"���ִ���{ex.Message}"));
                }
                break;
            case PermissionStatus.Restricted:
                await DisplayAlert("����ʧ��", "��Restricted��", "�Ҳ�");
                break;
            case PermissionStatus.Limited:
                await DisplayAlert("����ʧ��", "����������", "����");
                break;
            default:
                break;
        }
    }
}