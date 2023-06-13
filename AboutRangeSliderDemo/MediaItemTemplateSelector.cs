using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AboutRangeSliderDemo {
    public class MediaItemTemplateSelector : DataTemplateSelector {
        public DataTemplate ImageTemplate { get; set; }
        public DataTemplate VideoTemplate { get; set; }
        private MediaElement lastMediaElement = null;
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

        //public override DataTemplate SelectTemplate(object item, DependencyObject container) {
        //    ImageObject mediaItem = item as ImageObject;

        //    if (mediaItem != null) {
        //        switch (mediaItem.Type) {
        //            case MediaType.Image:
        //                // 如果上一个 media item 是 video, 则停止 audio
        //                if (lastMediaElement != null && lastMediaElement.CurrentState == MediaElementState.Playing) {
        //                    lastMediaElement.Stop();
        //                    lastMediaElement.MediaEnded -= MediaElement_MediaEnded;
        //                }
        //                return ImageTemplate;
        //            case MediaType.Video:
        //                // 如果上一个 media item 是 video, 则停止 audio
        //                if (lastMediaElement != null && lastMediaElement.CurrentState == MediaElementState.Playing) {
        //                    lastMediaElement.Stop();
        //                    lastMediaElement.MediaEnded -= MediaElement_MediaEnded;
        //                }
        //                // 如果当前 media item particle is playing，则暂停and remove
        //                if (mediaItem.MediaElement != null && mediaItem.State == MediaState.Playing) {
        //                    mediaItem.MediaElement.Pause();
        //                    (container as FrameworkElement).Unloaded += Container_Unloaded;
        //                }
        //                // 绑定事件，监听 video 的结束
        //                if (mediaItem.MediaElement != null) {
        //                    mediaItem.MediaElement.MediaEnded += MediaElement_MediaEnded;
        //                }
        //                // 记录当前 media element element
        //                lastMediaElement = mediaItem.MediaElement;
        //                return VideoTemplate;
        //        }
        //    }

        //    return null;
        //}

        //private void MediaElement_MediaEnded(object sender, RoutedEventArgs e) {
        //    // 当一个 video 结束时，停止 audio
        //    if (lastMediaElement != null && lastMediaElement.CurrentState == MediaElementState.Playing) {
        //        lastMediaElement.Stop();
        //        lastMediaElement.MediaEnded -= MediaElement_MediaEnded;
        //    }
        //}

        //private void Container_Unloaded(object sender, RoutedEventArgs e) {
        //    // 当一个 item 被卸载时，remove event listener & media element
        //    if (sender is FrameworkElement container) {
        //        container.Unloaded -= Container_Unloaded;
        //        if (container.DataContext is MediaItem mediaItem && mediaItem.MediaElement != null) {
        //            mediaItem.MediaElement.Stop();
        //            mediaItem.MediaElement.MediaEnded -= MediaElement_MediaEnded;
        //        }
        //    }
        //}
    }
}
