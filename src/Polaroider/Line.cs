
namespace Polaroider
{
	/// <summary>
	/// Represents a line in the snapshot
	/// </summary>
    public class Line
    {
        private readonly string _value;

		/// <summary>
		/// Creates a new line with the given value
		/// </summary>
		/// <param name="value"></param>
        public Line(string value)
        {
            _value = value;
        }

		/// <summary>
		/// Gets the linevalue
		/// </summary>
        public string Value => _value;

		/// <summary>
		/// Gets the string representative of the line
		/// </summary>
		/// <returns></returns>
        public override string ToString()
        {
            return _value;
        }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
        public override bool Equals(object obj)
        {
            var line = obj as Line;
            if (line == null)
            {
                return false;
            }

            return Value.Equals(line.Value);
        }

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
