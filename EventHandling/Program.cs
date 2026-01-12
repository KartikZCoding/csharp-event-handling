
namespace EventHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            var video = new Video() { Title = "Vlog-1" };
            var videoEncoder = new VideoEncoder();
            var mailService = new MailService();
            var messageService = new MessageService();

            videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
            videoEncoder.VideoEncoded += messageService.OnVideoEncoded;
            videoEncoder.Encode(video);
        }
    }

    public class MailService
    {
        public void OnVideoEncoded(object source, EventArgs e) // VideoEventArgs e)
        {
            Console.WriteLine($"MailService: Sending an email...");
            //Console.WriteLine($"MailService: Sending an email to {e.Video.Title}...");
        }
    }

    public class MessageService
    {
        public void OnVideoEncoded(object source, EventArgs e) // VideoEventArgs e)
        {
            Console.WriteLine($"MessageService: Sending a message...");
            //Console.WriteLine($"MessageService: Sending a message to {e.Video.Title}...");
        }
    }

}
