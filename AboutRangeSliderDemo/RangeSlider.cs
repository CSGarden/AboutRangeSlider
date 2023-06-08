using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace AboutRangeSliderDemo {
    [TemplatePart(Name = PART_StartRegion, Type = typeof(RepeatButton))]
    [TemplatePart(Name = PART_MiddleRegion, Type = typeof(RepeatButton))]
    [TemplatePart(Name = PART_EndRegion, Type = typeof(RepeatButton))]
    [TemplatePart(Name = PART_StartThumb, Type = typeof(Thumb))]
    [TemplatePart(Name = PART_EndThumb, Type = typeof(Thumb))]
    public class RangeSlider : Control {

        enum ThumbKind {
            None,
            Start,
            End
        }
        private const string PART_StartRegion = nameof(PART_StartRegion);
        private const string PART_MiddleRegion = nameof(PART_MiddleRegion);
        private const string PART_EndRegion = nameof(PART_EndRegion);
        private const string PART_StartThumb = nameof(PART_StartThumb);
        private const string PART_EndThumb = nameof(PART_EndThumb);

        RepeatButton _StartRegion;
        RepeatButton _MiddleRegion;
        RepeatButton _EndRegion;
        Thumb _StartThumb;
        Thumb _EndThumb;

        //滑动事件注册 拖动开始 拖动中 拖动结束
        static RangeSlider() {
            EventManager.RegisterClassHandler(typeof(RangeSlider), Thumb.DragStartedEvent, new DragStartedEventHandler(OnDragStartedEvent));
            EventManager.RegisterClassHandler(typeof(RangeSlider), Thumb.DragDeltaEvent, new DragDeltaEventHandler(OnDragDeltaEvent));
            EventManager.RegisterClassHandler(typeof(RangeSlider), Thumb.DragCompletedEvent, new DragCompletedEventHandler(OnDragCompletedEvent));
        }
        public RangeSlider() {
            LowerValueChanged += RangeSlider_LowerValueChanged;
            UpperValueChanged += RangeSlider_UpperValueChanged;
        }

        private void RangeSlider_UpperValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            UpdateThumbPosition(ThumbKind.Start);
        }

        private void RangeSlider_LowerValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            UpdateThumbPosition(ThumbKind.End);
        }

        private static void OnDragCompletedEvent(object sender, DragCompletedEventArgs e) {
            throw new NotImplementedException();
        }

        private static void OnDragDeltaEvent(object sender, DragDeltaEventArgs e) {
            var slider = (RangeSlider)sender;
               slider?.OnDragDeltaEvent(e);
        }

        private void OnDragDeltaEvent(DragDeltaEventArgs e) {
            if (!CanUpdate()) {
                return;
            }
            var offect = e.HorizontalChange / Calculate() * (Math.Abs(MaxValue - MinValue));
            var thumb = e.OriginalSource as Thumb;
            if (thumb == _StartThumb) {
                LowerValue = Math.Min(Math.Max(MinValue, LowerValue + offect), UpperValue);
                UpdateThumbPosition(ThumbKind.Start);
            }
        }

        private static void OnDragStartedEvent(object sender, DragStartedEventArgs e) {
            throw new NotImplementedException();
        }

        public double LowerValue {
            get { return (double)GetValue(LowerValueProperty); }
            set { SetValue(LowerValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for LowerValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LowerValueProperty =
            DependencyProperty.Register("LowerValue", typeof(double), typeof(RangeSlider), new PropertyMetadata(25d, OnLowerValueChanged, OnCoerceLowerValue));

        private static object OnCoerceLowerValue(DependencyObject d, object baseValue) {
            var slider = (RangeSlider)d;
            var num = (double)baseValue;
            if (num < slider.MinValue) {
                throw new ArgumentException("lower must be>=max");
            }
            return num;
        }

        private static void OnLowerValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var slider = (RangeSlider)d;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue, LowerValueChangedEvent);
            slider.RaiseEvent(args);
        }



        public double UpperValue {
            get { return (double)GetValue(UpperValueProperty); }
            set { SetValue(UpperValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UpperValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UpperValueProperty =
            DependencyProperty.Register("UpperValue", typeof(double), typeof(RangeSlider), new PropertyMetadata(75d, OnUpperChanged, OnCoerceUpper));

        private static object OnCoerceUpper(DependencyObject d, object baseValue) {
            var slider = (RangeSlider)d;
            var num = (double)baseValue;
            if (num > slider.MaxValue) {
                throw new ArgumentException("upper must be<=max");
            }
            return num;
        }

        private static void OnUpperChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var slider = (RangeSlider)d;
            var args = new RoutedPropertyChangedEventArgs<double>((double)e.OldValue, (double)e.NewValue, UpperValueChangedEvent);
            slider.RaiseEvent(args);
        }



        public double MinValue {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MinValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(RangeSlider), new PropertyMetadata(0d, OnMinChanged, OnCoerceMin));

        private static object OnCoerceMin(DependencyObject d, object baseValue) {
            var slider = (RangeSlider)d;
            var num = (double)baseValue;
            if (num > slider.MaxValue) {
                throw new ArgumentException("max must be<=max");
            }
            return num;
        }

        private static void OnMinChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            throw new NotImplementedException();
        }

        #region 最大值
        public double MaxValue {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MinValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaxValueProperty =
            DependencyProperty.Register("MinValue", typeof(double), typeof(RangeSlider), new PropertyMetadata(100d, OnMaxChanged, OnCoerceMax));

        private static object OnCoerceMax(DependencyObject d, object baseValue) {
            var slider = (RangeSlider)d;
            var num = (double)baseValue;
            if (num < slider.MinValue) {
                throw new ArgumentException("max must be>=min");
            }
            return num;
        }

        private static void OnMaxChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            throw new NotImplementedException();
        }

        #endregion

        public static readonly RoutedEvent LowerValueChangedEvent = EventManager.RegisterRoutedEvent("LowerValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(RangeSlider));
        public event RoutedPropertyChangedEventHandler<double> LowerValueChanged {
            add { AddHandler(LowerValueChangedEvent, value); }
            remove { RemoveHandler(LowerValueChangedEvent, value); }
        }

        public static readonly RoutedEvent UpperValueChangedEvent = EventManager.RegisterRoutedEvent("UpperValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<double>), typeof(RangeSlider));
        public event RoutedPropertyChangedEventHandler<double> UpperValueChanged {
            add { AddHandler(UpperValueChangedEvent, value); }
            remove { RemoveHandler(UpperValueChangedEvent, value); }
        }

        #region 单独方法
        private double MapToRange(double value, (double Min, double Max) original, (double Min, double Max) target) {
            var num = (value - original.Min) / (original.Max - original.Min);
            return target.Min + num * (target.Max - target.Min);
        }
        #endregion

    private void UpdateThumbPosition(ThumbKind kind) {
            if (!CanUpdate()) {
                return;
            }
            var total = CanUpdate();
            double scale = 0d;
            switch (kind) {
                case ThumbKind.None:
                    break;
                case ThumbKind.Start:
                    scale = MapToRange(LowerValue, (MinValue, MaxValue), (0d, 1d));
                    _StartRegion.Width = scale * ActualWidth;
                    break;
                case ThumbKind.End:
                    scale = MapToRange(UpperValue, (MinValue, MaxValue), (0d, 1d));
                    _EndRegion.Width = scale * ActualWidth;
                    break;
                default:
                    break;
            }
        }
        bool CanUpdate() {
            return _StartThumb != null && _EndThumb != null && _StartRegion != null && _MiddleRegion != null && _EndRegion != null;
        }

        double Calculate() {
            return _StartRegion.ActualWidth + _MiddleRegion.ActualWidth + _EndRegion.ActualWidth;
        }
        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
            _StartRegion = GetTemplateChild(PART_StartRegion) as RepeatButton;
            _MiddleRegion = GetTemplateChild(PART_MiddleRegion) as RepeatButton;
            _EndRegion = GetTemplateChild(PART_EndRegion) as RepeatButton;
            _StartThumb = GetTemplateChild(PART_StartThumb) as Thumb;
            _EndThumb = GetTemplateChild(PART_EndThumb) as Thumb;
        }
    }
}
