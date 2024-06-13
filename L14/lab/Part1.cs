using Lab10Lib;

namespace lab
{

    public static class Part1
    {
        #region request1: Elements far than

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

        #endregion request1: Elements far than

        #region request2: Get min distanse element in every window

        public static IEnumerable<ControlElement> LINQGetMinDistanseInEveryWindow(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            from window in app
            select window.Min(element => element.Value);

        public static IEnumerable<ControlElement> EXTGetMinDistanseInEveryWindow(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            app.Select(window => window.Min(element => element.Value));

        #endregion request2: Get min distanse element in every window

        #region request3: Get max distanse element in every window
        public static IEnumerable<ControlElement> LINQGetMaxDistanseInEveryWindow(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            from window in app
            select window.Max(element => element.Value);

        public static IEnumerable<ControlElement> EXTGetMaxDistanseInEveryWindow(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            app.Select(window => window.Max(element => element.Value));

        #endregion request3: Get max distanse element in every window

        #region request4: Average dist between elements

        public static double LINQAverageDistanceOfElements(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            (from window in app
             from element in window
             let distance = Math.Sqrt(Math.Pow(element.Value.X, 2) + Math.Pow(element.Value.Y, 2))
             select distance).Average(distance => distance);

        public static double EXTAverageDistanceOfElements(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            app.SelectMany(element => element)
               .Select(element => Math.Sqrt(Math.Pow(element.Value.X, 2) + Math.Pow(element.Value.Y, 2)))
               .Average(distance => distance);

        #endregion request4: Average dist between elements

        #region request5: Unic elements

        public static IEnumerable<ControlElement> LINQUnicElements(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            from enumerable in from window1 in app
                               let values = from element1 in window1 select element1.Value
                               select values.Except(from window2 in app
                                                    from element2 in window2
                                                    where window1 != window2
                                                    select element2.Value)
            from element in enumerable
            orderby element
            select element;

        public static IEnumerable<ControlElement> EXTUnicElements(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            app.SelectMany(window1 => window1.Select(element => element.Value)
                                             .Except(app.Where(window2 => window1 != window2)
                                                        .SelectMany(element => element.Values)))
               .OrderBy(element => element);

        #endregion request5: Unic elements

        #region request6: Commnon elements

        public static IEnumerable<ControlElement> LINQCommonElements(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            from enumerable in from window1 in app
                               let values = from element1 in window1 select element1.Value
                               select values.Intersect(from window2 in app
                                                       from element2 in window2
                                                       where window1 != window2
                                                       select element2.Value)
            from element in enumerable
            orderby element
            select element;

        public static IEnumerable<ControlElement> EXTCommonElements(Stack<Dictionary<ControlElement, ControlElement>> app) =>
            app.SelectMany(window1 => window1.Select(element => element.Value)
                                             .Intersect(app.Where(window2 => window1 != window2)
                                                           .SelectMany(element => element.Values)))
               .OrderBy(element => element);

        #endregion request6: Commnon elements

        #region request7: Element with it's function

        public static IEnumerable<dynamic> LINQElementAndItsFuntion(Stack<Dictionary<ControlElement, ControlElement>> app, List<Function> funcs) =>
            from window in app
            from element in window
            join function in funcs on element.Value.Id equals function.ConnectedID
            let item = new { Item = element.Value, Function = function.Description }
            select item;

        public static IEnumerable<dynamic> EXTElementAndItsFuntion(Stack<Dictionary<ControlElement, ControlElement>> app, List<Function> funcs) =>
            app.SelectMany(element => element)
               .Join(funcs,
                     element => element.Value.Id,
                     funtion => funtion.ConnectedID,
                     (element, function) => new { Item = element.Value, Function = function.Description });

        #endregion request7: Element with it's function

    }
}
