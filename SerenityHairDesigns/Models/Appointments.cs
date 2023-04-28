using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerenityHairDesigns.Models {
	public class Appointments {

		public long intAppointmentID { get; set; }
		public long intCustomerID { get; set; }
		public long intEmployeeID { get; set; }
		public long intAppointmentTypeID { get; set; }
		public long intServiceID { get; set; }
		public DateTime	dtmAppointmentDate { get; set; }
		public DateTime dtmAppointmentTime { get; set; }
		public double monAppointmentTip { get; set; }

		public SelectList ServicesDropDownList { get; set; }

		public SelectList AppointmentTypesDropDownList { get; set; }

	}
}