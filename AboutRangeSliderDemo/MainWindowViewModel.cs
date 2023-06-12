using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutRangeSliderDemo {
    public class MainWindowViewModel :ViewModelBase{
        public MainWindowViewModel() {
            ImageObjects = new ObservableCollection<ImageObject>();
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image,MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/1.jpeg", Title = "First Image", Description = "1" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/2.jpeg", Title = "Second Image", Description = "2" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/3.jpeg", Title = "Third Image", Description = ".3" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/4.jpeg", Title = "Four Image", Description = ".4" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/5.jpeg", Title = "Five Image", Description = ".5" });
        }
        public ObservableCollection<ImageObject> ImageObjects { get; set; }

    }
}
