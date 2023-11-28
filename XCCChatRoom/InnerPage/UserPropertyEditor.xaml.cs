using System.Text;
using XCCChatRoom.AllImpl;
using XFE������չ.NetCore.XFEDataBase;
using XFE������չ.StringExtension;

namespace XCCChatRoom.InnerPage;

public partial class UserPropertyEditor : ContentPage
{
    public UserPropertyEditor()
    {
        InitializeComponent();
    }
    private static bool modifyAuthentication = false;
    private async void ModifyAuthentication()
    {
        bool flag1 = false;
        bool flag2 = true;
        var randomCode = new Random().Next(0, 999999).ToString();
        if (randomCode.Length < 6)
        {
            for (int i = 6 - randomCode.Length; i > 0; i--)
                randomCode = $"0{randomCode}";
        }
        if (!modifyAuthentication) 
        {
            bool Telflag= await DisplayAlert("��ǰ��������ȫ", "����������֤", "ȷ��", "ȡ��");
            if (Telflag)
            {
                flag1 = true;
                if (flag2)
                {
                    await TencentSms.SendVerifyCode(this, "1922760", "+86" + UserInfo.CurrentUser.Atel, new string[] { randomCode });
                    flag2 = false;
                }
            }
            else { flag1 = false;}
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
        if(newUserProperty is not null && newUserProperty != string.Empty)
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
        bool flag = XFE������չ.StringExtension.StringExtension.IsValidEmail(newUserProperty);
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
                if(modifyAuthentication)
                    await Shell.Current.GoToAsync(nameof(UserTelEditorPage));
                else
                    ModifyAuthentication();
                break;
        }
    }
}