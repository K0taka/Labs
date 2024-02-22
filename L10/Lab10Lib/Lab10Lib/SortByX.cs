namespace Lab10Lib
{
    public class SortByX: IComparer<object>
    {
        public int Compare(object? obj1, object? obj2)
        {
            ControlElement? cntEl1 = obj1 as ControlElement;
            ControlElement? cntEl2 = obj2 as ControlElement;

            if (cntEl1 == null)
            {
                if (cntEl2 == null)
                    return 0;
                return -1;
            }

            if (cntEl2 == null)
                return 1;

            if (cntEl1.X > cntEl2.X)
                return 1;
            if (cntEl1.X < cntEl2.X)
                return -1;
            return 0;
        }
    }
}
