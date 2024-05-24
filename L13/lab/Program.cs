using IOLib;
using Lab10Lib;
using static IOLib.IO;

namespace lab
{
    internal class Program
    {
        static readonly ObservedCollection<ControlElement, Button> firstCollection = new("Button tree");
        static readonly ObservedCollection<ControlElement, TextField> secondCollection = new("Fields tree");
        static readonly Journal j1 = new(WriteLine);
        static readonly Journal j2 = new(WriteLine);
        static readonly Journal jErrors = new(WriteLine);

        static void Main()
        {
            firstCollection.RegisterCountChangedHandler(j1.Add);
            firstCollection.RegisterReferenceChangedHandler(j1.Add);

            firstCollection.RegisterReferenceChangedHandler(j2.Add);
            secondCollection.RegisterReferenceChangedHandler(j2.Add);
        }
    }
}
