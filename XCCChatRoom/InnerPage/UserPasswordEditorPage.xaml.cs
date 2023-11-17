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

    /*protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();
#if ANDROID
        (OldPassword.Handler.PlatformView as Android.Widget.EditText).Background = null;
        (NewPassword.Handler.PlatformView as Android.Widget.EditText).Background = null;
        (NewPasswordConfirmation.Handler.PlatformView as Android.Widget.EditText).Background = null;
#endif
    }*/

    private void OldPassword_Unfocused(object sender, FocusEventArgs e)
    {
        if (UserInfo.CurrentUser.Apassword == OldPassword.Text) { flag1 = true; }
        else { flag1 = false; }
    }


    private void NewPassword_Unfocused(object sender, FocusEventArgs e)
    {
        if (NewPassword.Text.PasswordEditor()) { flag2 = true; }
        else { flag2 = false; }
    }

    private void NewPasswordConfirmation_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (flag2)
        {
            if (NewPassword.Text == NewPasswordConfirmation.Text) { flag3 = true; }
            else { flag3 = false; }
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        if(flag1 && flag2 && flag3)
        {
            UserInfo.EditUserProperty(UserPropertyToEdit.Password,NewPassword.Text, this);
            await DisplayAlert("��ʾ", "�޸ĳɹ�", "����");
            Shell.Current.SendBackButtonPressed();
        }
        else if(!flag1)
        {
            await DisplayAlert("������", "���������", "ȷ��");
        }
        else if(!flag2)
        {
            await DisplayAlert("������", "�����벻����Ҫ��", "ȷ��");
        }
        else if(!flag3)
        {
            await DisplayAlert("������", "�������������벻һ��", "ȷ��");
        }
    }

    private void ForgotPasswordButton_Click(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(ForgetPasswordPage));
    }
}