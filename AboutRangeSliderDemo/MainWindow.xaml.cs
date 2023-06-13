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
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            DataContext= new MainWindowViewModel();
        }

        private void MediaElement_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
            var mediaElement = sender as MediaElement;
            if (mediaElement != null) {
                if ((bool)e.NewValue) {
                    mediaElement.Play();
                } else {
                    mediaElement.Pause();
                }
            }       
        }

    }

}
