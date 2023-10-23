using XCCChatRoom.AllImpl;
using XCCChatRoom.Controls;
using XFE������չ.FormatExtension;
using XFE������չ.NetCore.XFEDataBase;

namespace XCCChatRoom.InnerPage;

public partial class GroupContactPage : ContentPage
{
    public static GroupContactPage Current { get; set; }
    public string UserName
    {
        get => userName;
        set
        {
            userName = value;
            OfficalGroupCardView.UserNameInGroup = value;
        }
    }
    private XFEDictionary groups = new XFEDictionary();
    private XFEExecuter XFEExecuter = XCCDataBase.XFEDataBase.CreateExecuter();
    private string userName;
    public GroupContactPage()
    {
        Current = this;
        InitializeComponent();
        this.Loaded += async (sender, e) =>
        {
            AppUpdate.StartCheckUpdate(this);
            await UserInfo.ReadUserData(this);
            groupRefreshView.IsRefreshing = true;
        };
        UserName = UserInfo.StaticUserName;
        //"����".WriteIn($"{Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryDownloads)}/����.txt");
        ToolbarItems.Add(new ToolbarItem
        {
            Text = "���Ⱥ��",
            IconImageSource = "plus",
            Command = new Command(async () =>
            {
                if (UserInfo.IsLoginSuccessful)
                {
                    var targetGroupName = await DisplayPromptAsync("���Ⱥ��", "������Ⱥ������", "ȷ��", "ȡ��", "Ⱥ������", 20, Keyboard.Default, string.Empty);
                    if (string.IsNullOrWhiteSpace(targetGroupName))
                    {
                        if (targetGroupName == string.Empty)
                            await DisplayAlert("����", "Ⱥ�����Ʋ���Ϊ��", "ȷ��");
                        return;
                    }
                    else
                    {
                        var userNameInGroup = await DisplayPromptAsync("Ⱥ�����", "�������ڸ�Ⱥ�е�����", "ȷ��", null, "Ⱥ������", 20, Keyboard.Default, UserInfo.StaticUserName);
                        if (string.IsNullOrWhiteSpace(userNameInGroup))
                        {
                            if (userNameInGroup == string.Empty)
                                await DisplayAlert("����", "Ⱥ�����Ʋ���Ϊ��", "ȷ��");
                            return;
                        }
                        try
                        {
                            groups.Add(targetGroupName, userNameInGroup);
                            var groupView = new GroupCardView
                            {
                                GroupName = targetGroupName,
                                UserNameInGroup = userNameInGroup
                            };
                            groupView.Click += GroupCardView_Click;
                            groupView.Swipe += GroupCardView_Swipe;
                            GroupStackLayout.Children.Add(groupView);
                            await UpLoadGroup();
                        }
                        catch (Exception ex)
                        {
                            await DisplayAlert("Ⱥ���ظ�", "Ⱥ�����Ʋ����ظ�\nԭ�쳣��" + ex.Message, "ȷ��");
                        }
                    }
                }
                else
                {
                    await DisplayAlert("δ��¼", "���ȵ�¼", "ȷ��");
                }
            })
        });
        OfficalGroupCardView.Click += GroupCardView_Click;
        OfficalGroupCardView.Swipe += GroupCardView_Swipe;
    }
    private void LoadGroup()
    {
        if (UserInfo.IsLoginSuccessful)
        {
            OfficalGroupCardView.IsVisible = true;
            try
            {
                groups = new XFEDictionary(UserInfo.CurrentUser.Agroups);
                if (groups.Count > 0)
                {
                    foreach (var item in groups)
                    {
                        var groupView = new GroupCardView
                        {
                            GroupName = item.Header,
                            UserNameInGroup = item.Content
                        };
                        groupView.Click += GroupCardView_Click;
                        groupView.Swipe += GroupCardView_Swipe;
                        GroupStackLayout.Children.Add(groupView);
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("����", ex.Message, "ȷ��");
            }
        }
        else
        {
            OfficalGroupCardView.IsVisible = false;
        }
    }
    private async Task UpLoadGroup()
    {
        try
        {
            UserInfo.CurrentUser.Agroups = groups.ToString();
            await UserInfo.CurrentUser.ExecuteUpdate(XFEExecuter);
        }
        catch (Exception ex)
        {
            await DisplayAlert("����", ex.Message, "ȷ��");
        }
    }
    private async void GroupCardView_Swipe(object sender, GroupCardViewSwipeEventArgs e)
    {
        var objSender = sender as GroupCardView;
        await objSender.TranslateTo(-200, 0, 80, Easing.SpringOut);
        if (objSender.GroupName == "XFE������[�ٷ�]")
        {
            await objSender.TranslateTo(0, 0, 80, Easing.SpringOut);
            return;
        }
        var result = await DisplayAlert("ɾ��", "�Ƿ�ɾ����Ⱥ�飿", "��", "��");
        if (result)
        {
            GroupStackLayout.Children.Remove(objSender);
            groups.Remove(objSender.GroupName);
            await UpLoadGroup();
        }
        else
        {
            await objSender.TranslateTo(0, 0, 80, Easing.SpringOut);
        }
    }
    private async void GroupCardView_Click(object sender, GroupCardViewClickEventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ChatPage)}?GroupName={e.GroupName}&CurrentName={e.UserNameInGroup}");
    }

    private void groupRefreshView_Refreshing(object sender, EventArgs e)
    {
        RemoveOtherGroup();
        LoadGroup();
        groupRefreshView.IsRefreshing = false;
    }
    public void RemoveOtherGroup()
    {
        for (int i = GroupStackLayout.Children.Count; i > 1; i--)
        {
            GroupStackLayout.Children.RemoveAt(i - 1);
        }
    }
}