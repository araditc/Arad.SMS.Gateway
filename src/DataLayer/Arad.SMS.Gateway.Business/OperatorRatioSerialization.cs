using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
	[Serializable]
	public class OperatorRatioSerialization
	{
		private Operators operators;
		private decimal ratio;

		public Operators Operator
		{
			get { return operators; }
			set { operators = value; }
		}

		public decimal Ratio
		{
			get { return ratio; }
			set { ratio = value; }
		}
	}
}
