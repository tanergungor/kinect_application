﻿#pragma checksum "..\..\..\UserControls\CheckTaskUserControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "871E6C8A77B65026A2AF57D2D242D9FA"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using KINECT_APPLICATION.UserControls;
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


namespace KINECT_APPLICATION.UserControls {
    
    
    /// <summary>
    /// CheckTaskUserControl
    /// </summary>
    public partial class CheckTaskUserControl : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox taskContent;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas oCanvas;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas oCamera;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Play;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressPain;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Clear;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label FrameSliderNumber;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider FrameSlider;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressFatigue;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\UserControls\CheckTaskUserControl.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar ProgressMood;
        
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
            System.Uri resourceLocater = new System.Uri("/KINECT_APPLICATION;component/usercontrols/checktaskusercontrol.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserControls\CheckTaskUserControl.xaml"
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
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.taskContent = ((System.Windows.Controls.ListBox)(target));
            
            #line 18 "..\..\..\UserControls\CheckTaskUserControl.xaml"
            this.taskContent.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.oCanvas = ((System.Windows.Controls.Canvas)(target));
            return;
            case 4:
            this.oCamera = ((System.Windows.Controls.Canvas)(target));
            return;
            case 5:
            this.Play = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\UserControls\CheckTaskUserControl.xaml"
            this.Play.Click += new System.Windows.RoutedEventHandler(this.Play_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ProgressPain = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 7:
            this.Clear = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\UserControls\CheckTaskUserControl.xaml"
            this.Clear.Click += new System.Windows.RoutedEventHandler(this.Clear_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.FrameSliderNumber = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.FrameSlider = ((System.Windows.Controls.Slider)(target));
            
            #line 38 "..\..\..\UserControls\CheckTaskUserControl.xaml"
            this.FrameSlider.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.ValueChanged);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ProgressFatigue = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 11:
            
            #line 49 "..\..\..\UserControls\CheckTaskUserControl.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.ProgressMood = ((System.Windows.Controls.ProgressBar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

