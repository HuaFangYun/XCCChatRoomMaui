using XCCChatRoom.AllImpl;

namespace XCCChatRoom.InnerPage;

public partial class UserPasswordEditorPage : ContentPage
{
    private bool flag1 = false;
    private bool flag2 = false;
    private bool flag3 = false; /*ȷ�ϰ�ť������������*/
	public UserPasswordEditorPage()
	{
		InitializeComponent();
	}

    private async void OldPassword_Unfocused(object sender, FocusEventArgs e)
    {
        if (UserInfo.CurrentUser.Apassword == OldPassword.Text) { flag1 = true; }
        else { await DisplayAlert("������", "�������", "ȷ��"); }
    }


    private async void NewPassword_Unfocused(object sender, FocusEventArgs e)
    {
        if (NewPassword.Text.PasswordEditor()) { flag2 = true; }
        else { await DisplayAlert("������", "���벻����Ҫ��", "ȷ��"); }
    }

    private void NewPasswordConfirmation_TextChanged(object sender, EventArgs e)
    {
        if(NewPassword.Text == NewPasswordConfirmation.Text) { flag3 = true; }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if(flag1 && flag2 && flag3)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Password,NewPassword.Text, this);
            await DisplayAlert("��ʾ", "�޸ĳɹ�", "����");
        }
        else
        {
            await DisplayAlert("��ʾ", "�밴��Ҫ����������","ȷ��");
        }
    }
}