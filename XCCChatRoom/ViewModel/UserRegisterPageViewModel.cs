﻿using CommunityToolkit.Mvvm.ComponentModel;
using XCCChatRoom.AllImpl;
using XCCChatRoom.ViewPage;
using XFE各类拓展.NetCore.XFEDataBase;

namespace XCCChatRoom.ViewModel;

internal partial class UserRegisterPageViewModel:ObservableObject
{
    [ObservableProperty]
    private string phoneNum;
    [ObservableProperty]
    private string verifyCode;
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string mail;
    [ObservableProperty]
    private string password;
    public UserRegisterPage ViewPage { get; init; }
    private readonly XFEExecuter XFEExecuter = XCCDataBase.XFEDataBase.CreateExecuter();
    private bool isTelEditor = false, isMailEditor = false, isNameEditor = false, isPasswordEditor = false, isPasswordEnsureEditor = false;
    private string randomCode = string.Empty;
    private string currentPhoneNum = string.Empty;
    private bool isCoolDown = false;

    public UserRegisterPageViewModel(UserRegisterPage viewPage)
    {
        ViewPage = viewPage;
        new Action(() =>
        {
            Thread.Sleep(500);
            while (!UserTelEditor.IsFocused)
            {
                UserTelEditor.Focus();
                Thread.Sleep(100);
            }
        }).StartNewTask();
    }

