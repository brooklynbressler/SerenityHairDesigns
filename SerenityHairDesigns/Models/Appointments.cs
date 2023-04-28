using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerenityHairDesigns.Models
{
    public class Appointments 
    {
        public int intAppointmentID = 0;
        public DateTime dtmAppointmentDate { get; set; }

        public int intEstTimeInMins { get; set; }

        public string strAppointmentName { get; set; }

        public decimal monAppointmentCost { get; set; }

        public decimal monAppointmentTip { get; set; }
    }
}