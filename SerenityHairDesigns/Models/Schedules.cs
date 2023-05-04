using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models
{
	public class Schedules
	{

		public int intScheduleID { get; set; }
		public long lngEmployeeID { get; set; }


		public DateTime dteStartTime { get; set; }
		public DateTime dteEndTime { get; set; }

	}
}