using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace Vizpower_Plugin_Installer__WPF_
{
    /// <summary>
    /// Interaction logic for LoadingWait.xaml
    /// </summary>
    public partial class LoadingWait : UserControl
    {
        #region 参数
        //     集成到按指定时间间隔和指定优先级处理的 System.Windows.Threading.Dispatcher 队列中的计时器。
        private readonly DispatcherTimer animationTimer;
        #endregion

        #region 构造方法
        /// <summary>
        /// 构造方法
        /// </summary>
        public LoadingWait()
        {
            InitializeComponent();
            animationTimer = new DispatcherTimer(DispatcherPriority.ContextIdle, Dispatcher);
            animationTimer.Interval = new TimeSpan(0, 0, 0, 0, 75); //指定时间间隔
        }
        #endregion

        #region 属性
        #region 得到要显示的提示信息
        /// <summary>
        /// 得到要显示的提示信息
        /// </summary>
        public string TipContent
        {
            get { return (string)GetValue(TipContentProperty); }
            set { SetValue(TipContentProperty, value); }
        }

        public static readonly DependencyProperty TipContentProperty = DependencyProperty.Register("TipContent", typeof(string), typeof(LoadingWait), new UIPropertyMetadata("正在处理..."));
        #endregion

        #region 按钮的名称--例如：取消
        /// <summary>
        /// 按钮的名称
        /// </summary>
        public object InnerContent
        {
            get { return (object)GetValue(InnerContentProperty); }
            set { SetValue(InnerContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerContentProperty =
            DependencyProperty.Register("InnerContent", typeof(object), typeof(LoadingWait), new UIPropertyMetadata(null));
        #endregion

        #region 按钮的显示或隐藏
        /// <summary>
        /// 按钮的显示与隐藏
        /// </summary>
        public Visibility InnerContentVisibility
        {
            get { return (Visibility)GetValue(InnerContentVisibilityProperty); }
            set { SetValue(InnerContentVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InnerContentVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerContentVisibilityProperty =
            DependencyProperty.Register("InnerContentVisibility", typeof(Visibility), typeof(LoadingWait), new UIPropertyMetadata(Visibility.Collapsed));
        #endregion
        #endregion

        #region 方法
        /// <summary>
        /// 开始方法
        /// </summary>
        private void Start()
        {
            //修改光标的样式，为等待状态
            //this.Cursor = Cursors.Wait;
            //超过计时器间隔时发生。
            animationTimer.Tick += HandleAnimationTick;
            animationTimer.Start();
        }

        /// <summary>
        /// 结束方法
        /// </summary>
        private void Stop()
        {
            animationTimer.Stop();
            //修改光标的样式，为箭头
            this.Cursor = Cursors.Arrow;
            animationTimer.Tick -= HandleAnimationTick;
        }

        /// <summary>
        /// 超过计时器间隔时发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleAnimationTick(object sender, EventArgs e)
        {
            //设置旋转角度
            SpinnerRotate.Angle = (SpinnerRotate.Angle + 36) % 360;
        }

        /// <summary>
        /// Canvas加载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            //圆周长就是：C = π * d 或者C=2*π*r（其中d是圆的直径，r是圆的半径）
            const double offset = Math.PI;  //π
            const double step = Math.PI * 2 / 10.0;

            SetPositin(C0, offset, 0.0, step);
            SetPositin(C1, offset, 1.0, step);
            SetPositin(C2, offset, 2.0, step);
            SetPositin(C3, offset, 3.0, step);
            SetPositin(C4, offset, 4.0, step);
            SetPositin(C5, offset, 5.0, step);
            SetPositin(C6, offset, 6.0, step);
            SetPositin(C7, offset, 7.0, step);
            SetPositin(C8, offset, 8.0, step);
            this.IsVisibleChanged -= HandleVisibleChanged;
            this.IsVisibleChanged += HandleVisibleChanged;
            ////  DesignerProperties   提供用于与设计器进行通信的附加属性。
            if (!DesignerProperties.GetIsInDesignMode(this))
            {
                if (this.Visibility == System.Windows.Visibility.Visible)
                {
                    Start();
                }
            }

        }

        /// <summary>
        /// Canvas卸载时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            Stop();
        }

        /// <summary>
        /// 确定圆的位置
        /// </summary>
        /// <param name="ellipse"></param>
        /// <param name="offset"></param>
        /// <param name="posOffSet"></param>
        /// <param name="step"></param>
        private void SetPositin(Ellipse ellipse, double offset, double posOffSet, double step)
        {
            ellipse.SetValue(Canvas.LeftProperty, 50.0 + Math.Sin(offset + posOffSet * step) * 50.0);

            ellipse.SetValue(Canvas.TopProperty, 50 + Math.Cos(offset + posOffSet * step) * 50.0);
        }

        /// <summary>
        /// 设置显示与隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            bool isVisible = (bool)e.NewValue;

            if (isVisible)
                Start();
            else
                Stop();
        }
        #endregion
    }
}
