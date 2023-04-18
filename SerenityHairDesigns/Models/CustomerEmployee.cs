using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerenityHairDesigns.Models {
	public class CustomerEmployee {

		public Customer customer { get; set; }
		public Employee employee { get; set; }


		public bool IsAuthenticated {
			get {
				if (customer.intCustomerID > 0 || employee.intEmployeeID > 0) return true;
				return false;
			}
		}

	}
}