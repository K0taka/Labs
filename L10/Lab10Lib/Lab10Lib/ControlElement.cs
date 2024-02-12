namespace Lab10Lib
{
    public class ControlElement
    {
        protected static Random rnd = new();
        protected static uint nextId;
        protected static List<uint> destroyedIds = [];

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

        protected static uint ChooseId()
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
            return $"Элемент типа ControlElement имеет ID {Id}, и находится по координатам x = {X}, y = {Y}";
        }

        public override bool Equals(object? obj)
        {
            if (obj is not ControlElement el)
                return false;
            return el.X == X && el.Y == Y;
        }

        public virtual void Init()
        {
            static uint GetAnswer()
            {
                bool isCorrect = false;
                uint ans = 0;
                do
                {
                    try
                    {
                        ans = uint.Parse(Console.ReadLine()!);
                        isCorrect = true;
                    }
                    catch (FormatException)
                    {
                        Console.Write("Необходимо ввести целое положительное число! Повторите ввод >>> ");
                    }
                } while (!isCorrect);
                return ans;
            }

            Console.Write("Введите координату X >> ");
            try
            {
                X = GetAnswer();
            }
            catch (InvalidOperationException)
            {
                Console.Write("Вы не можете задать это поле таким значением! Повторите ввод >>> ");
            }

            Console.Write("Введите координату Y >> ");
            try
            {
                Y = GetAnswer();
            }
            catch (InvalidOperationException)
            {
                Console.Write("Вы не можете задать это поле таким значением! Повторите ввод >>> ");
            }
        }

        public virtual void RandomInit()
        {

        }
    }
}
