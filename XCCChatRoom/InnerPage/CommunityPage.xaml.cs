using XCCChatRoom.AllImpl;
using XCCChatRoom.Controls;
using XFE������չ.ArrayExtension;
using XFE������չ.NetCore.XFEDataBase;
using XFE������չ.TaskExtension;

namespace XCCChatRoom.InnerPage;

public partial class CommunityPage : ContentPage
{
    public static CommunityPage Current { get; private set; }
    private bool tapped = false;
    private readonly XFEExecuter XFEExecuter = XCCDataBase.XFEDataBase.CreateExecuter();
    private List<PostCardView> postCardList = new List<PostCardView>();
    private List<string> postIdList = new List<string>();
    private long totalHeight = 0;
    private bool firstRefresh = true;
    private bool refreshingIsBusy = false;
    private bool RefreshingIsBusy
    {
        get => refreshingIsBusy;
        set
        {
            refreshingIsBusy = value;
            if (value)
            {
                LoadingLabel.IsVisible = true;
            }
            else
            {
                LoadingLabel.IsVisible = false;
            }
        }
    }
    public CommunityPage()
    {
        InitializeComponent();
        Current = this;
        postRefreshView.IsRefreshing = true;
    }

    public async void postRefreshView_Refreshing(object sender, EventArgs e)
    {
        await new Action(async () =>
        {
            var postDataList = await AppAlgorithm.GetLatestPost(20);
            if (postDataList is not null)
                foreach (var postData in postDataList)
                {
                    var tarPostCard = postCardList.Find(x => x.PostId == postData.PostID);
                    if (tarPostCard is not null)
                    {
                        tarPostCard.Dispatcher.Dispatch(() =>
                        {
                            tarPostCard.ReloadData(postData);
                            if (UserInfo.IsLoginSuccessful && UserInfo.CurrentUser.LikedPostID.Contains(tarPostCard.PostId))
                            {
                                tarPostCard.IsLike = true;
                            }
                        });
                        totalHeight = GetTotalHeight();
                    }
                    else
                    {
                        PostCardView post;
                        if (firstRefresh)
                            post = new PostCardView(postData, false);
                        else
                            post = new PostCardView(postData);
                        post.LikeClick += Post_LikeClick;
                        post.Click += Post_Click;
                        post.TagClick += Post_TagClick;
                        if (UserInfo.IsLoginSuccessful && UserInfo.CurrentUser.LikedPostID.Contains(postData.PostID))
                        {
                            post.IsLike = true;
                        }
                        postCardList.Add(post);
                        postIdList.Add(post.PostId);
                        postRefreshView.Dispatcher.Dispatch(() =>
                        {
                            postStackLayout.Children.Insert(0, post);
                            //postListView.chi
                        });
                        totalHeight = GetTotalHeight();
                        await Console.Out.WriteLineAsync($"������{postScrollView.Height}\t��ǰ��{totalHeight}");
                        if (totalHeight > postScrollView.Height && firstRefresh)
                            break;
                    }
                }
            postRefreshView.Dispatcher.Dispatch(() =>
            {
                postRefreshView.IsRefreshing = false;
            });
            totalHeight = GetTotalHeight();
            firstRefresh = false;
        }).StartNewTask();
    }

    private async void GetDownPost()
    {
        var postDataList = await AppAlgorithm.GetElderPost(3, postIdList);
        if (postDataList is not null)
            foreach (var postData in postDataList)
            {
                var post = new PostCardView(postData);
                post.LikeClick += Post_LikeClick;
                post.Click += Post_Click;
                post.TagClick += Post_TagClick;
                if (UserInfo.IsLoginSuccessful && UserInfo.CurrentUser.LikedPostID.Contains(postData.PostID))
                {
                    post.IsLike = true;
                }
                postCardList.Add(post);
                postIdList.Add(postData.PostID);
                postRefreshView.Dispatcher.Dispatch(() =>
                {
                    postStackLayout.Children.Add(post);
                });
            }
        totalHeight = GetTotalHeight();
        RefreshingIsBusy = false;
    }

