using System.Threading.Tasks;
using ridestyler.core.feed.Engine;

namespace ridestyler.customer.feed.visionwheel
{
    class Program
    {
        static void Main(string[] args)
        {
            Flow<VisionWheelFeed> flowEngine = new Flow<VisionWheelFeed>();
            Task t = flowEngine.Run();
            t.Wait();
        }
    } 
}
