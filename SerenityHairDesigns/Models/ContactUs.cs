using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace SerenityHairDesigns.Models
{
	public class ContactUs
	{

		public string strName { get; set; }

		public string strEmail { get; set; }

		public string strMessage { get; set; }
		public int intRating { get; set; }

        public enum ActionTypes
        {
            NoType = 0,
            InsertSuccessful = 1,
            UpdateSuccessful = 2,
            DuplicateEmail = 3,
            DuplicateUserID = 4,
            Unknown = 5,
            RequiredFieldsMissing = 6,
            LoginFailed = 7
        }
    }
}