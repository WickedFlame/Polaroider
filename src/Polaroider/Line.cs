
namespace Polaroider
{
    public class Line
    {
        private readonly string _value;

        public Line(string value)
        {
            _value = value;
        }

        public string Value => _value;

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            var line = obj as Line;
            if (line == null)
            {
                return false;
            }

            return Value.Equals(line.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
