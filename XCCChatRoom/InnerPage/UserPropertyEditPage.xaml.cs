using MauiPopup;
using XCCChatRoom.AllImpl;
using XCCChatRoom.Controls;
using XFE������չ.NetCore.StringExtension;

namespace XCCChatRoom.InnerPage;

public partial class UserPropertyEditPage : ContentPage
{
    public string CurrentPhoneNumLabelText
    {
        get
        {
            return CurrentUserTelLabel.Text;
        }
        set
        {
            CurrentUserTelLabel.Dispatcher.Dispatch(() => CurrentUserTelLabel.Text = value);
        }
    }
    public string CurrentPasswordLabelText
    {
        get
        {
            return CurrentPasswordLabel.Text;
        }
        set
        {
            CurrentPasswordLabel.Dispatcher.Dispatch(() => CurrentPasswordLabel.Text = value);
        }
    }
    public string CurrentMailLabelText
    {
        get
        {
            return CurrentUserMailLabel.Text;
        }
        set
        {
            CurrentUserMailLabel.Dispatcher.Dispatch(() => CurrentUserMailLabel.Text = value);
        }
    }
    public UserPropertyEditPage()
    {
        InitializeComponent();
        CurrentUserTelLabel.Text = UserInfo.CurrentUser.Aname = UserInfo.CurrentUser.Aname;
        CurrentPhoneNumLabelText = UserInfo.CurrentUser.Atel;
        CurrentMailLabelText = UserInfo.CurrentUser.Amail;
    }
    private static bool modifyAuthentication = false;
    private async void ModifyAuthentication()
    {
        bool flag1 = false;
        bool flag2 = true;
        var randomCode = IDGenerator.SummonRandomID(6);

        if (!modifyAuthentication)
        {
            bool telFlag = await DisplayAlert("��ǰ��������ȫ", "����������֤", "ȷ��", "ȡ��");
            if (telFlag)
            {
                flag1 = true;
                if (flag2)
                {
                    /*await TencentSms.SendVerifyCode("1922760", "+86" + UserInfo.CurrentUser.Atel, new string[] { randomCode });*/
                    flag2 = false;
                }
            }
            else { flag1 = false; }
            if (flag1)
            {
                string captcha = /*await DisplayPromptAsync("�ֻ�����֤", "��������֤��", "ȷ��", "ȡ��");*/randomCode;
                if (captcha == randomCode)
                {
                    modifyAuthentication = true;
                }
            }
        }

    }
    private async void UserNameEditor()
    {
        if (CurrentUserNameEntry.Text is not null && CurrentUserNameEntry.Text != string.Empty)
        {
            bool flag = CurrentUserNameEntry.Text.UserNameEditor();
            if (flag)
            {
                UserInfo.EditUserProperty(UserPropertyToEdit.UserName, CurrentUserNameEntry.Text, this);
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
        string InformationToBeModified = button1.ClassId;
        switch (InformationToBeModified)
        {
            case "UserNameEditor":
                UserNameEditor();
                break;
            case "PasswordEditor":
                if (modifyAuthentication)
                    await Shell.Current.GoToAsync(nameof(UserPasswordEditorPage));
                else
                    ModifyAuthentication();
                break;
            case "MailEditor":
                if (modifyAuthentication)
                    /*await Shell.Current.GoToAsync(nameof(UserMailEditorPage));*/
                    await DisplayAlert("1", "д���뽫�����滻Ϊע������", "3");
                else
                    ModifyAuthentication();
                break;
            case "TelEditor":
                //if (modifyAuthentication)
                await PopupAction.DisplayPopup(new UserTelEditPopup(this));
                //else
                //ModifyAuthentication();
                break;
            default:
                await DisplayAlert("��Ǹ", "�����쳣�����ʧ��", "ȷ��");
                break;
        }
    }
}