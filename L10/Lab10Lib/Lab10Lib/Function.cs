namespace Lab10Lib
{
    public class Function
    {
        uint connectedID;
        readonly string description;

        public uint ConnectedID
        {
            get { return connectedID; }
            set { connectedID = value; }
        }

        public string Description => description;

        public Function(uint ID, string description)
        {
            connectedID = ID;
            this.description = description; 
        }
    }
}
