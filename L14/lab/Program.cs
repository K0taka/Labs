using Lab10Lib;

namespace lab
{
    public class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }

        #region part1

        public static IEnumerable<IGrouping<double, ControlElement>> LINQGetElementsFarThen(Stack<Dictionary<ControlElement, ControlElement>> app, double dist) =>
            from window in app
            from element in window
            let distanse = Math.Sqrt(Math.Pow(element.Key.X, 2) + Math.Pow(element.Key.Y, 2))
            where distanse > dist
            orderby element.Value
            group element.Value by distanse;

        public static IEnumerable<IGrouping<double, ControlElement>> EXTGetElementsFarThen(Stack<Dictionary<ControlElement, ControlElement>> app, double dist) =>
            app.SelectMany(window => window.Where(element => Math.Sqrt(Math.Pow(element.Key.X, 2) + Math.Pow(element.Key.Y, 2)) > dist))
            .OrderBy(element => element.Value)
            .GroupBy(element => Math.Sqrt(Math.Pow(element.Key.X, 2) + Math.Pow(element.Key.Y, 2)), element => element.Value);


        #endregion part1

        #region part2


        #endregion part2
    }
}
