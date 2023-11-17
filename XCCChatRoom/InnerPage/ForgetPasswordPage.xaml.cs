using XCCChatRoom.AllImpl;
using XFE������չ.NetCore.XFEDataBase;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.InnerPage;

public partial class ForgetPasswordPage : ContentPage
{
    private bool �ֻ����ж� = false;
    private bool �����ж� = false;
    private bool ��֤���ж� = false;
    /*ȷ�ϰ�ť������������*/
    private string TelCaptcha = null;
    public ForgetPasswordPage()
	{
		InitializeComponent();
	}
    
    private async void �ֻ��Ű󶨼��()
    {
        if(Tel.Text is not null)
        {
            var xFEExecuter = XCCDataBase.XFEDataBase.CreateExecuter();
            var result = await xFEExecuter.ExecuteGet<XFEChatRoom_UserInfoForm>(x => x.Atel == Tel.Text);
            if (result.Count > 0 && result is not null)
            {
                if (result.Count == 1) { �ֻ����ж� = true; }
                else
                {
                    await DisplayAlert("�쳣", "���ֻ��Ű󶨹�����˺�", "ȷ��");
                    �ֻ����ж� = false;
                }
            }
            else
            {
                await DisplayAlert("�����ڸ��û�", "���ֻ���δ�󶨹��˺�", "ȷ��");
                �ֻ����ж� = false;
            }
        }
        else 
        {
            �ֻ����ж� = false;
            await DisplayAlert("����", "���������ֻ���", "ȷ��"); 
        }
    }

    private async void ����ϸ��ж�()
    {
        �����ж� = NewPassword.Text.PasswordEditor();
        if(!�����ж�)
        {
            await DisplayAlert("���벻�ϸ�", "���޸�����", "ȷ��");
        }
    }

    private async void GetTelCode_Clicked(object sender, EventArgs e)
    {
        �ֻ��Ű󶨼��();
        if (�ֻ����ж�)
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
            var randomCode = new Random().Next(1, 999999).ToString();
            if (randomCode.Length < 6)
            {
                for (int i = 6 - randomCode.Length; i > 0; i--)
                    randomCode = $"0{randomCode}";
            }
            this.TelCaptcha = randomCode;
            await TencentSms.SendVerifyCode(this, "1922760", "+86" + Tel.Text, new string[] { randomCode });
            ��֤���ж� = false;
        }
    }

    private async void ��֤����()
    {
        if (ForgetPassword_TelCaptcha is not null)
            if (ForgetPassword_TelCaptcha.Text == this.TelCaptcha)
                ��֤���ж� = true;
            else { ��֤���ж� = false; }
        else 
        { 
            await DisplayAlert("����", "��δ������֤��", "ȷ��");
            ��֤���ж� = false; 
        }
    }
    private async void  Button_Clicked(object sender, EventArgs e)
    {
        ��֤����();
        ����ϸ��ж�();
        if (!�����ж�)
            await DisplayAlert("����", "��δ��������", "ȷ��");
        else if(!��֤���ж�)
            await DisplayAlert("����", "��֤�����", "ȷ��");
        else
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Password, NewPassword.Text, this);
            await DisplayAlert("��ʾ", "�޸ĳɹ�", "����");
            Shell.Current.SendBackButtonPressed();
        }
        
    }
}