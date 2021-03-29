using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	[Serializable]
	public class ZaribOperatorSerialization
	{
		private Operators operators;
		private decimal zarib;

		public Operators Operator
		{
			get { return operators; }
			set { operators = value; }
		}

		private decimal Zarib
		{
			get { return zarib; }
			set { zarib = value; }
		}
	}
}
