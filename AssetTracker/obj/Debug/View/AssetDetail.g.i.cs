﻿#pragma checksum "..\..\..\View\AssetDetail.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "47DFCA17AA60EF2BA721C0B61C8255C596C525B074D817E94C2DE1E68532DAEB"
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
using AssetTracker.ViewModel;
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
    /// AssetDetail
    /// </summary>
    public partial class AssetDetail : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 23 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel LeftPanel;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Metadata;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ScrollViewer RightPanel;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Header;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel AssetTitle;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox DisplayName;
        
        #line default
        #line hidden
        
        
        #line 156 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AssetID;
        
        #line default
        #line hidden
        
        
        #line 165 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel AssigningGrp;
        
        #line default
        #line hidden
        
        
        #line 169 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AssetTracker.View.Searchbox Searchbox_AssignedTo;
        
        #line default
        #line hidden
        
        
        #line 174 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel CurrentStatus;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AssetTracker.View.Searchbox Searchbox_Phase;
        
        #line default
        #line hidden
        
        
        #line 186 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Body;
        
        #line default
        #line hidden
        
        
        #line 195 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Timeline;
        
        #line default
        #line hidden
        
        
        #line 228 "..\..\..\View\AssetDetail.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView Changelog;
        
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
            System.Uri resourceLocater = new System.Uri("/AssetTracker;component/view/assetdetail.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\AssetDetail.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.LeftPanel = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.Metadata = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.RightPanel = ((System.Windows.Controls.ScrollViewer)(target));
            return;
            case 5:
            this.Header = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 6:
            this.AssetTitle = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.DisplayName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            
            #line 147 "..\..\..\View\AssetDetail.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditDisplayNameClicked);
            
            #line default
            #line hidden
            return;
            case 9:
            this.AssetID = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 10:
            this.AssigningGrp = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 11:
            this.Searchbox_AssignedTo = ((AssetTracker.View.Searchbox)(target));
            return;
            case 12:
            this.CurrentStatus = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 13:
            this.Searchbox_Phase = ((AssetTracker.View.Searchbox)(target));
            return;
            case 14:
            this.Body = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 15:
            this.Timeline = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 16:
            this.Changelog = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 3:
            
            #line 75 "..\..\..\View\AssetDetail.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.HierarchyObjectClicked);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

