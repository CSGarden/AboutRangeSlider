using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AboutRangeSliderDemo {
    public class ImageObject :ObservableObject{
        public MediaType Type { get; set; }

        public string MediaPath { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }

    public enum MediaType {
        Image,
        Video
    }
}
