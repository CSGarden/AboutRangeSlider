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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AboutRangeSliderDemo {
    public class CentreSlider : Slider {
        protected override void OnValueChanged(double oldValue, double newValue) {
            base.OnValueChanged(oldValue, newValue);
            RefreshSlider();
        }
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo) {
            base.OnRenderSizeChanged(sizeInfo);
            RefreshSlider();
        }
        public double LineX1 {
            get { return (double)GetValue(LineX1Property); }
            set { SetValue(LineX1Property, value); }
        }
        public double LineX2 {
            get { return (double)GetValue(LineX2Property); }
            set { SetValue(LineX2Property, value); }
        }

        // Using a DependencyProperty as the backing store for LineX2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineX2Property =
            DependencyProperty.Register("LineX2", typeof(double), typeof(CentreSlider), new PropertyMetadata(0.0));
        // Using a DependencyProperty as the backing store for LineX1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LineX1Property =
            DependencyProperty.Register("LineX1", typeof(double), typeof(CentreSlider), new PropertyMetadata(0.0));

        public void RefreshSlider() {
            var Proportion = ActualWidth / Maximum;
            LineX1 = ActualWidth / 2;
            LineX2 = Value * Proportion;
        }
    }
}