    private async void NextStepButton_WaitClick(object sender, WaitButtonClickedEventArgs e)
    {
        try
        {
            TelVerifyCodeBorder.Stroke = Color.FromArgb("#444654");
            TelVerifyCodeLabel.TextColor = Color.Parse("Gray");
            TelVerifyCodeLabel.Text = "验证码";
            UserTelBorder.Stroke = Color.FromArgb("#444654");
            UserTelLabel.TextColor = Color.Parse("Gray");
            UserTelLabel.Text = "手机号";
            if (TelVerifyCodeEditor.Text == randomCode)
            {
                if (UserTelEditor.Text == currentPhoneNum)
                {
                    var telResult = await XFEExecuter.ExecuteGet<XFEChatRoom_UserInfoForm>(x => x.Atel == UserTelEditor.Text);
                    if (telResult.Count > 0)
                    {
                        UserTelLabel.Text = "手机号已存在";
                        UserTelLabel.TextColor = Color.Parse("Red");
                        UserTelBorder.Stroke = Color.Parse("Red");
                        ControlExtension.BorderShake(UserTelBorder);
                        UserTelEditor.Focus();
                        e.Continue();
                        return;
                    }
                    else
                    {
                        #region FadeAnimation
                        _ = UserTelLabel.TranslateTo(-100, 0, 800, Easing.SpringOut);
                        _ = UserTelBorder.TranslateTo(-100, 0, 800, Easing.SpringOut);
                        _ = NextStepButton.TranslateTo(-100, 0, 800, Easing.SpringOut);
                        _ = TelVerifyCodeButton.TranslateTo(-100, 0, 800, Easing.SpringOut);
                        _ = TelVerifyCodeLabel.TranslateTo(-100, 0, 800, Easing.SpringOut);
                        _ = TelVerifyCodeBorder.TranslateTo(-100, 0, 800, Easing.SpringOut);
                        _ = UserTelLabel.FadeTo(0, 800, Easing.SpringOut);
                        _ = UserTelBorder.FadeTo(0, 800, Easing.SpringOut);
                        _ = TelVerifyCodeLabel.FadeTo(0, 800, Easing.SpringOut);
                        _ = TelVerifyCodeBorder.FadeTo(0, 800, Easing.SpringOut);
                        _ = NextStepButton.FadeTo(0, 800, Easing.SpringOut);
                        _ = SwtichToLoginPageButton.FadeTo(0, 800, Easing.SpringOut);
                        await TelVerifyCodeButton.FadeTo(0, 800, Easing.SpringOut);
                        #endregion
                        #region SetInvisible
                        UserTelLabel.IsVisible = false;
                        UserTelEditor.IsEnabled = false;
                        UserTelBorder.IsVisible = false;
                        TelVerifyCodeLabel.IsVisible = false;
                        TelVerifyCodeEditor.IsEnabled = false;
                        TelVerifyCodeBorder.IsVisible = false;
                        NextStepButton.IsVisible = false;
                        NextStepButton.IsEnabled = false;
                        TelVerifyCodeButton.IsVisible = false;
                        #endregion
                        #region SetVisible
                        UserNameLabel.IsVisible = true;
                        UserNameBorder.IsVisible = true;
                        UserPasswordLabel.IsVisible = true;
                        UserPasswordBorder.IsVisible = true;
                        UserPasswordEnsureLabel.IsVisible = true;
                        UserPasswordEnsureBorder.IsVisible = true;
                        UserMailLabel.IsVisible = true;
                        UserMailBorder.IsVisible = true;
                        UserRegisterButton.IsVisible = true;
                        #endregion
                        #region ShowAnimation
                        await new Action(() =>
                        {
                            this.Dispatcher.Dispatch(() =>
                            {
                                UserNameLabel.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserNameLabel.TranslateTo(0, 0, 1000, Easing.CubicOut);
                                UserNameBorder.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserNameBorder.TranslateTo(0, 0, 1000, Easing.CubicOut);
                            });
                            Thread.Sleep(200);
                            this.Dispatcher.Dispatch(() =>
                            {
                                UserMailLabel.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserMailLabel.TranslateTo(0, 0, 1000, Easing.CubicOut);
                                UserMailBorder.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserMailBorder.TranslateTo(0, 0, 1000, Easing.CubicOut);
                            });
                            Thread.Sleep(200);
                            this.Dispatcher.Dispatch(() =>
                            {
                                UserPasswordLabel.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserPasswordLabel.TranslateTo(0, 0, 1000, Easing.CubicOut);
                                UserPasswordBorder.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserPasswordBorder.TranslateTo(0, 0, 1000, Easing.CubicOut);
                            });
                            Thread.Sleep(200);
                            this.Dispatcher.Dispatch(() =>
                            {
                                UserPasswordEnsureLabel.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserPasswordEnsureLabel.TranslateTo(0, 0, 1000, Easing.CubicOut);
                                UserPasswordEnsureBorder.FadeTo(0.5, 1000, Easing.CubicOut);
                                UserPasswordEnsureBorder.TranslateTo(0, 0, 1000, Easing.CubicOut);
                            });
                            Thread.Sleep(200);
                            this.Dispatcher.Dispatch(() =>
                            {
                                UserRegisterButton.FadeTo(1, 1000, Easing.CubicOut);
                                UserRegisterButton.TranslateTo(0, 0, 1000, Easing.CubicOut);
                            });
                            Thread.Sleep(200);
                            this.Dispatcher.Dispatch(() =>
                            {
                                SwtichToLoginPageButton.FadeTo(1, 1000, Easing.CubicOut);
                            });
                            Thread.Sleep(200);
                        }).StartNewTask();
                        #endregion
                        UserNameEditor.Focus();
                        e.Continue();
                    }
                }
                else
                {
                    UserTelLabel.TextColor = Color.Parse("Red");
                    UserTelBorder.Stroke = Color.Parse("Red");
                    UserTelLabel.Text = "手机号与验证码发送时的不一致";
                    UserTelEditor.Focus();
                    e.Continue();
                    ControlExtension.BorderShake(UserTelBorder);
                }
            }
            else
            {
                TelVerifyCodeLabel.TextColor = Color.Parse("Red");
                TelVerifyCodeBorder.Stroke = Color.Parse("Red");
                TelVerifyCodeLabel.Text = "验证码错误";
                TelVerifyCodeEditor.Focus();
                e.Continue();
                ControlExtension.BorderShake(TelVerifyCodeBorder);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current?.DisplayAlert("注册时出现错误", ex.Message, "确定");
        }
    }

    private async void TelVerifyCodeButton_Clicked(object sender, EventArgs e)
    {
        randomCode = IDGenerator.SummonRandomID(6);
        currentPhoneNum = UserTelEditor.Text;
        TelVerifyCodeButton.IsEnabled = false;
        TelVerifyCodeButton.BackgroundColor = Color.FromArgb("#A491E8");
        TelVerifyCodeButton.Text = "发送中...";
        isCoolDown = true;
        var resp = await TencentSms.SendVerifyCode("1922756", "+86" + UserTelEditor.Text, [randomCode, "2"]);
        if (resp == null || resp.SendStatusSet.First().Code != "Ok")
        {
            await Shell.Current?.DisplayAlert("出错啦！", $"验证码发送失败：{resp?.SendStatusSet.First().Message}\n手机号：{UserTelEditor.Text}", "啊？");
            TelVerifyCodeButton.IsEnabled = true;
            TelVerifyCodeButton.BackgroundColor = Color.FromArgb("#512BD4");
            TelVerifyCodeButton.Text = "重新发送";
        }
        else
        {
            TelVerifyCodeButton.Text = "重新发送 60";
            await new Action(() =>
            {
                int timer = 60;
                TelVerifyCodeButton.Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    TelVerifyCodeButton.Text = $"重新发送 {--timer}";
                    if (timer == 0)
                    {
                        TelVerifyCodeButton.Text = "重新发送";
                        TelVerifyCodeButton.IsEnabled = true;
                        isCoolDown = false;
                        TelVerifyCodeButton.BackgroundColor = Color.FromArgb("#512BD4");
                        return false;
                    }
                    return true;
                });
            }).StartNewTask();
        }
    }

    private async void UserRegisterButton_WaitClick(object sender, WaitButtonClickedEventArgs e)
    {
        string UUID = await IDGenerator.GetCorrectUserUID(XCCDataBase.XFEDataBase.CreateExecuter());
        try
        {
            var mailResult = await XFEExecuter.ExecuteGet<XFEChatRoom_UserInfoForm>(x => x.Amail == UserMailEditor.Text);
            if (mailResult.Count > 0)
            {
                UserMailLabel.Text = "邮箱已存在";
                UserMailLabel.TextColor = Color.Parse("Red");
                UserMailBorder.Stroke = Color.Parse("Red");
                ControlExtension.BorderShake(UserMailBorder);
                UserMailEditor.Focus();
                return;
            }
            var result = await XFEExecuter.ExecuteAdd(new XFEChatRoom_UserInfoForm()
            {
                ID = UUID,
                Aname = UserNameEditor.Text,
                Atel = UserTelEditor.Text,
                Amail = UserMailEditor.Text,
                Apassword = UserPasswordEditor.Text,
            });
            if (result == 0)
            {
                await PopupAction.DisplayPopup(new ErrorPopup("注册失败", "未能成功注册，请重试"));
                e.Continue();
                return;
            }
            var successfulLabel = new Label
            {
                Text = "注册成功",
                Opacity = 0,
                TextColor = Color.Parse("White"),
                FontSize = 40,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center
            };
            this.Content = successfulLabel;
            await successfulLabel.FadeTo(1, 800, Easing.CubicOut);
            await Task.Delay(500);
            await successfulLabel.FadeTo(0, 800, Easing.CubicOut);
            Shell.Current.SendBackButtonPressed();
            e.Continue();
        }
        catch (Exception ex)
        {
            try
            {
                await XFEExecuter.ExecuteDelete<XFEChatRoom_UserInfoForm>(x => x.ID == UUID);
            }
            catch (Exception) { }
            if (await Shell.Current?.DisplayAlert("注册出错啦！", $"注册失败：{ex.Message}", "重试", "返回"))
            {
                Trace.WriteLine($"手机号：{UserTelEditor.Text}");
                Trace.WriteLine($"邮箱：{UserMailEditor.Text}");
                Trace.WriteLine($"密码：{UserPasswordEditor.Text}");
                Trace.WriteLine($"确认密码：{UserPasswordEnsureEditor.Text}");
                Trace.WriteLine($"验证码：{TelVerifyCodeEditor.Text}");
                Trace.WriteLine($"随机码：{randomCode}");
                Trace.WriteLine($"用户名：{UserNameEditor.Text}");
                Trace.WriteLine(ex.ToString());
                e.Continue();
            }
            else
            {
                await Shell.Current.GoToAsync("..");
                e.Continue();
            }
        }
    }

    private async void SwitchToLoginPageButton_WaitClick(object sender, WaitButtonClickedEventArgs e)
    {
        await Shell.Current.GoToAsync("..");
        e.Continue();
    }
    #region 编辑框内容检测
    private void UserTelEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (UserTelEditor.Text.IsMobPhoneNumber())
        {
            isTelEditor = true;
            UserTelLabel.Text = "手机号";
            UserTelLabel.TextColor = Color.Parse("Black");
            UserTelBorder.Stroke = Color.FromArgb("#444654");
            if (!isCoolDown)
            {
                TelVerifyCodeButton.IsEnabled = true;
                TelVerifyCodeButton.BackgroundColor = Color.FromArgb("#512BD4");
            }
        }
        else
        {
            isTelEditor = false;
            UserTelLabel.Text = "手机号格式不正确";
            UserTelLabel.TextColor = Color.Parse("Red");
            TelVerifyCodeButton.IsEnabled = false;
            TelVerifyCodeButton.BackgroundColor = Color.FromArgb("#A491E8");
        }
    }

    private void UserMailEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (UserMailEditor.Text.IsValidEmail())
        {
            isMailEditor = true;
            UserMailLabel.Text = "邮箱";
            UserMailLabel.TextColor = Color.Parse("Black");
            UserMailBorder.Stroke = Color.FromArgb("#444654");
        }
        else
        {
            isMailEditor = false;
            UserMailLabel.Text = "邮箱格式不正确";
            UserMailLabel.TextColor = Color.Parse("Red");
        }
        CheckCorrect();
    }

    private void UserNameEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(UserNameEditor.Text))
        {
            if (!UserNameEditor.Text.Contains(' '))
            {
                isNameEditor = true;
                UserNameLabel.Text = "昵称";
                UserNameLabel.TextColor = Color.Parse("Black");
            }
            else
            {
                UserNameLabel.Text = "昵称不可包含空格";
                UserNameLabel.TextColor = Color.Parse("Red");
                isNameEditor = false;
            }
        }
        else
        {
            UserNameLabel.Text = "昵称不可为空";
            UserNameLabel.TextColor = Color.Parse("Red");
            isNameEditor = false;
        }
        CheckCorrect();
    }

    private void UserPasswordEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrEmpty(UserPasswordEditor.Text))
        {
            if (!UserPasswordEditor.Text.Contains(' '))
            {
                isPasswordEditor = true;
                UserPasswordLabel.Text = "密码";
                UserPasswordLabel.TextColor = Color.Parse("Black");
            }
            else
            {
                UserPasswordLabel.Text = "密码不可包含空格";
                UserPasswordLabel.TextColor = Color.Parse("Red");
                isPasswordEditor = false;
            }
        }
        else
        {
            UserPasswordLabel.Text = "密码不可为空";
            UserPasswordLabel.TextColor = Color.Parse("Red");
            isPasswordEditor = false;
        }
        CheckCorrect();
    }

    private void UserPasswordEnsureEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (UserPasswordEnsureEditor.Text == UserPasswordEditor.Text)
        {
            isPasswordEnsureEditor = true;
            UserPasswordEnsureLabel.Text = "确认密码";
            UserPasswordEnsureLabel.TextColor = Color.Parse("Black");
        }
        else
        {
            isPasswordEnsureEditor = false;
            UserPasswordEnsureLabel.Text = "两次密码不一致";
            UserPasswordEnsureLabel.TextColor = Color.Parse("Red");
        }
        CheckCorrect();
    }

    private void TelVerifyCodeEditor_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (isTelEditor && TelVerifyCodeEditor.Text.Length == 6)
        {
            if (!NextStepButton.IsWaiting)
            {
                NextStepButton.BackgroundColor = Color.Parse("#512BD4");
                NextStepButton.IsEnabled = true;
            }
            TelVerifyCodeEditor_Unfocused(null, null);
        }
        else
        {
            if (!NextStepButton.IsWaiting)
            {
                NextStepButton.BackgroundColor = Color.Parse("#A491E8");
                NextStepButton.IsEnabled = false;
            }
        }
    }

    private void CheckCorrect()
    {
        if (isTelEditor && isMailEditor && isNameEditor && isPasswordEditor && isPasswordEnsureEditor)
        {
            UserRegisterButton.BackgroundColor = Color.Parse("#512BD4");
            if (!UserRegisterButton.IsWaiting)
                UserRegisterButton.IsEnabled = true;
        }
        else
        {
            UserRegisterButton.BackgroundColor = Color.Parse("#A491E8");
            if (!UserRegisterButton.IsWaiting)
                UserRegisterButton.IsEnabled = false;
        }
    }
    #endregion
    #region 编辑框焦点事件
    private void UserTelEditor_Focused(object sender, FocusEventArgs e)
    {
        UserTelLabel.FadeTo(1, 300, Easing.CubicOut);
        UserTelLabel.ScaleTo(1.2, 300, Easing.CubicOut);
        UserTelBorder.FadeTo(1, 300, Easing.CubicOut);
        UserTelBorder.ScaleTo(1.2, 300, Easing.CubicOut);
    }

    private void UserTelEditor_Unfocused(object sender, FocusEventArgs e)
    {
        UserTelLabel.FadeTo(0.5, 300, Easing.CubicOut);
        UserTelLabel.ScaleTo(1, 300, Easing.CubicOut);
        UserTelBorder.FadeTo(0.5, 300, Easing.CubicOut);
        UserTelBorder.ScaleTo(1, 300, Easing.CubicOut);
    }

    private void UserMailEditor_Focused(object sender, FocusEventArgs e)
    {
        UserMailLabel.FadeTo(1, 300, Easing.CubicOut);
        UserMailLabel.ScaleTo(1.2, 300, Easing.CubicOut);
        UserMailBorder.FadeTo(1, 300, Easing.CubicOut);
        UserMailBorder.ScaleTo(1.2, 300, Easing.CubicOut);
    }

    private void UserMailEditor_Unfocused(object sender, FocusEventArgs e)
    {
        UserMailLabel.FadeTo(0.5, 300, Easing.CubicOut);
        UserMailLabel.ScaleTo(1, 300, Easing.CubicOut);
        UserMailBorder.FadeTo(0.5, 300, Easing.CubicOut);
        UserMailBorder.ScaleTo(1, 300, Easing.CubicOut);
    }

    private void UserNameEditor_Focused(object sender, FocusEventArgs e)
    {
        UserNameLabel.FadeTo(1, 300, Easing.CubicOut);
        UserNameLabel.ScaleTo(1.2, 300, Easing.CubicOut);
        UserNameBorder.FadeTo(1, 300, Easing.CubicOut);
        UserNameBorder.ScaleTo(1.2, 300, Easing.CubicOut);
    }

    private void UserNameEditor_Unfocused(object sender, FocusEventArgs e)
    {
        UserNameLabel.FadeTo(0.5, 300, Easing.CubicOut);
        UserNameLabel.ScaleTo(1, 300, Easing.CubicOut);
        UserNameBorder.FadeTo(0.5, 300, Easing.CubicOut);
        UserNameBorder.ScaleTo(1, 300, Easing.CubicOut);
    }

    private void TelVerifyCodeEditor_Focused(object sender, FocusEventArgs e)
    {
        TelVerifyCodeLabel.FadeTo(1, 300, Easing.CubicOut);
        TelVerifyCodeLabel.ScaleTo(1.2, 300, Easing.CubicOut);
        TelVerifyCodeBorder.FadeTo(1, 300, Easing.CubicOut);
        TelVerifyCodeBorder.ScaleTo(1.2, 300, Easing.CubicOut);
        TelVerifyCodeButton.ScaleTo(0.8, 300, Easing.CubicOut);
    }

    private void TelVerifyCodeEditor_Unfocused(object sender, FocusEventArgs e)
    {
        TelVerifyCodeLabel.FadeTo(0.5, 300, Easing.CubicOut);
        TelVerifyCodeLabel.ScaleTo(1, 300, Easing.CubicOut);
        TelVerifyCodeBorder.FadeTo(0.5, 300, Easing.CubicOut);
        TelVerifyCodeBorder.ScaleTo(1, 300, Easing.CubicOut);
        TelVerifyCodeButton.ScaleTo(1, 300, Easing.CubicOut);
    }

    private void UserPasswordEditor_Focused(object sender, FocusEventArgs e)
    {
        UserPasswordLabel.FadeTo(1, 300, Easing.CubicOut);
        UserPasswordLabel.ScaleTo(1.2, 300, Easing.CubicOut);
        UserPasswordBorder.FadeTo(1, 300, Easing.CubicOut);
        UserPasswordBorder.ScaleTo(1.2, 300, Easing.CubicOut);
    }

    private void UserPasswordEditor_Unfocused(object sender, FocusEventArgs e)
    {
        UserPasswordLabel.FadeTo(0.5, 300, Easing.CubicOut);
        UserPasswordLabel.ScaleTo(1, 300, Easing.CubicOut);
        UserPasswordBorder.FadeTo(0.5, 300, Easing.CubicOut);
        UserPasswordBorder.ScaleTo(1, 300, Easing.CubicOut);
    }

    private void UserPasswordEnsureEditor_Focused(object sender, FocusEventArgs e)
    {
        UserPasswordEnsureLabel.FadeTo(1, 300, Easing.CubicOut);
        UserPasswordEnsureLabel.ScaleTo(1.2, 300, Easing.CubicOut);
        UserPasswordEnsureBorder.FadeTo(1, 300, Easing.CubicOut);
        UserPasswordEnsureBorder.ScaleTo(1.2, 300, Easing.CubicOut);
    }

    private void UserPasswordEnsureEditor_Unfocused(object sender, FocusEventArgs e)
    {
        UserPasswordEnsureLabel.FadeTo(0.5, 300, Easing.CubicOut);
        UserPasswordEnsureLabel.ScaleTo(1, 300, Easing.CubicOut);
        UserPasswordEnsureBorder.FadeTo(0.5, 300, Easing.CubicOut);
        UserPasswordEnsureBorder.ScaleTo(1, 300, Easing.CubicOut);
    }
    #endregion
}
