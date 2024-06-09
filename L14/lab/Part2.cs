using Lab10Lib;
using AVLTree;

namespace lab
{
    public static class Part2
    {
        #region request1: Count elements of type

        public static int LINQCountElementsOfType(AVL<ControlElement, ControlElement> tree, Type type) =>
            (from element in tree
            where element.GetType() == type
            select element).Count();

        public static int EXTCountElementsOfType(AVL<ControlElement, ControlElement> tree, Type type) =>
            tree.Where(element => element.GetType() == type)
                .Count();

        #endregion request1: Count elements of type

        #region request2: Get element with min X

        public static ControlElement? LINQGetElementWithMinX(AVL<ControlElement, ControlElement> tree) =>
            (from element in tree
             select element.Value).Min(new SortByX());

        public static ControlElement? EXTGetElementWithMinX(AVL<ControlElement, ControlElement> tree) =>
            tree.Select(element => element.Value)
                .Min(new SortByX());

        #endregion request2: Get element with min X

        #region request3: Get element with max Y

        public static ControlElement? LINQGetElementWithMaxY(AVL<ControlElement, ControlElement> tree) =>
            (from element in tree
             select element.Value).Max(new SortByY());

        public static ControlElement? EXTGetElementWithMaxY(AVL<ControlElement, ControlElement> tree) =>
            tree.Select(element => element.Value)
                .Max(new SortByY());

        #endregion request3: Get element with max Y

        #region request4: Average Distance

        public static double LINQAverageDistance(AVL<ControlElement, ControlElement> tree) =>
            (from element in tree
            let distance = Math.Sqrt(Math.Pow(element.Value.X, 2) + Math.Pow(element.Value.Y, 2))
            select distance).Average();

        public static double EXTAverageDistance(AVL<ControlElement, ControlElement> tree) =>
            tree.Average(element => Math.Sqrt(Math.Pow(element.Value.X, 2) + Math.Pow(element.Value.Y, 2)));

        #endregion request4: Average Distance

        #region request5: Group count of types

        public static IEnumerable<IGrouping<Type, int>> LINQCountGroupsByTypes(AVL<ControlElement, ControlElement> tree) =>
            from gr in from element in tree
                       group element.Value by element.Value.GetType()
            group gr.Count() by gr.Key;

        #endregion request6: Group count of types


    }
}
