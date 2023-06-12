using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AboutRangeSliderDemo {
    public class MediaItemTemplateSelector : DataTemplateSelector {
        public MediaItemTemplateSelector()
        {
            
        }
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate VideoTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container) {
            ImageObject imageObject = item as ImageObject;
            if (imageObject != null) {
                switch (imageObject.Type) {
                    case MediaType.Image:
                        return ImageTemplate;
                    case MediaType.Video:
                        return VideoTemplate;
                }
            }
            return base.SelectTemplate(item, container);
        }
    }
}
