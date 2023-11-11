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
                await Shell.Current.GoToAsync(nameof(UserPasswordEditorPage));
                break;
            case "���°�����":
                /*await Shell.Current.GoToAsync(nameof(UserMailEditorPage));*/
                await DisplayAlert("1", "2", "3");
                break;
            case "���°󶨵绰����":
                await Shell.Current.GoToAsync(nameof(UserTelEditorPage));
                break;
        }
    }
}