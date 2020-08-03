using System;
using System.Collections.Generic;
using System.Text;

namespace Polaroider
{
	public interface ILineParser
	{
		Line Parse(string line);
	}
}
