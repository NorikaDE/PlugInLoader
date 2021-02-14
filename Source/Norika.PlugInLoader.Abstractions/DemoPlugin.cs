using System;

namespace Norika.PlugInLoader.Abstractions
{
    public class DemoPlugin : IDemoPlugIn
    {
        public void Bla()
        {
            Console.WriteLine("Say bla");
        }
    }
    
    public class DemoPlugin2 : IDemoPlugIn
    {
        public void Bla()
        {
            Console.WriteLine("And okay");
        }
    }
}
