
using System.Collections.Generic;

namespace GeneralTools.TreeView
{
	public class TreeNode
	{
		public string id { get; set; }

		public string text { get; set; }

		public string state { get; set; }

		public object attributes { get; set; }

		public List<TreeNode> children { get; set; }
	}
}
