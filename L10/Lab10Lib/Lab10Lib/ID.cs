namespace Lab10Lib
{
    public class ID: IComparable
    {
        internal static readonly List<uint> destroyedIds = [];
        private static uint nextId;
        private readonly uint id;

        public ID() => id = ChooseId();

        public static implicit operator uint(ID id) => id.id;

        public int CompareTo(object? obj)
        {
            if (obj is not ID id)
                return -1;
            return this.id.CompareTo(id);
        }

        public override string ToString() => id.ToString();

        public override bool Equals(object? obj)
        {
            if (obj is not ID id)
                return false;
            return this.id.Equals(id);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() * 3;
        }

        private static uint ChooseId()
        {
            if (destroyedIds != null && destroyedIds.Count != 0)
            {
                uint res = destroyedIds[0];
                destroyedIds.RemoveAt(0);
                return res;
            }
            return nextId++;
        }
    }
}
