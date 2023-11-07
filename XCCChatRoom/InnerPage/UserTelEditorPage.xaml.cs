using XCCChatRoom.AllImpl;
using XFE������չ.StringExtension;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.InnerPage;

public partial class UserTelEditorPage : ContentPage
{
    private bool flag1 = false;
    private bool flag2 = false; /*ȷ�ϰ�ť������������*/
    private string TelCaptcha = null;
    public UserTelEditorPage()
    {
        InitializeComponent();
        OldTel.Text = UserInfo.CurrentUser.Atel;
    }

    private async void NewTel_Unfocused(object sender, FocusEventArgs e)
    {
        if (NewTel != null)
        {
            if (NewTel.Text.IsMobPhoneNumber())
            {
                flag1 = true;
            }
            else { await DisplayAlert("��������", "�����ֻ��Ų��Ϲ�Ŷ", "֪����"); }
        }
        else { await DisplayAlert("�ֻ���δ����", "�ֻ��Ų���Ϊ��Ŷ", "������"); }
    }

    private async void GetTelCode_Clicked(object sender, EventArgs e)
    {
        GetTelCode.IsEnabled = false;
        int countDown = 60;
        Timer timer = new Timer(1000);
        timer.Elapsed += (sender, e) =>
        {
            countDown--;
            if (countDown <= 0)
            {
                GetTelCode.IsEnabled = true;
                timer.Dispose();
                return;
            }
            GetTelCode.Text = countDown.ToString();
        };
        var randomCode = new Random().Next(0, 999999).ToString();
        this.TelCaptcha = randomCode;
        await TencentSms.SendVerifyCode(this, "1922760", "+86" + NewTel.Text, new string[] { randomCode });

    }
    private void TelEditorCaptcha_TextChange(object sender, TextChangedEventArgs e)
    {
        
        if (TelEditorCaptcha.Text == this.TelCaptcha) { flag2 = true; }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (flag1 && flag2) 
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.PhoneNum, NewTel.Text, this);
            OldTel.Text = UserInfo.CurrentUser.Atel;
            await DisplayAlert("�޸ĳɹ�", "�����ֻ������޸ĳɹ�", "ȷ��");

        }
        else {
            if (flag1)  await DisplayAlert("��֤�����", "��֤�벻ƥ��", "ȷ��"); 
            else await DisplayAlert("�ֻ��Ŵ���", "��������ȷ���ֻ���", "ȷ��"); 
        }

    }
}