﻿#pragma checksum "..\..\Main.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "537B8ED555FC46AC53B026061C964C88A94ADFAF46BBE8DE5CA8384EB99E9F10"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Bombermen;
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


namespace Bombermen {
    
    
    /// <summary>
    /// Main
    /// </summary>
    public partial class Main : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas game_canv;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbur;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbkey;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbun;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock score;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock tbdoor;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox time;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button restart;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\Main.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button menu;
        
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
            System.Uri resourceLocater = new System.Uri("/Bombermen;component/main.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Main.xaml"
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
            
            #line 8 "..\..\Main.xaml"
            ((Bombermen.Main)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.Key_Released);
            
            #line default
            #line hidden
            
            #line 8 "..\..\Main.xaml"
            ((Bombermen.Main)(target)).KeyDown += new System.Windows.Input.KeyEventHandler(this.Key_Pressed);
            
            #line default
            #line hidden
            return;
            case 2:
            this.game_canv = ((System.Windows.Controls.Canvas)(target));
            return;
            case 3:
            this.tbur = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.tbkey = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.tbun = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.score = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.tbdoor = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.time = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.restart = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\Main.xaml"
            this.restart.Click += new System.Windows.RoutedEventHandler(this.restart_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.menu = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\Main.xaml"
            this.menu.Click += new System.Windows.RoutedEventHandler(this.quit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

