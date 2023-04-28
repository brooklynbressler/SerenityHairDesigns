using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models
{
	public class EmployeesMoneyInfo
	{
		public decimal decBoothRental { get; set; }
		public decimal decBuildingRental { get; set; }
		public decimal decBuildingUtilities { get; set; }
		public int intAppointmentPay { get; set; }
		public int intTipPay { get; set; }

	}
}