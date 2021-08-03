﻿#pragma checksum "..\..\..\View\UserDashboard.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "E49B99BC78A18B415982600B43F7F0EFCDE8E1278FA4657889A5CF17C0A6BFBE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AssetTracker.View;
using AssetTracker.ViewModels;
using MaterialIcons;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AssetTracker.View {
    
    
    /// <summary>
    /// UserDashboard
    /// </summary>
    public partial class UserDashboard : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AssetTracker.View.UserDashboard Dashboard;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border LeftPanel;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border RightPanel;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Content_Header;
        
        #line default
        #line hidden
        
        
        #line 122 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border Content_Title;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Content_OverviewCounts;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\View\UserDashboard.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView UserAlertList;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AssetTracker;component/view/userdashboard.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\UserDashboard.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Dashboard = ((AssetTracker.View.UserDashboard)(target));
            return;
            case 2:
            this.LeftPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.RightPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 4:
            this.Content_Header = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 5:
            this.Content_Title = ((System.Windows.Controls.Border)(target));
            return;
            case 6:
            this.Content_OverviewCounts = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.UserAlertList = ((System.Windows.Controls.ListView)(target));
            
            #line 182 "..\..\..\View\UserDashboard.xaml"
            this.UserAlertList.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.ListView_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

