﻿using XCCChatRoom.InnerPage;

namespace XCCChatRoom;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        this.BindingContext = this;
        Routing.RegisterRoute(nameof(ChatPage), typeof(ChatPage));
        Routing.RegisterRoute(nameof(UserLoginPage), typeof(UserLoginPage));
        Routing.RegisterRoute(nameof(UserRegisterPage), typeof(UserRegisterPage));
        Routing.RegisterRoute(nameof(PostEditPage), typeof(PostEditPage));
        Routing.RegisterRoute(nameof(PostViewPage), typeof(PostViewPage));
        Routing.RegisterRoute(nameof(GPTAIChatPage), typeof(GPTAIChatPage));
        Routing.RegisterRoute(nameof(AIDrawPage), typeof(AIDrawPage));
    }
}
