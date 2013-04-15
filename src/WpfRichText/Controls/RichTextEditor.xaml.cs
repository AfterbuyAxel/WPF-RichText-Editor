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
		public static readonly DependencyProperty IsReadOnlyProperty =
		  DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(RichTextEditor),
		  new PropertyMetadata(false));

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

		private TextRange textRange = null;

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
				SetValue(IsContextMenuEnabledProperty, value);
			}
		}

		/// <summary></summary>
		public bool IsReadOnly
		{
			get { return (GetValue(IsReadOnlyProperty) as bool? == true); }
			set
			{
				SetValue(IsReadOnlyProperty, value);
				if (value == true)
				{
					SetValue(IsToolBarVisibleProperty, false);
					SetValue(IsContextMenuEnabledProperty, false);
				}
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


		private void FontColorPicker_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
		{
			this.mainRTB.Selection.ApplyPropertyValue(ForegroundProperty, e.NewValue.ToString(CultureInfo.InvariantCulture));
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (this.mainRTB != null && this.mainRTB.Selection != null)
				this.mainRTB.Selection.ApplyPropertyValue(FontFamilyProperty, e.AddedItems[0]);
		}

		private void insertLink_Click(object sender, RoutedEventArgs e)
		{
			this.textRange = new TextRange(this.mainRTB.Selection.Start, this.mainRTB.Selection.End);
			this.uriInputPopup.IsOpen = true;
		}

		private void uriCancelClick(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			this.uriInputPopup.IsOpen = false;
			this.uriInput.Text = string.Empty;
		}

		private void uriSubmitClick(object sender, RoutedEventArgs e)
		{
			e.Handled = true;
			this.uriInputPopup.IsOpen = false;
			this.mainRTB.Selection.Select(this.textRange.Start, this.textRange.End);
			if (!string.IsNullOrEmpty(this.uriInput.Text))
			{
				this.textRange = new TextRange(this.mainRTB.Selection.Start, this.mainRTB.Selection.End);
				Hyperlink hlink = new Hyperlink(this.textRange.Start, this.textRange.End);
				hlink.NavigateUri = new Uri(this.uriInput.Text, UriKind.RelativeOrAbsolute);
				this.uriInput.Text = string.Empty;
			}
			else
				this.mainRTB.Selection.ClearAllProperties();			
		}

		private void uriInput_KeyPressed(object sender, KeyEventArgs e)
		{
			switch (e.Key)
			{
				case Key.Enter:
					this.uriSubmitClick(sender, e);
					break;
				case Key.Escape:
					this.uriCancelClick(sender, e);
					break;
				default:
					break;
			}
		}

	}
}
