using XCCChatRoom.AllImpl;
using XCCChatRoom.Model;
using XFE������չ.NetCore.XFEDataBase;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.ViewPage;

public partial class ForgetPasswordPage : ContentPage
{
    private bool telIsValid = false;
    private bool passwordIsValid = false;
    private bool captchaIsValid = false;
    /*ȷ�ϰ�ť������������*/
    private string telCaptcha = null;
    public ForgetPasswordPage()
	{
		InitializeComponent();
	}
    
    private async void TelBindJudgment()
    {
        if(Tel.Text is not null)
        {
            var xFEExecuter = XCCDataBase.XFEDataBase.CreateExecuter();
            var result = await xFEExecuter.ExecuteGet<XFEChatRoom_UserInfoForm>(x => x.Atel == Tel.Text);
            if (result.Count > 0 && result is not null)
            {
                if (result.Count == 1) { telIsValid = true; }
                else
                {
                    await Shell.Current?.DisplayAlert("�쳣", "���ֻ��Ű󶨹�����˺�", "ȷ��");
                    telIsValid = false;
                }
            }
            else
            {
                await Shell.Current?.DisplayAlert("�����ڸ��û�", "���ֻ���δ�󶨹��˺�", "ȷ��");
                telIsValid = false;
            }
        }
        else 
        {
            telIsValid = false;
            await Shell.Current?.DisplayAlert("����", "���������ֻ���", "ȷ��"); 
        }
    }

    private async void PasswordQualifiedJudgment()
    {
        passwordIsValid = NewPasswordEditor.Text.PasswordEditor();
        if(!passwordIsValid)
        {
            await Shell.Current?.DisplayAlert("���벻���Ϲ淶", "���޸�����", "ȷ��");
        }
    }

    private async void GetTelCode_Clicked(object sender, EventArgs e)
    {
        TelBindJudgment();
        if (telIsValid)
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
            this.telCaptcha = randomCode;
            await TencentSms.SendVerifyCode("1922760", "+86" + Tel.Text, new string[] { randomCode });
            captchaIsValid = false;
        }
    }

    private async void CaptchaJudgment()
    {
        if (ForgetPassword_TelCaptchaEntry is not null)
            if (ForgetPassword_TelCaptchaEntry.Text == this.telCaptcha)
                captchaIsValid = true;
            else { captchaIsValid = false; }
        else 
        { 
            await Shell.Current?.DisplayAlert("����", "��δ������֤��", "ȷ��");
            captchaIsValid = false; 
        }
    }
    private async void  Button_Clicked(object sender, EventArgs e)
    {
        CaptchaJudgment();
        PasswordQualifiedJudgment();
        if (!passwordIsValid)
            await Shell.Current?.DisplayAlert("����", "��δ��������", "ȷ��");
        else if(!captchaIsValid)
            await Shell.Current?.DisplayAlert("����", "��֤�����", "ȷ��");
        else
        {
            UserInfoPage.EditUserProperty(UserPropertyToEdit.Password, NewPasswordEditor.Text, this);
            await Shell.Current?.DisplayAlert("��ʾ", "�޸ĳɹ�", "����");
            Shell.Current.SendBackButtonPressed();
        }
        
    }
}