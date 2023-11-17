using XCCChatRoom.AllImpl;
using XFE������չ.NetCore.XFEDataBase;
using XFE������չ.StringExtension;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.InnerPage;

public partial class UserTelEditorPage : ContentPage
{
    private bool flag1 = false;
    private bool flag2 = false;
    private bool flag3 = false;/*ȷ�ϰ�ť������������*/
    private string TelCaptcha = null;
    private string new_Tel = null;
    public UserTelEditorPage()
    {
        InitializeComponent();
        OldTel.Text = UserInfo.CurrentUser.Atel;
    }

    private async void NewTel_Unfocused(object sender, FocusEventArgs e)
    {
        if (NewTel is not null)
        {
            if (NewTel.Text.IsMobPhoneNumber())
            {
                var xFEExecuter = XCCDataBase.XFEDataBase.CreateExecuter();
                var result = await xFEExecuter.ExecuteGet<XFEChatRoom_UserInfoForm>(x => x.Atel == NewTel.Text);
                if (result is null)
                {
                    flag1 = true;
                }
                else 
                { 
                    flag1 = false;
                    await DisplayAlert("ʧ��", "���ֻ������а��˺�", "ȷ��"); 
                }
            }
            else 
            {
                flag1 = false;
                await DisplayAlert("��������", "�����ֻ��Ų��Ϲ�Ŷ", "֪����");
            }
        }
        else {  flag1 = false;}
    }

    private async void GetTelCode_Clicked(object sender, EventArgs e)
    {
        if (flag1)
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
            if (randomCode.Length < 6)
            {
                for (int i = 6 - randomCode.Length; i > 0; i--)
                    randomCode = $"0{randomCode}";
            }
            this.TelCaptcha = randomCode;
            await TencentSms.SendVerifyCode(this, "1922760", "+86" + NewTel.Text, new string[] { randomCode });
            new_Tel = NewTel.Text;
            flag2 = true;
        }
        else { await DisplayAlert("����", "���������ֻ���", "ȷ��"); }
    }
    private void TelEditorCaptcha_TextChange(object sender, TextChangedEventArgs e)
    {
        if (flag2)
        {
            if (TelEditorCaptcha.Text == this.TelCaptcha) { flag3 = true; }
            else { flag3 = false; }
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (flag1 && flag3) 
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.PhoneNum, new_Tel, this);
            OldTel.Text = UserInfo.CurrentUser.Atel;
            await DisplayAlert("�޸ĳɹ�", "�����ֻ������޸ĳɹ�", "ȷ��");
            Shell.Current.SendBackButtonPressed();
        }
        else {
            if (flag1)  await DisplayAlert("��֤�����", "��֤�벻ƥ��", "ȷ��"); 
            else await DisplayAlert("�ֻ��Ŵ���", "��������ȷ���ֻ���", "ȷ��");
        }
    }
}