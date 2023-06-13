using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AboutRangeSliderDemo {
    public class ImageObject :ObservableObject{
        public MediaType Type { get; set; }

        public string MediaPath { get; set; }

        public string Title { get; set; }
        public MediaElement MediaElement { get; set; }

        public string Description { get; set; }
        public MediaState State { get; set; }
    }

    public enum MediaType {
        Image,
        Video
    }
    public enum MediaState {
        Playing,
        Paused
    }
}
