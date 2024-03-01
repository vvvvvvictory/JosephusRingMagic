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
  /// GenderSelector.xaml 的交互逻辑
  /// </summary>
  public partial class GenderSelector : Window
  {
    /// <summary>
    /// 男: 1, 女：2
    /// </summary>
    public int GenderCode { get; private set; }
    public GenderSelector()
    {
      InitializeComponent();
    }

    private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton == MouseButtonState.Pressed) {
        DragMove();
      }
    }

    private void Woman_Button_OnClick(object sender, RoutedEventArgs e)
    {
      GenderCode = 2;
      Close();
    }

    private void Man_Button_OnClick(object sender, RoutedEventArgs e)
    {
      GenderCode = 1;
      Close();
    }
  }
}
