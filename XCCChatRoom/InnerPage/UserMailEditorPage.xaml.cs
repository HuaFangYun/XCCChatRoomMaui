using XFE������չ.MailExtension;
using XFE������չ.StringExtension;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.InnerPage;

public partial class UserMailEditorPage : ContentPage
{
    private bool flag1 = false;
    private bool flag2 = false; /*ȷ�ϰ�ť������������*/
    private string MailCaptcha = null;

    public UserMailEditorPage()
	{
		InitializeComponent();
        OldMail.Text = UserInfo.CurrentUser.Amail;
	}

    private void NewMail_Unfocused(object sender, FocusEventArgs e)
    {
        if (NewMail.Text is not null)
        {
            if (NewMail.Text.IsValidEmail()) { flag1 = true; }
        }
    }

    private void MailEditorCaptcha_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (MailEditorCaptcha.Text == this.MailCaptcha) { flag2 = true; }
    }

    private async void GetMailCode_Clicked(object sender, EventArgs e)
    {
        GetMailCode.IsEnabled = false;
        int countDown = 60;
        Timer timer = new Timer(1000);
        timer.Elapsed += (sender, e) =>
        {
            countDown--;
            if (countDown <= 0)
            {
                GetMailCode.IsEnabled = true;
                timer.Dispose();
                return;
            }
            GetMailCode.Text = countDown.ToString();
        };
        var randomCode = new Random().Next(0, 999999).ToString();
        this.MailCaptcha = randomCode;
        XFEMail xFEMail = new XFEMail();
        try
        {
            xFEMail.SendEmail("��֤����", $"�������޸�XCG�����ҵİ�����\n��֤��Ϊ:{randomCode}\n"
            + "������������˵Ĳ������뾡���޸�����");
        }
        catch(Exception ex) {
            await DisplayAlert("�޷������ʼ�", "ʧ����ʧ����", "�˳�");
        }
        
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (flag1 && flag2)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Mail, NewMail.Text, this);
            OldMail.Text = UserInfo.CurrentUser.Amail;
            await DisplayAlert("�޸ĳɹ�", "�����������޸ĳɹ�", "ȷ��");
        }
        else
        {
            if (flag1) await DisplayAlert("��֤�����", "��֤�벻ƥ��", "ȷ��");
            else await DisplayAlert("�������", "��������ȷ������", "ȷ��");
        }
    }
}