using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models
{
	public class StylistAvailability
	{

		public int intStylistAvailability { get; set; }
		public long lngEmployeeID { get; set; }
	
		public DateTime dteStartTime { get; set; }
		public string strStartTimeString { get; set; }
		public DateTime dteEndTime { get; set; }

		public bool blnIsAvailable { get; set; }




	}
}