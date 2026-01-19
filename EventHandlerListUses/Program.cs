using System.ComponentModel;

namespace EventHandlerListUses
{
    class Program
    {
        static void Main(string[] args)
        {
            //FileEventArgs file = new FileEventArgs
            //{
            //    FileName = "Result.pdf"
            //};


            DownloadCreatorPub downloadCreatorPub = new DownloadCreatorPub();
            DonwloadListenSub donwloadListenSub = new DonwloadListenSub(downloadCreatorPub);

            //downloadCreatorPub.Start(file);
            downloadCreatorPub.Start(new UrlEventArgs { Url = "http://example.com/error" });
            Console.WriteLine();
            downloadCreatorPub.Start(new UrlEventArgs { Url = "http://example.com/main.csv" });
        }
    }

    public class DownloadCreatorPub
    {
        private static readonly object DownloadStart = new object();
        private static readonly object DownloadProgress = new object();
        private static readonly object DownloadEnd = new object();

        private EventHandlerList events = new EventHandlerList();

        //public event EventHandler<FileEventArgs> StartDownload
        public event EventHandler<UrlEventArgs> StartDownload
        {
            add
            {
                events.AddHandler(DownloadStart, value);
            }
            remove
            {
                events.RemoveHandler(DownloadStart, value);
            }
        }
        //public event EventHandler<FileEventArgs> ProgressDownload
        public event EventHandler<UrlEventArgs> ProgressDownload
        {
            add
            {
                events.AddHandler(DownloadProgress, value);
            }
            remove
            {
                events.RemoveHandler(DownloadProgress, value);
            }
        }
        //public event EventHandler<FileEventArgs> EndDownload
        public event EventHandler<UrlEventArgs> EndDownload
        {
            add
            {
                events.AddHandler(DownloadEnd, value);
            }
            remove
            {
                events.RemoveHandler(DownloadEnd, value);
            }
        }


        // if you use FileEventArgs then change a UrlEventArgs and that instense
        protected virtual void OnStartDownload()
        {
            var handler = (EventHandler<UrlEventArgs>)events[DownloadStart];
            handler?.Invoke(this, new UrlEventArgs());
        }
        protected virtual void OnProgressDownload(UrlEventArgs url)
        {
            var handler = (EventHandler<UrlEventArgs>)events[DownloadProgress];
            handler?.Invoke(this, url);
        }
        protected virtual void OnEndDownload()
        {
            var handler = (EventHandler<UrlEventArgs>)events[DownloadEnd];
            handler?.Invoke(this, new UrlEventArgs());
        }

        //public void Start(FileEventArgs file)
        //{
        //    Console.WriteLine($"Download Manager Start..!");
        //    OnStartDownload();
        //    OnProgressDownload(file);
        //    OnEndDownload();
        //    Console.WriteLine($"Download Manager Stoped..!");
        //}

        public void Start(UrlEventArgs url)
        {
            Console.WriteLine($"Download Manager Start..!");
            OnStartDownload();
            Thread.Sleep(2000);

            if (url.Url.Contains("error"))
            {
                Console.WriteLine($"File can't download!");
            }
            else
            {
                OnProgressDownload(url);
                Thread.Sleep(2000);
                OnEndDownload();
            }

            Console.WriteLine($"Download Manager Stoped..!");
        }
    }

    public class DonwloadListenSub
    {
        public DonwloadListenSub(DownloadCreatorPub downloadCreatorPub)
        {
            downloadCreatorPub.StartDownload += DownloadCreatorPub_StartDownload;
            downloadCreatorPub.ProgressDownload += DownloadCreatorPub_ProgressDownload;
            downloadCreatorPub.EndDownload += DownloadCreatorPub_EndDownload;
        }

        private void DownloadCreatorPub_StartDownload(object? sender, EventArgs e)
        {
            Console.WriteLine($"File downloading....");
        }
        // also change here if use FileEventArgs
        private void DownloadCreatorPub_ProgressDownload(object? sender, UrlEventArgs e)
        {
            string fileName = Path.GetFileName(e.Url);
            Console.WriteLine($"Donwload progress, File : {fileName}");
        }
        private void DownloadCreatorPub_EndDownload(object? sender, EventArgs e)
        {
            Console.WriteLine($"File download completed.");
        }
    }

    public class FileEventArgs : EventArgs
    {
        public string FileName { get; set; }
    }

    public class UrlEventArgs : EventArgs
    {
        public string Url { get; set; }
    }
}
