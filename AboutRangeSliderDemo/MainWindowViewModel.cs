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
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image,MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/1.jpg", Title = "11", Description = "1" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/2.jpg", Title = "22", Description = "2" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Video, MediaPath = @"F:\chen\Videos\lay\【张艺兴 ｜ 大航海2】旧金山站饭拍合集（更新中） 《莲》+开场VCR cr.微雨何须归.mp4", Title = "视频", Description = ".3" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/4.jpg", Title = "33", Description = ".4" });
            ImageObjects.Add(new ImageObject() { Type = MediaType.Image, MediaPath = "pack://application:,,,/AboutRangeSliderDemo;component/5.jpg", Title = "44", Description = ".5" });
        }
        private ObservableCollection<ImageObject> imageObjects= new ObservableCollection<ImageObject>();
        public ObservableCollection<ImageObject> ImageObjects {
            get {
                return imageObjects;
            }
            set {
                Set(ref imageObjects, value);
            }
        }



    }
}