    public void RemovePostByID(string postId)
    {
        postStackLayout.Children.Remove(postCardList.Find(x => x.PostId == postId));
        totalHeight = GetTotalHeight();
    }

    private async void Post_TagClick(object sender, PostCardViewTagClickEventArgs e)
    {
        await DisplayAlert("���α�ǩ", "�Ƿ����α�ǩ��" + e.TagString, "����", "ȡ��");
    }

    private async void Post_Click(object sender, PostCardViewClickEventArgs e)
    {
        if (UserInfo.IsLoginSuccessful)
        {
            await Shell.Current.GoToAsync($"{nameof(PostViewPage)}?PostID={e.PostEntity.PostID}");
        }
        else
        {
            if (await DisplayAlert("����", "���ȵ�¼Ŷ", "ǰ����¼", "ȡ��"))
            {
                await Shell.Current.GoToAsync(nameof(UserLoginPage));
            }
        }
    }

    private async void Post_LikeClick(object sender, PostCardViewLikeClickEventArgs e)
    {
        var post = sender as PostCardView;
        if (!UserInfo.IsLoginSuccessful)
        {
            post.LikeCount--;
            post.IsLike = false;
            if (await DisplayAlert("����", "���ȵ�¼Ŷ", "ǰ����¼", "ȡ��"))
            {
                await Shell.Current.GoToAsync(nameof(UserLoginPage));
            }
            return;
        }
        if (e.IsLike)
        {
            e.PostEntity.PostLike++;
            UserInfo.CurrentUser.LikedPostID += new string[] { e.PostEntity.PostID }.ToXFEString();
            await UserInfo.UpLoadUserInfo();
            if (await e.PostEntity.ExecuteUpdate(XFEExecuter) == 0)
            {
                e.PostEntity.PostLike--;
                post.LikeCount--;
                post.IsLike = false;
                await DisplayAlert("����ʧ��", "������������", "ȷ��");
            }
        }
        else
        {
            if (e.PostEntity.PostLike >= 0)
                e.PostEntity.PostLike--;
            UserInfo.CurrentUser.LikedPostID = UserInfo.CurrentUser.LikedPostID.Replace($"[+-{e.PostEntity.PostID}-+]", string.Empty);
            await UserInfo.UpLoadUserInfo();
            if (await e.PostEntity.ExecuteUpdate(XFEExecuter) == 0)
            {
                e.PostEntity.PostLike++;
                post.LikeCount--;
                post.IsLike = true;
                await DisplayAlert("ȡ������ʧ��", "������������", "ȷ��");
            }
        }
    }
    public void ChangeToUnLoginStyle()
    {
        foreach (var post in postCardList)
        {
            post.IsLike = false;
        }
    }
    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        if (tapped)
        {
            return;
        }
        await ellipse.ScaleTo(0.8, 100, Easing.CubicOut);
        var task = Shell.Current.GoToAsync(nameof(PostEditPage));
        await ellipse.ScaleTo(1, 100, Easing.CubicOut);
        tapped = false;
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if (tapped)
        {
            return;
        }
        tapped = true;
        await ellipse.ScaleTo(0.8, 100, Easing.CubicOut);
        var task = Shell.Current.GoToAsync(nameof(PostEditPage));
        await ellipse.ScaleTo(1, 100, Easing.CubicOut);
        tapped = false;
    }
    private long GetTotalHeight()
    {
        long totalHeight = 0;
        foreach (var post in postCardList)
        {
            totalHeight += (long)post.DesiredSize.Height + 6;
        }
        return totalHeight;
    }
    private void postScrollView_Scrolled(object sender, ScrolledEventArgs e)
    {
        if (RefreshingIsBusy)
            return;
        if (totalHeight - e.ScrollY - postScrollView.Height <= 0)
        {
            RefreshingIsBusy = true;
            GetDownPost();
            Console.WriteLine("���ظ���");
        }
    }
}