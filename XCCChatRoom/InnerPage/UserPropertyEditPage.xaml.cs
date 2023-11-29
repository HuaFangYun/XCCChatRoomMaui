using XCCChatRoom.AllImpl;
using XFE������չ.StringExtension;

namespace XCCChatRoom.InnerPage;

public partial class UserPropertyEditPage : ContentPage
{
    public UserPropertyEditPage()
    {
        InitializeComponent();
    }
    private static bool modifyAuthentication = false;
    private async void ModifyAuthentication()
    {
        bool flag1 = false;
        bool flag2 = true;
        var randomCode = new Random().Next(100000, 999999).ToString();
        if (!modifyAuthentication)
        {
            bool telFlag = await DisplayAlert("��ǰ��������ȫ", "����������֤", "ȷ��", "ȡ��");
            if (telFlag)
            {
                flag1 = true;
                if (flag2)
                {
                    await TencentSms.SendVerifyCode("1922760", "+86" + UserInfo.CurrentUser.Atel, new string[] { randomCode });
                    flag2 = false;
                }
            }
            else { flag1 = false; }
            if (flag1)
            {
                string captcha = await DisplayPromptAsync("�ֻ�����֤", "��������֤��", "ȷ��", "ȡ��");
                if (captcha == randomCode)
                {
                    modifyAuthentication = true;
                }
            }
        }

    }
    private async void UserNameEditor()
    {
        string newUserProperty = await DisplayPromptAsync("�޸�", "��������Ҫ�޸ĵ��ǳ�", "ȷ��", "ȡ��");
        if (newUserProperty is not null && newUserProperty != string.Empty)
        {
            bool flag = newUserProperty.UserNameEditor();
            if (flag)
            {
                UserInfo.EditUserProperty(UserPropertyToEdit.UserName, newUserProperty, this);
                await DisplayAlert("�޸ĳɹ�", "���ݺϷ�", "������");
            }
            else
            {
                await DisplayAlert("�Ƿ��ǳ�", "������Ϸ��ǳ�", "������");
            }
        }
    }

    private async void MailEditor()
    {
        var newUserProperty = await DisplayPromptAsync("�޸�", "��������Ҫ�޸ĵ�����", "ȷ��", "ȡ��");
        bool flag = newUserProperty.IsValidEmail();
        if (flag)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Mail, newUserProperty, this);
            await DisplayAlert("�޸ĳɹ�", "���ݺϷ�", "������");
        }
        else
        {
            await DisplayAlert("������Ч", "��������Ч�������ַ", "������");
        }
    }

    private async void UserPropertyEditorButton_Click(object sender, EventArgs e)
    {
        Button button1 = (Button)sender;
        string InformationToBeModified = button1.Text;
        switch (InformationToBeModified)
        {
            case "�޸��û���":
                UserNameEditor();
                break;
            case "��������":
                if (modifyAuthentication)
                    await Shell.Current.GoToAsync(nameof(UserPasswordEditorPage));
                else
                    ModifyAuthentication();
                break;
            case "���°�����":

                if (modifyAuthentication)
                    /*await Shell.Current.GoToAsync(nameof(UserMailEditorPage));*/
                    await DisplayAlert("1", "д���뽫�����滻Ϊע������", "3");
                else
                    ModifyAuthentication();
                break;
            case "���°󶨵绰����":
                //if (modifyAuthentication)
                    await Shell.Current.GoToAsync(nameof(UserTelEditPage));
                //else
                    //ModifyAuthentication();
                break;
        }
    }
}