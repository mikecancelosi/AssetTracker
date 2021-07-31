﻿#pragma checksum "..\..\..\View\RoleEdit.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D7F46ADB4816A8C535E6061E42A1BBF9EC3DDDE73806D643659B5FDFC66C0C35"
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
using AssetTracker.View.Extensions;
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
    /// RoleEdit
    /// </summary>
    public partial class RoleEdit : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\View\RoleEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border LeftPanel;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\View\RoleEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border ControlButtons;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\View\RoleEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Breadcrumbs;
        
        #line default
        #line hidden
        
        
        #line 145 "..\..\..\View\RoleEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Control PromptSavePanel;
        
        #line default
        #line hidden
        
        
        #line 149 "..\..\..\View\RoleEdit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border DeletePrompt;
        
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
            System.Uri resourceLocater = new System.Uri("/AssetTracker;component/view/roleedit.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\RoleEdit.xaml"
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
            this.LeftPanel = ((System.Windows.Controls.Border)(target));
            return;
            case 2:
            this.ControlButtons = ((System.Windows.Controls.Border)(target));
            return;
            case 3:
            this.Breadcrumbs = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            
            #line 101 "..\..\..\View\RoleEdit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NavigateToProjectSettings);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 121 "..\..\..\View\RoleEdit.xaml"
            ((System.Windows.Controls.TextBox)(target)).TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.RoleName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 126 "..\..\..\View\RoleEdit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnActivateAllClicked);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 132 "..\..\..\View\RoleEdit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnDeactivateAllClicked);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PromptSavePanel = ((System.Windows.Controls.Control)(target));
            return;
            case 9:
            this.DeletePrompt = ((System.Windows.Controls.Border)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

