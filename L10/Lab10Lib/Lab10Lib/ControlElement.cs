namespace Lab10Lib
{
    public class ControlElement
    {
        protected static readonly Random rnd = new();
        private static uint nextId;
        protected static readonly List<uint> destroyedIds = [];

        protected uint id;
        protected uint x;
        protected uint y;

        public uint Id
        {
            get { return id; }
        }

        public uint X
        {
            get { return x; }
            set 
            {
                if (value >= 0)
                    x = value;
                else
                    throw new InvalidOperationException("Incorrect x cordinate");
            }
        }

        public uint Y
        {
            get { return y; }
            set 
            {
                if (value >= 0)
                    y = value;
                else
                    throw new InvalidOperationException("Incorrect y cordinate");
            }
        }

        public ControlElement()
        {
            id = ChooseId();
            X = 0;
            Y = 0;
        }

        public ControlElement(uint x, uint y)
        {
            id = ChooseId();
            X = x;
            Y = y;
        }

        public ControlElement(ControlElement el)
        {
            id = ChooseId();
            X = el.X;
            Y = el.Y;
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

        public override string ToString()
        {
            return $"Элемент типа {GetType()} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}.";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ControlElement el)
                return false;
            return el.X == X && el.Y == Y;
        }

        public virtual void Init()
        {
            X = (uint)GetIntegerAnswer("Введите координату X >>> ", 0, 1921);
            Y = (uint)GetIntegerAnswer("Введите координату Y >>> ", 0, 1081);
        }

        public virtual void RandomInit()
        {
            id = ChooseId();
            X = (uint)rnd.Next(0, 1921);
            Y = (uint)rnd.Next(0, 1081);
        }
        
        ~ControlElement()
        { 
            destroyedIds.Add(id);
        }
    }
}
