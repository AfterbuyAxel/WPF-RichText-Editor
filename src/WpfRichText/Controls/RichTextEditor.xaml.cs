using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRichText
{
    /// <summary>
    /// Interaction logic for BindableRichTextbox.xaml
    /// </summary>
    public partial class RichTextEditor : UserControl
    {
		/// <summary></summary>
		public static readonly DependencyProperty TextProperty =
		  DependencyProperty.Register("Text", typeof(string), typeof(RichTextEditor),
		  new PropertyMetadata(string.Empty));

		/// <summary></summary>
		public static readonly DependencyProperty IsToolbarVisibleProperty =
		  DependencyProperty.Register("IsToolbarVisible", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(true));

		/// <summary></summary>
		public static readonly DependencyProperty IsContextMenuEnabledProperty =
		  DependencyProperty.Register("IsContextMenuEnabled", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(true));

		/// <summary></summary>
		public RichTextEditor()
        {
            InitializeComponent();
		}

		/// <summary></summary>
		public string Text
		{
			get { return GetValue(TextProperty) as string; }
			set
			{
				SetValue(TextProperty, value);
			}
		}

		/// <summary></summary>
		public bool IsToolbarVisible
		{
			get { return (GetValue(IsToolbarVisibleProperty) as bool? == true); }
			set
			{
				SetValue(IsToolbarVisibleProperty, value);
			}
		}

		/// <summary></summary>
		public bool IsContextMenuEnabled
		{
			get 
			{ 
				return (GetValue(IsContextMenuEnabledProperty) as bool? == true);
			}
			set
			{
				if (value)
					this.mainRTB.SetResourceReference(RichTextBox.ContextMenuProperty, "rtbContextMenu");
				else
					this.mainRTB.ContextMenu = null;
				SetValue(IsContextMenuEnabledProperty, value);
			}
		}

		private void mainRTB_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			if (!IsContextMenuEnabled)
			{
				this.mainRTB.ContextMenu.Visibility = System.Windows.Visibility.Collapsed;
				this.mainRTB.ContextMenu.IsEnabled = false;
			}
		}
	}
}
