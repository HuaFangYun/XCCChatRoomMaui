using XCCChatRoom.AllImpl;

namespace XCCChatRoom.InnerPage;

public partial class UserPropertyEditor : ContentPage
{
    public UserPropertyEditor()
    {
        InitializeComponent();
    }

    private async void UserNameEditorButton_Click(object sender, TappedEventArgs e)
    {
        var checkParamName = e.Parameter as string;
        var paramName = string.Empty;
        switch (checkParamName)
        {
            case "�޸��û���":
                paramName = "�ǳ�";
                break;
            case "��������":
                paramName = "����";
                break;
            case "���°�����":
                paramName = "����";
                break;
            case "���°󶨵绰����":
                paramName = "�绰����";
                break;
        }
        var newUserProperty = await DisplayPromptAsync($"�޸�{paramName}", $"�������µ�{paramName}", "ȷ��", "ȡ��");
        if (newUserProperty is not null && newUserProperty != string.Empty)
        {
            if (newUserProperty.Contains(' ') && newUserProperty.VerifyString())
            {
                UserInfo.EditUserProperty(UserPropertyToEdit.UserName, newUserProperty, this);
            }
            else
            {
                await DisplayAlert("��������", $"�����벻���ո��{paramName}", "������");
            }
        }
        else
        {
            await DisplayAlert("��������", $"{paramName}����Ϊ��", "������");
        }
    }
}