using PreStart.Abstractions;

namespace PreStart.Models
{
    public class Site : TableData
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
