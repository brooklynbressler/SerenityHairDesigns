using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models
{
	public class Products
	{
		public int intProductID { get; set; }
		public string strProductName { get; set; }
		public int intTotalInventory { get; set; }
		public bool blnNeedsRestocking { get; set; }



	}
}