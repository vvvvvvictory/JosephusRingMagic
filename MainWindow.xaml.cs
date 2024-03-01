using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace JosephusRingMagic
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private int _currentStep = 0;
    private Action[] _actions;
    private BitmapImage _hiddenCard;
    private List<BitmapImage> _pokerHost;
    private Image[,] _imageMap;
    private int _genderCode;

    public MainWindow()
    {
      InitializeComponent();
      InitializeFields();
      Step1();
    }

    private void InitializeFields()
    {
      // 初始化action
      _actions = new[] {
        Step1, Step2, Step3, Step4, Step5, Step6, Step7, Step8
      };

      // 初始化 _imageMap
      _imageMap = new Image[,] {
        {
          X_Image_00, X_Image_01, X_Image_02, X_Image_03, X_Image_04,
          X_Image_05, X_Image_06, X_Image_07
        }, {
          X_Image_10, X_Image_11, X_Image_12, X_Image_13, X_Image_14,
          X_Image_15, X_Image_16, X_Image_17
        }, {
          X_Image_20, X_Image_21, X_Image_22, X_Image_23, X_Image_24,
          X_Image_25, X_Image_26, X_Image_27
        }
      };
    }

    private void FloatUp(Image img, double ratio)
    {
      double width = img.ActualWidth;
      double height = img.ActualHeight;
      double clipHeight = height / 2;

      // 创建裁剪区域
      RectangleGeometry clipGeometry =
        new RectangleGeometry(new Rect(0, clipHeight, width, clipHeight));

      // 更新图像的 Clip 属性为裁剪区域
      img.Clip = clipGeometry;

      // 计算偏移量
      double yOffset = -clipHeight * ratio; // 向上偏移一半高度
      Transform renderTransform = img.RenderTransform;
      // 如果 RenderTransform 不是一个 TransformGroup，则创建一个新的 TransformGroup，并将其设置为 RenderTransform
      if (renderTransform is not TransformGroup) {
        renderTransform = new TransformGroup();
        img.RenderTransform = renderTransform;
      }

      // 将向上偏移的 TranslateTransform 添加到 TransformGroup 中
      ((TransformGroup)renderTransform).Children.Clear();
      ((TransformGroup)renderTransform).Children.Add(
        new TranslateTransform(0, yOffset));
    }

    private void SinkDown(Image img, double ratio)
    {
      double width = img.ActualWidth;
      double height = img.ActualHeight;
      double clipHeight = height / 2;

      // 创建裁剪区域，裁剪掉下半部分
      RectangleGeometry clipGeometry =
        new RectangleGeometry(new Rect(0, 0, width, clipHeight));
      img.Clip = clipGeometry;

      // 计算偏移量
      double yOffset = clipHeight * ratio;
      Transform renderTransform = img.RenderTransform;
      if (renderTransform is not TransformGroup) {
        renderTransform = new TransformGroup();
        img.RenderTransform = renderTransform;
      }

      // 将向下偏移的 TranslateTransform 添加到 TransformGroup 中
      ((TransformGroup)renderTransform).Children.Clear();
      ((TransformGroup)renderTransform).Children.Add(
        new TranslateTransform(0, yOffset));
    }

    private void OnUpperSizeChanged(object sender, SizeChangedEventArgs e)
    {
      Image img = (e.OriginalSource as Image)!;
      if (img is { ActualHeight: > 0, ActualWidth: > 0 }) {
        SinkDown(img, 0.95);
      }
    }

    private void OnUpperSizeChanged_For_Step3(object sender,
      SizeChangedEventArgs e)
    {
      Image img = (e.OriginalSource as Image)!;
      if (img is { ActualHeight: > 0, ActualWidth: > 0 }) {
        SinkDown(img, 0.5);
      }
    }

    private void OnBottomSizeChanged(object sender, SizeChangedEventArgs e)
    {
      Image img = (e.OriginalSource as Image)!;
      if (img.ActualHeight > 0) {
        FloatUp(img, 0.95);
      }
    }

    private void OnBottomSizeChanged_For_Step3(object sender,
      SizeChangedEventArgs e)
    {
      Image img = (e.OriginalSource as Image)!;
      if (img.ActualHeight > 0) {
        FloatUp(img, 0.5);
      }
    }

    private void NextStep_OnClick(object sender, RoutedEventArgs e)
    {
      _currentStep = (_currentStep + 1) % _actions.Length;
      _actions[_currentStep]();
    }

    /// <summary>
    /// 随机选择四张扑克牌并显示
    /// </summary>
    private void Step1()
    {
      #region 收尾工作

      // 还原所有图像网格
      X_Image_03.Source = null;
      X_Image_23.Source = null;
      X_Image_03.SetValue(Grid.ColumnSpanProperty, 1);
      X_Image_23.SetValue(Grid.ColumnSpanProperty, 1);
      X_Image_03.SizeChanged -= OnUpperSizeChanged;
      X_Image_23.SizeChanged -= OnBottomSizeChanged;

      // 还原数据
      _miracleIndex = 0;
      for (int i = 0; i < 8; ++i) {
        _imageMap[1, i].Clip = null;
        Transform renderTransform = _imageMap[1, i].RenderTransform;
        // 移出renderTransform
        if (renderTransform is not TransformGroup) {
          renderTransform = new TransformGroup();
          _imageMap[1, i].RenderTransform = renderTransform;
        }
        ((TransformGroup)renderTransform).Children.Clear();
      }

      #endregion


      X_Desc.Text = "Step 1: 随机选择四张扑克牌";
      X_NextStep.Content = "下一步";
      X_SplitLine.Visibility = Visibility.Collapsed;

      // 初始化 _pokerHost
      _pokerHost = new List<BitmapImage>();
      foreach (string uri in Poker.GetRandomCard(4)) {
        _pokerHost.Add(new BitmapImage(new Uri(uri)));
      }

      // 显示扑克牌
      int row = 1, col = 2;
      for (int i = 0; i < 4; ++i) {
        _imageMap[row, col + i].Source = _pokerHost[i];
      }
    }

    /// <summary>
    /// 将四张扑克牌从中间撕开
    /// </summary>
    private void Step2()
    {
      X_Desc.Text = "Step 2: 将四张扑克牌对折后从中间撕开";
      X_NextStep.Content = "下一步";

      // 显示分隔条
      X_SplitLine.Visibility = Visibility.Visible;

      // 删除中间的扑克牌，重新上下显示
      int row = 1, col = 2;
      for (int i = 0; i < 4; ++i) {
        _imageMap[row, col + i].Source = null;
      }

      for (int i = 0; i < 4; ++i) {
        // 第一行
        _imageMap[0, 2 + i].Source = _pokerHost[i];            // 更改显示源
        _imageMap[0, 2 + i].SizeChanged += OnUpperSizeChanged; // 应用自适应变换
        SinkDown(_imageMap[0, 2 + i], 0.95);                   // 应用Clip变换

        // 第三行
        _imageMap[2, 2 + i].Source = _pokerHost[i];             // 更改显示原
        _imageMap[2, 2 + i].SizeChanged += OnBottomSizeChanged; // 应用自适应变换
        FloatUp(_imageMap[2, 2 + i], 0.95);                     // 应用Clip变换
      }
    }

    /// <summary>
    /// 将下面四张放到后面
    /// </summary>
    private void Step3()
    {
      X_Desc.Text = "Step 3: 将撕下的扑克牌放到后面";
      X_SplitLine.Visibility = Visibility.Collapsed;
      X_NextStep.Content = "下一步";

      // 还原上一步操作
      for (int i = 0; i < 4; ++i) {
        // 第一行
        _imageMap[0, 2 + i].Source = null;                     // 更改显示源
        _imageMap[0, 2 + i].SizeChanged -= OnUpperSizeChanged; // 应用自适应变换
        // SinkDown(_imageMap[0, 2 + i], 0.95);                   // 应用Clip变换

        // 第三行
        _imageMap[2, 2 + i].Source = null;                      // 更改显示原
        _imageMap[2, 2 + i].SizeChanged -= OnBottomSizeChanged; // 应用自适应变换
        // FloatUp(_imageMap[2, 2 + i], 0.95);                     // 应用Clip变换
      }

      // 扩展 _pokerHost
      _pokerHost.AddRange(_pokerHost);

      // 重新显示所有扑克排列
      for (int i = 0; i < 8; ++i) {
        _imageMap[1, i].Source = _pokerHost[i];
        // 前四张显示上半部分，后四张显示下半部分
        if (i < 4) {
          SinkDown(_imageMap[1, i], 0.5);
          _imageMap[1, i].SizeChanged += OnUpperSizeChanged_For_Step3;
        }
        else {
          FloatUp(_imageMap[1, i], 0.5);
          _imageMap[1, i].SizeChanged += OnBottomSizeChanged_For_Step3;
        }
      }
    }

    /// <summary>
    /// 根据名字字数选择多少张牌放到最后面
    /// </summary>
    private void Step4()
    {
      X_Desc.Text = "Step 4: 根据名字子数放几张牌到后面";
      X_NextStep.Content = "下一步";

      // 获取用户输入的名字
      int usernameLength = 0;
      while (usernameLength == 0) {
        string userInput =
          Microsoft.VisualBasic.Interaction.InputBox("请问你的名字几个字符？", "请输入名字字数",
            "");
        if (int.TryParse(userInput, out usernameLength)) {
          break;
        }

        MessageBox.Show("无效的输入，请重试", "Error", MessageBoxButton.OK,
          MessageBoxImage.Error);
      }

      // 定义反转函数
      void Reverse(List<BitmapImage> list, int start, int end)
      {
        while (start < end) {
          (list[start], list[end]) = (list[end], list[start]);
          ++start;
          --end;
        }
      }

      // 移动
      int shift = usernameLength % _pokerHost.Count;
      Reverse(_pokerHost, 0, shift - 1);
      Reverse(_pokerHost, shift, _pokerHost.Count - 1);
      Reverse(_pokerHost, 0, _pokerHost.Count - 1);

      // 显示
      for (int i = 0; i < 8; ++i) {
        _imageMap[1, i].Source = _pokerHost[i];
      }
    }

    /// <summary>
    /// 将最上面三张牌插到其余牌堆中间
    /// </summary>
    private void Step5()
    {
      X_Desc.Text = "Step 5: 将最上面三张牌插到其余牌堆中间";
      X_NextStep.Content = "下一步";

      // 更新
      List<BitmapImage> firstThree = _pokerHost.GetRange(0, 3);
      _pokerHost.RemoveRange(0, 3);
      int inertIndex = _pokerHost.Count / 2;
      _pokerHost.InsertRange(inertIndex, firstThree);

      // 显示
      for (int i = 0; i < 8; ++i) {
        _imageMap[1, i].Source = _pokerHost[i];
      }
    }

    /// <summary>
    /// 将最上面一张牌藏起来并询问是哪里人
    /// </summary>
    private void Step6()
    {
      X_Desc.Text = "Step 6: 将最上面一张藏起来";
      X_NextStep.Content = "下一步";

      // 保存第一张并显示为背面
      _hiddenCard = _pokerHost[0]; // 保存第一张
      _pokerHost.RemoveAt(0);      // 移出第一张
      _imageMap[1, 0].Source =
        new BitmapImage(new Uri(Poker.GetCard(string.Empty, 16))); // 显示第一张为背面

      // 获取用户选择
      RegionSelector rs = new RegionSelector() { Owner = this };
      rs.ShowDialog();
      int regionCode = rs.RegionCode;

      // 根据是哪里人拿起相应数量的牌插入到剩余的牌堆中间
      List<BitmapImage> tmp = _pokerHost.GetRange(0, regionCode);
      _pokerHost.RemoveRange(0, regionCode);
      int insertIndex = _pokerHost.Count / 2;
      _pokerHost.InsertRange(insertIndex, tmp);

      // 显示
      for (int i = 0; i < _pokerHost.Count; ++i) {
        // +1 跳过隐藏的第一张
        _imageMap[1, 1 + i].Source = _pokerHost[i];
      }
    }

    /// <summary>
    /// 根据男生或女生扔掉一张或两张牌
    /// </summary>
    private void Step7()
    {
      X_Desc.Text = "Step 7: 根据男生或女生扔掉一张或两张牌";
      X_NextStep.Content = "下一步";

      // 获取用户性别选择
      GenderSelector gs = new GenderSelector() { Owner = this };
      gs.ShowDialog();
      _genderCode = gs.GenderCode;

      // 更新
      _pokerHost.RemoveRange(0, _genderCode);

      // 显示
      _imageMap[1, 1].Source = null;
      if (_genderCode == 2) {
        _imageMap[1, 2].Source = null;
      }
    }

    /// <summary>
    /// 见证奇迹的时刻
    /// </summary>
    private void Step8()
    {
      X_Desc.Text = "Step 8: 见证奇迹的时刻";
      X_NextStep.Content = "见";
      X_NextStep.Click -= NextStep_OnClick;        // 移出原来的事件响应函数
      X_NextStep.Click += WitnessAMomentOfMiracle; // 添加新的事件响应函数
    }

    private string[] _miracle =
      { "见", "证", "奇", "迹", "的", "时", "刻", "好运留下来，烦恼丢出去" };
    private int _miracleIndex = 0;

    /// <summary>
    /// 见证奇迹的时刻
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void WitnessAMomentOfMiracle(object sender, RoutedEventArgs e)
    {
      Button btn = (e.OriginalSource as Button)!;
      btn.Content = _miracle[++_miracleIndex];

      // 每念一个字，放一张牌到最后面，对应每一次按钮点击
      BitmapImage tmp2 = _pokerHost[0]; // 暂存
      _pokerHost.RemoveAt(0);           // 移出
      _pokerHost.Add(tmp2);             // 添加

      // 显示
      for (int i = 0; i < _pokerHost.Count; ++i) {
        _imageMap[1, 1 + _genderCode + i].Source = _pokerHost[i];
      }

      if (_miracleIndex == _miracle.Length - 1) {
        btn.Click -= WitnessAMomentOfMiracle;
        btn.Click += ThrownTroubleAndRemainsLuck;
      }
    }

    /// <summary>
    /// 好运留下来，烦恼丢出去
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ThrownTroubleAndRemainsLuck(object sender, RoutedEventArgs e)
    {
      Button btn = (e.OriginalSource as Button)!;

      if (_pokerHost.Count > 1) {
        // 保留最后一张
        BitmapImage luck = _pokerHost[0];    // 好运
        BitmapImage trouble = _pokerHost[1]; // 烦恼
        _pokerHost.RemoveRange(0, 2);        // 烦恼丢出去
        _pokerHost.Add(luck);                // 好运留下来

        int j = 0;
        for (int i = 0; i < 8; ++i) {
          if (i < 1 + _genderCode) {
            continue;
          }

          if (j < _pokerHost.Count) {
            _imageMap[1, i].Source = _pokerHost[j++];
          }
          else {
            _imageMap[1, i].Source = null;
          }
        }
      }
      else {
        // 刘谦祝福
        X_Desc.Text =
          "刘谦：朋友们，如果这一次成功了，这两张卡片会完全找到彼此的另外一半，你是男生，女生，" +
          "南方人，北方人，名字有几个字，在这一刻我们都会见证奇迹";
        // 清除所有卡片，显示最后两张
        for (int i = 0; i < 8; ++i) {
          _imageMap[1, i].Source = null;
          if (i < 4) {
            _imageMap[1, i].SizeChanged -= OnUpperSizeChanged_For_Step3;
          }
          else {
            _imageMap[1, i].SizeChanged -= OnBottomSizeChanged_For_Step3;
          }
        }

        // 更新源
        X_Image_03.Source = _hiddenCard;
        X_Image_23.Source = _pokerHost[0];
        // 设置跨列举中
        X_Image_03.SetValue(Grid.ColumnSpanProperty, 2);
        X_Image_23.SetValue(Grid.ColumnSpanProperty, 2);
        // 显示裁剪区域并添加自适应变换
        SinkDown(X_Image_03, 0.95);
        X_Image_03.SizeChanged += OnUpperSizeChanged;
        FloatUp(X_Image_23, 0.95);
        X_Image_23.SizeChanged += OnBottomSizeChanged;
        // 进入下一个循环
        btn.Click -= ThrownTroubleAndRemainsLuck;
        btn.Content = "再来一次";
        btn.Click += NextStep_OnClick;
      }
    }
  }
}