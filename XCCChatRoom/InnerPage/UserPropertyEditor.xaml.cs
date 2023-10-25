using XCCChatRoom.AllImpl;
using XFE������չ.StringExtension;

namespace XCCChatRoom.InnerPage;

public partial class UserPropertyEditor : ContentPage
{
    public UserPropertyEditor()
    {
        InitializeComponent();
    }


    private async void UserNameEditor()
    {
        string newUserProperty = await DisplayPromptAsync("�޸�", "��������Ҫ�޸ĵ��ǳ�", "ȷ��", "ȡ��");
        bool flag = newUserProperty.UserNameEditor();
        if (flag)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.UserName, newUserProperty, this);
        }
        else
        {
            await DisplayAlert("�Ƿ��ǳ�", "������Ϸ��ǳ�", "������");
        }
    }

    private async void PasswordEditor()
    {
        string newUserProperty = await DisplayPromptAsync("�޸�", "��������Ҫ�޸ĵ�����", "ȷ��", "ȡ��");
        bool flag = newUserProperty.PasswordEditor();
        if (flag)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Password, newUserProperty, this);
        }
        else
        {
            await DisplayAlert("������̻����", "��������ʳ��ȵ�����", "������");
        }
    }

    private async void MailEditor()
    {
        var newUserProperty = await DisplayPromptAsync("�޸�", "��������Ҫ�޸ĵ�����", "ȷ��", "ȡ��");
        bool flag = XFE������չ.StringExtension.StringExtension.IsValidEmail(newUserProperty);
        if (flag)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Mail, newUserProperty, this);
        }
        else
        {
            await DisplayAlert("������Ч", "��������Ч�������ַ", "������");
        }
    }

    private async void TelEditor()
    {
        var newUserProperty = await DisplayPromptAsync("�޸�", "��������Ҫ�޸ĵ��ֻ���", "ȷ��", "ȡ��");
        bool flag = newUserProperty.IsMobPhoneNumber();
        if (flag)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.PhoneNum, newUserProperty, this);
        }
        else
        {
            await DisplayAlert("�ֻ�����Ч", "��������Ч���ֻ��ţ�Ŀǰ��֧���й���½�û���", "������");
        }
    }
    private void UserPropertyEditorButton_Click(object sender, EventArgs e)
    {
        Button button1 = (Button)sender;
        string InformationToBeModified = button1.Text;
        switch (InformationToBeModified)
        {
            case "�޸��û���":
                UserNameEditor();
                break;
            case "��������":
                PasswordEditor();
                break;
            case "���°�����":
                MailEditor();
                break;
            case "���°󶨵绰����":
                TelEditor();
                break;
        }
    }
}