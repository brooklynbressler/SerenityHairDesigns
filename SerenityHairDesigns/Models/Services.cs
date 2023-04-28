using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models
{
	public class Services
	{
		public int intServiceID { get; set; }
		public string strServiceName { get; set; }
		public decimal decServiceCost { get; set; }
		public int intMinutes { get; set; }
		public int intGenderID { get; set; }

	}
}