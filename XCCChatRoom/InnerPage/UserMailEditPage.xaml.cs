using XFE������չ.NetCore.MailExtension;
using XFE������չ.NetCore.StringExtension;
using Timer = System.Timers.Timer;

namespace XCCChatRoom.InnerPage;

public partial class UserMailEditPage : ContentPage
{
    private bool flag1 = false;
    private bool flag2 = false;
    private bool flag3 = false; /*ȷ�ϰ�ť������������*/
    private string MailCaptcha = null;
    private string new_Mail = null;

    public UserMailEditPage()
	{
		InitializeComponent();
        OldMail.Text = UserInfo.CurrentUser.Amail;
	}

    private async void NewMail_Unfocused(object sender, FocusEventArgs e)
    {
        if (NewMail.Text is not null)
        {
            if (NewMail.Text.IsValidEmail()) { flag1 = true; }
            else
            {
                flag1 = false;
                await DisplayAlert("�������", "��������ȷ������", "ȷ��");
            }
        }
    }

    private void MailEditorCaptcha_TextChanged(object sender, TextChangedEventArgs e)
    {
        if(flag2) 
        {
            if (MailEditorCaptcha.Text == this.MailCaptcha) { flag3 = true; }
            else { flag3 = false; }
        }
    }

    private async void GetMailCode_Clicked(object sender, EventArgs e)
    {
        if (flag1)
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
            var randomCode = new Random().Next(100000, 999999).ToString();
            this.MailCaptcha = randomCode;
            var xFEMail = new XFEMail();
            try
            {
                /*xFEMail.SendEmail("��֤����", $"�������޸�XCG�����ҵİ�����\n��֤��Ϊ:{randomCode}\n"
                + "������������˵Ĳ������뾡���޸�����");*/
                new_Mail = NewMail.Text;
                flag2 = true;
            }
            catch (Exception ex)
            {
                await DisplayAlert("�޷������ʼ�", $"����{ex.Message}", "�˳�");
            }
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if (flag1 && flag3)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Mail, new_Mail, this);
            OldMail.Text = UserInfo.CurrentUser.Amail;
            await DisplayAlert("�޸ĳɹ�", "�����������޸ĳɹ�", "ȷ��");
            Shell.Current.SendBackButtonPressed();
        }
        else
        {
            if (flag1) await DisplayAlert("��֤�����", "��֤�벻ƥ��", "ȷ��");
            else
            {
                await DisplayAlert("����Ŵ���", "����Ų���ȷ", "ȷ��");
            }
        }
    }
}