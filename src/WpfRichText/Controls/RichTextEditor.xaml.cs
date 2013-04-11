using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
		public static readonly DependencyProperty IsToolBarVisibleProperty =
		  DependencyProperty.Register("IsToolBarVisible", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(true));

		/// <summary></summary>
		public static readonly DependencyProperty IsContextMenuEnabledProperty =
		  DependencyProperty.Register("IsContextMenuEnabled", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(true));

		/// <summary></summary>
		public static readonly DependencyProperty AvailableFontsProperty =
		  DependencyProperty.Register("AvailableFonts", typeof(Collection<String>), typeof(RichTextEditor),
		  new PropertyMetadata(new Collection<String>(
			  new List<String>(4) 
			  {
				  "Arial",
				  "Courier New",
				  "Tahoma",
				  "Times New Roman"
			  }
		)));

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
		public bool IsToolBarVisible
		{
			get { return (GetValue(IsToolBarVisibleProperty) as bool? == true); }
			set
			{
				SetValue(IsToolBarVisibleProperty, value);
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

		/// <summary></summary>
		public Collection<String> AvailableFonts
		{
			get { return GetValue(AvailableFontsProperty) as Collection<String>; }
			set
			{
				SetValue(AvailableFontsProperty, value);
			}
		}


		private void ColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			this.mainRTB.Selection.ApplyPropertyValue(ForegroundProperty, e.NewValue.ToString(CultureInfo.InvariantCulture));
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.mainRTB != null && this.mainRTB.Selection != null)
				this.mainRTB.Selection.ApplyPropertyValue(FontFamilyProperty, e.AddedItems[0]);
		}
	}
}
