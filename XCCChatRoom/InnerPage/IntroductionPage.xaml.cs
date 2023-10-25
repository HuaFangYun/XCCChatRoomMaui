namespace XCCChatRoom.InnerPage;

public partial class IntroductionPage : ContentPage
{
    private int pageCount;

    public int PageCount
    {
        get { return pageCount; }
        set
        {
            pageCount = value;
        }
    }

    public IntroductionPage()
    {
        InitializeComponent();
        PageCount = 1;
        this.Loaded += async (sender, e) =>
        {
#pragma warning disable CS4014
            titleLabel.FadeTo(1, 800, Easing.CubicOut);
            titleLabel.TranslateTo(0, 0, 800, Easing.CubicOut);
            await Task.Delay(100);
            showImage.FadeTo(1, 800, Easing.CubicOut);
            showImage.TranslateTo(0, 0, 800, Easing.CubicOut);
            await Task.Delay(100);
            contentLabel.FadeTo(0.9, 800, Easing.CubicOut);
            contentLabel.TranslateTo(0, 0, 800, Easing.CubicOut);
            await Task.Delay(100);
            indexLabel.FadeTo(0.5, 800, Easing.CubicOut);
            indexLabel.TranslateTo(0, 0, 800, Easing.CubicOut);
            await Task.Delay(100);
            nextButton.FadeTo(1, 800, Easing.CubicOut);
            nextButton.TranslateTo(0, 0, 800, Easing.CubicOut);
            await Task.Delay(100);
            skipButton.FadeTo(0.7, 800, Easing.CubicOut);
            await skipButton.TranslateTo(0, 0, 800, Easing.CubicOut);
#pragma warning restore
        };
    }

    private async void nextButton_Clicked(object sender, EventArgs e)
    {
        PageCount++;
        switch (PageCount)
        {
            case 2:
                OutAndInAnimation("��������", "����������˵һ������˵��\n���⣬���ǻ����������ܣ����鷢�������аɣ�", "being_creative");
                break;
            case 3:
                OutAndInAnimation("AIʱ��", "�����AI��ʱ�������ǵ������ô���ٵ������أ�\n���ǵ��������AI�����AI�����ȣ����Խ���㷽����������⡣ǧ��ҪС����AI", "artificial_intelligence");
                break;
            case 4:
                OutAndInAnimation("�������", "���˵һ����������������ģ�������һ������û�б�����˰ɣ��������������һ�׼�Լ��ı��������ӷ�ף�����ª", "emoji_discuss");
                break;
            case 5:
                OutAndInAnimation("���ǵ��Ŷ�", "������һ����Ϊ��Ȥ����������һ��Ŀ����Ŷӣ�����Ȼ���࣬�������Ƕ����ڷ�", "about_our_team");
                break;
            case 6:
                OutAndInAnimation("���ڲ��԰�", "���ǵ����Ŀǰ���Ǵ����ڲ�׶Σ�������������κ�bug������֪ͨ����", "beta_testing");
                break;
            case 7:
                OutAndInAnimation("�������", "������в������Ǹ��µ���Դ����Ȼ�����кܶ����У������ǳ����������һ�죬�����������˲ŷḻ�����ǵ�����", "get_inspired");
                break;
            case 8:
                nextButton.Text = "���";
                nextButton.Margin = new Thickness(20, 10, 20, 50);
                skipButton.IsVisible = false;
                OutAndInAnimation("���", "����һ�ж�׼���������������ǿ�ʼ�ɣ�", "success");
                break;
            case 9:
                nextButton.IsEnabled = false;
                await this.FadeTo(0, 1500);
                var mainShell = new AppShell
                {
                    Opacity = 0
                };
                await mainShell.FadeTo(1, 300, Easing.CubicOut);
                Application.Current.MainPage = mainShell;
                break;
            default:
                throw new Exception("ҳ�泬����Χ");
        }
    }

    private void skipButton_Click(object sender, TappedEventArgs e)
    {
        PageCount = 7;
        nextButton_Clicked(null, null);
    }
    private async void OutAndInAnimation(string titleText, string contentText, string imageResource)
    {
#pragma warning disable CS4014
        nextButton.IsEnabled = false;
        titleLabel.TranslateTo(0, -150, 800, Easing.SpringOut);
        showImage.TranslateTo(-150, 0, 800, Easing.SpringOut);
        contentLabel.TranslateTo(-150, 0, 800, Easing.SpringOut);
        indexLabel.TranslateTo(150, 0, 800, Easing.SpringOut);
        titleLabel.FadeTo(0, 800, Easing.SpringOut);
        showImage.FadeTo(0, 800, Easing.SpringOut);
        contentLabel.FadeTo(0, 800, Easing.SpringOut);
        await indexLabel.FadeTo(0, 800, Easing.SpringOut);
        if (pageCount < 9)
            indexLabel.Text = $"{pageCount}/8";
        titleLabel.Text = titleText;
        contentLabel.Text = contentText;
        showImage.Source = imageResource;
        titleLabel.TranslationX = 0;
        titleLabel.TranslationY = 0;
        showImage.TranslationX = 0;
        showImage.TranslationY = 0;
        contentLabel.TranslationX = 0;
        contentLabel.TranslationY = 0;
        indexLabel.TranslationX = 0;
        indexLabel.TranslationY = 0;
        nextButton.IsEnabled = true;
        titleLabel.FadeTo(1, 800, Easing.SpringOut);
        showImage.FadeTo(1, 800, Easing.SpringOut);
        contentLabel.FadeTo(0.9, 800, Easing.SpringOut);
        await indexLabel.FadeTo(0.5, 800, Easing.SpringOut);
#pragma warning restore
    }
}