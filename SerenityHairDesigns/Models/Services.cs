using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerenityHairDesigns.Models {
	public class Services {

		public long intServiceID { get; set; }
		public string strServiceName { get; set; }
		public decimal monServiceCost { get; set; }
		public int intEstTimeSpent { get; set; }

	}
}