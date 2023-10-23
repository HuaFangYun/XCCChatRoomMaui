namespace XCCChatRoom.InnerPage;

public partial class AICenterPage : ContentPage
{
    public AICenterPage()
    {
        InitializeComponent();
    }

    private async void ChatToNormalGPTButton_Clicked(object sender, EventArgs e)
    {
        if (UserInfo.IsLoginSuccessful)
        {
            await Shell.Current.GoToAsync(nameof(GPTAIChatPage));
        }
        else
        {
            if (await DisplayAlert("��δ��¼", "����û�е�¼���޷�ʹ�øù���", "ǰ����¼", "ȡ��"))
            {
                await Shell.Current.GoToAsync(nameof(UserLoginPage));
            }
        }
    }

    private async void ChatToHigherGPTButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("������δ����", "Ŀǰ�ù��ܻ�δ������ɣ������ڴ�", "������");
        //Shell.Current.GoToAsync(nameof(GPTAIChatPage));
    }

    private async void AIDrawPictureButton_Clicked(object sender, EventArgs e)
    {
        await DisplayAlert("������δ����", "Ŀǰ�ù��ܻ�δ������ɣ������ڴ�", "��Ҳû�У�");
        //Shell.Current.GoToAsync(nameof(AIDrawPage));
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        await DisplayAlert("����AI", "Ŀǰ���ڳ��������У����ڲ��԰�ܶ๦���д����ƣ����������Ĳ��㻹���½�", "�˽�");
    }
}