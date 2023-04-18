using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models
{
	public class EmployeeProducts
	{
		public int intEmployeeProductID { get; set; }
		public long intEmployeeID { get; set; }
		public int intProductID { get; set; }
		public int intProductInventory { get; set; }

		public virtual Products product { get; set; }


	}
}