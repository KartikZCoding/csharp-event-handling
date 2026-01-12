using System;
using System.Collections.Generic;
using System.Text;

namespace EventHandling
{
    //public class VideoEventArgs : EventArgs
    //{
    //    public Video Video { get; set; }
    //}
    public class VideoEncoder 
    {
        // 1. Define a delegate
        //public delegate void VideoEncoderEventHandler(object source, VideoEventArgs args);

        // 2. Define an event based on that delegate
        public event EventHandler VideoEncoded;
        //public event EventHandler<VideoEventArgs> VideoEncoded;


        public void Encode(Video video)
        {
            Console.WriteLine($"Video {video.Title} encoding...!");
            Thread.Sleep(3000);

            //OnVideoEncoded(video);
            OnVideoEncoded();
        }

        // 3. Raise the event
        protected virtual void OnVideoEncoded() // OnVideoEncoded(Video video)
        {
            if(VideoEncoded != null)
            {
                VideoEncoded(this, EventArgs.Empty);
                //VideoEncoded(this, new VideoEventArgs() { Video = video });
            }
        }
    }
}
