namespace Lab10Lib
{
    public class ControlElement: IInit, IComparable, ICloneable
    {
        protected static readonly Random rnd = new();
        protected ID id;
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
            id = new ID();
            X = 0;
            Y = 0;
        }

        public ControlElement(uint x, uint y)
        {
            id = new ID();
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"Элемент типа {GetType().Name} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}.";
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
            X = (uint)rnd.Next(0, 1921);
            Y = (uint)rnd.Next(0, 1081);
        }

        public virtual int CompareTo(object? obj)
        {
            if (obj is not ControlElement element)
                return -1;
            return Id.CompareTo(element.Id);
        }
        
        public void Show()
        {
            WriteLine($"Элемент типа {GetType()} имеет ID {Id}, и находится по координатам x = {X}, y = {Y}.");
        }

        public virtual object Clone()
        {
            return new ControlElement(X, Y);
        }

        public override int GetHashCode()
        {
            return id.GetHashCode() + X.GetHashCode()*5 + Y.GetHashCode()*7;
        }

        ~ControlElement()
        { 
            ID.destroyedIds.Add(id);
        }
    }
}
