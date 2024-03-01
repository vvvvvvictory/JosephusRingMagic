using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace JosephusRingMagic
{
  /// <summary>
  /// RegionSelector.xaml 的交互逻辑
  /// </summary>
  public partial class RegionSelector : Window
  {
    public int RegionCode { get; private set; }
    public RegionSelector()
    {
      InitializeComponent();
    }

    private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed) {
        DragMove();
      }
    }

    private void South_Button_OnClick(object sender, RoutedEventArgs e)
    {
      RegionCode = 1;
      Close();
    }

    private void North_Button_OnClick(object sender, RoutedEventArgs e)
    {
      RegionCode = 2;
      Close();
    }

    private void Unknown_Button_OnClick(object sender, RoutedEventArgs e)
    {
      RegionCode = 3;
      Close();
    }
  }
}
