using System.ComponentModel;

namespace EventHandlerListUses
{
    class Program
    {
        static void Main(string[] args)
        {
            FileEventArgs file = new FileEventArgs
            {
                FileName = "Result.pdf"
            };
            DownloadCreatorPub downloadCreatorPub = new DownloadCreatorPub();
            DonwloadListenSub donwloadListenSub = new DonwloadListenSub(downloadCreatorPub);

            downloadCreatorPub.Start(file);
        }
    }

    public class DownloadCreatorPub
    {
        private static readonly object DownloadStart = new object();
        private static readonly object DownloadProgress = new object();
        private static readonly object DownloadEnd = new object();

        private EventHandlerList events = new EventHandlerList();

        public event EventHandler<FileEventArgs> StartDownload
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
        public event EventHandler<FileEventArgs> ProgressDownload
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
        public event EventHandler<FileEventArgs> EndDownload
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

        protected virtual void OnStartDownload()
        {
            var handler = (EventHandler<FileEventArgs>)events[DownloadStart];
            handler?.Invoke(this, new FileEventArgs());
        }
        protected virtual void OnProgressDownload(FileEventArgs file)
        {
            var handler = (EventHandler<FileEventArgs>)events[DownloadProgress];
            handler?.Invoke(this, file);
        }
        protected virtual void OnEndDownload()
        {
            var handler = (EventHandler<FileEventArgs>)events[DownloadEnd];
            handler?.Invoke(this, new FileEventArgs());
        }

        public void Start(FileEventArgs file)
        {
            Console.WriteLine($"Download Manager Start..!");
            OnStartDownload();
            OnProgressDownload(file);
            OnEndDownload();
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
        private void DownloadCreatorPub_ProgressDownload(object? sender, FileEventArgs e)
        {
            Console.WriteLine($"Donwload progress, File : {e.FileName}");
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
}
