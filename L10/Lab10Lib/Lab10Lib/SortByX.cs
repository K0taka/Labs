using System.Diagnostics.CodeAnalysis;

namespace Lab10Lib
{
    /// <summary>
    /// Класс-компаратор для координаты X
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class SortByX: IComparer<ControlElement>
    {
        public int Compare(ControlElement? obj1, ControlElement? obj2)
        {
            if (obj1 == null)
            {
                if (obj2 == null)
                    return 0;
                return -1;
            }

            if (obj2 == null)
                return 1;

            if (obj1.X > obj2.X)
                return 1;
            if (obj1.X < obj2.X)
                return -1;
            return 0;
        }
    }
}
