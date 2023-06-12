using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AboutRangeSliderDemo.Controls {
    [TemplatePart(Name = PART_PrevRepeatButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = PART_NextRepeatButton, Type = typeof(RepeatButton))]
    [TemplatePart(Name = PART_IndexListBox, Type = typeof(ListBox))]
    [TemplatePart(Name = PART_ScrollViewer, Type = typeof(ScrollViewer))]
    public class Carousel : ListBox {
        static Carousel() {
            PrevCommand = new RoutedCommand("Prev", typeof(Carousel));
            NextCommand = new RoutedCommand("Next", typeof(Carousel));

            CommandManager.RegisterClassCommandBinding(typeof(Carousel),
                new CommandBinding(PrevCommand, OnExecutePrevCommand, OnCanExecutePrevCommand));
            CommandManager.RegisterClassCommandBinding(typeof(Carousel),
               new CommandBinding(NextCommand, OnExecuteNextCommand, OnCanExecuteNextCommand));
        }
        private const string PART_PrevRepeatButton = nameof(PART_PrevRepeatButton);
        private const string PART_NextRepeatButton = nameof(PART_NextRepeatButton);
        private const string PART_IndexListBox = nameof(PART_IndexListBox);
        private const string PART_ScrollViewer = nameof(PART_ScrollViewer);

        private DoubleAnimation animation;
        private ScrollViewer scrollViewer;
        private ListBox indexListBox;
        private RepeatButton prevRepeatButton;
        private RepeatButton nextRepeatButton;
        public Carousel() {
            DefaultStyleKey = typeof(Carousel);
            animation = new DoubleAnimation();
            animation.Duration = AnimationDuration;
            animation.EasingFunction = new CubicEase() { EasingMode = EasingMode.EaseInOut };
        }
        /// <summary>
        /// 
        /// </summary>
        public object ExtendObject {
            get { return (object)GetValue(ExtendObjectProperty); }
            set { SetValue(ExtendObjectProperty, value); }
        }


        public Duration AnimationDuration {
            get { return (Duration)GetValue(AnimationDurationProperty); }
            set { SetValue(AnimationDurationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AnimationDuration.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AnimationDurationProperty =
            DependencyProperty.Register("AnimationDuration", typeof(Duration), typeof(Carousel), new PropertyMetadata(new Duration(TimeSpan.FromMilliseconds(1000)), OnAnimationDurationChanged));

        // Using a DependencyProperty as the backing store for ExtendObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExtendObjectProperty =
            DependencyProperty.Register("ExtendObject", typeof(object), typeof(Carousel), new PropertyMetadata(default(object)));
        private static void OnAnimationDurationChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            if (d is Carousel carousel) {
                carousel.animation.Duration = (Duration)e.NewValue;
            }
        }
        /// <summary>
        /// 通过定义路由命令，可以在不同的对象和控件间共享相同的命令逻辑。
        /// 例如，在此示例中，PrevCommand 路由命令可以与 Carousel 控件中的 RepeatButton 按钮相关联，以便在用户点击 RepeatButton 时触发 PrevCommand 命令并调用逻辑方法。
        /// 这使得命令逻辑和控件的外观和行为绑定在一起，使代码更加模块化、易于维护和重用。
        /// </summary>
        public static RoutedCommand PrevCommand { get; private set; }
        private static void OnCanExecutePrevCommand(object sender, CanExecuteRoutedEventArgs e) {
            var carousel = (Carousel)sender;

            e.CanExecute = carousel.SelectedIndex > 0;
        }

        private static void OnExecutePrevCommand(object sender, ExecutedRoutedEventArgs e) {
            var carousel = (Carousel)sender;
            carousel.SelectedIndex -= 1;
        }
        public static RoutedCommand NextCommand { get; private set; }

        private static void OnCanExecuteNextCommand(object sender, CanExecuteRoutedEventArgs e) {
            var carousel = (Carousel)sender;

            e.CanExecute = carousel.SelectedIndex > -1 && carousel.Items.Count > 0;
        }

        private static void OnExecuteNextCommand(object sender, ExecutedRoutedEventArgs e) {
            var carousel = (Carousel)sender;
            carousel.SelectedIndex += 1;
        }

        //滚动到指定位置时执行动画效果
        private void Animation(double from, double to, Action action) {
            if (scrollViewer == null) {
                return;
            }
            animation.From = from;
            animation.To = to;
            scrollViewer.BeginAnimation(ScrollViewerAttacher.HorizontalOffsetProperty, animation);
            animation.Completed += (s, e) => {
                action?.Invoke();
            };
        }

        private (int Start, int End) ScrollDirection(SelectionChangedEventArgs e) {
            var start = e.RemovedItems.Cast<object>().FirstOrDefault();
            var end = e.AddedItems.Cast<object>().FirstOrDefault();
            if (start == null || end == null) {
                return (0, 0);
            }
            return (Items.IndexOf(start), Items.IndexOf(end));
        }

        protected override void OnSelectionChanged(SelectionChangedEventArgs e) {
            base.OnSelectionChanged(e);
            if (scrollViewer == null) { return; }
            var result = ScrollDirection(e);
            var offset = result.Start - result.End;
            if (offset == -1) {
                var item = ItemContainerGenerator.ContainerFromIndex(result.Start) as FrameworkElement;
                var from = scrollViewer.HorizontalOffset;
                var to = scrollViewer.HorizontalOffset + item.ActualWidth;
                Animation(from, to, () => {
                    ScrollIntoView(SelectedItem);
                });
            } else if (offset == 1) {
                var item = ItemContainerGenerator.ContainerFromIndex(result.Start) as FrameworkElement;
                var from = scrollViewer.HorizontalOffset;
                var to = scrollViewer.HorizontalOffset - item.ActualWidth;
                Animation(from, to, () => {
                    ScrollIntoView(SelectedItem);
                });
            } else {
                ScrollIntoView(SelectedItem);
            }
        }
        protected override DependencyObject GetContainerForItemOverride() {
            return new CarouselItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item) {
            return item is CarouselItem;
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            prevRepeatButton = GetTemplateChild(PART_PrevRepeatButton) as RepeatButton;
            nextRepeatButton = GetTemplateChild(PART_NextRepeatButton) as RepeatButton;
            indexListBox = GetTemplateChild(PART_IndexListBox) as ListBox;
            scrollViewer = GetTemplateChild(PART_ScrollViewer) as ScrollViewer;
        }
    }
}
