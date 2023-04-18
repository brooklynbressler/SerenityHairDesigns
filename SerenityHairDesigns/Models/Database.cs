using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Models
{
	public class Database
	{


		string strConnectionString = @"Data Source=BROOKIE-B-PC\SQLEXPRESS;Initial Catalog=SerenityHairDesigns;Integrated Security=True";
		public bool InsertReport(long UID, long IDToReport, int ProblemID) {
			try {

				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_REPORTS", cn);

				SetParameter(ref cm, "@uid", UID, SqlDbType.BigInt);
				SetParameter(ref cm, "@id_to_report", IDToReport, SqlDbType.BigInt);
				SetParameter(ref cm, "@problem_id", ProblemID, SqlDbType.TinyInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}
		//public int RateEvent(long UID, long ID, long Rating) {
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlCommand cm = new SqlCommand("UPDATE_EVENT_RATING", cn);
		//		int intReturnValue = -1;

		//		SetParameter(ref cm, "@rating_id", null, SqlDbType.BigInt, Direction: ParameterDirection.Output);
		//		SetParameter(ref cm, "@uid", UID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@event_id", ID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@rating", Rating, SqlDbType.TinyInt);

		//		SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

		//		cm.ExecuteReader();

		//		1 = new rate added

		//		2 = existing rate updated

		//		intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
		//		CloseDBConnection(ref cn);
		//		return intReturnValue;
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}

		//public List<Rating> GetEventRatings(long UID) {
		//	try {
		//		DataSet ds = new DataSet();
		//		SqlConnection cn = new SqlConnection();
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlDataAdapter da = new SqlDataAdapter("SELECT_USER_EVENT_RATINGS", cn);
		//		List<Rating> ratings = new List<Rating>();

		//		da.SelectCommand.CommandType = CommandType.StoredProcedure;

		//		SetParameter(ref da, "@uid", UID, SqlDbType.BigInt);

		//		try {
		//			da.Fill(ds);
		//		}
		//		catch (Exception ex2) {
		//			SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
		//		}
		//		finally { CloseDBConnection(ref cn); }

		//		if (ds.Tables[0].Rows.Count != 0) {
		//			foreach (DataRow dr in ds.Tables[0].Rows) {
		//				Rating r = new Rating();
		//				r.Type = Rating.Types.Event;
		//				r.ID = (long)dr["EventID"];
		//				r.Rate = (byte)dr["Rating"];
		//				ratings.Add(r);
		//			}
		//		}
		//		return ratings;
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}



		//public int ToggleEventLike(long UID, long ID) {
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlCommand cm = new SqlCommand("TOGGLE_EVENT_LIKE", cn);
		//		int intReturnValue = -1;

		//		SetParameter(ref cm, "@uid", UID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@event_id", ID, SqlDbType.BigInt);

		//		SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

		//		cm.ExecuteReader();

		//		1 = added

		//		0 = removed

		//		intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
		//		CloseDBConnection(ref cn);
		//		return intReturnValue;
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}





		public bool DeleteEvent(long ID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_EVENT", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@id", ID, SqlDbType.BigInt);
				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				if (intReturnValue == 1) return true;
				return false;

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public bool DeleteEventImage(long ID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_EVENT_IMAGE", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@id", ID, SqlDbType.BigInt);
				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				if (intReturnValue == 1) return true;
				return false;

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		//public List<Image> GetEventImages(long EventID = 0, long EventImageID = 0, bool PrimaryOnly = false) {
		//	try {
		//		DataSet ds = new DataSet();
		//		SqlConnection cn = new SqlConnection();
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlDataAdapter da = new SqlDataAdapter("SELECT_EVENT_IMAGES", cn);
		//		List<Image> imgs = new List<Image>();

		//		da.SelectCommand.CommandType = CommandType.StoredProcedure;

		//		if (EventID > 0) SetParameter(ref da, "@event_id", EventID, SqlDbType.BigInt);
		//		if (EventImageID > 0) SetParameter(ref da, "@event_image_id", EventImageID, SqlDbType.BigInt);
		//		if (PrimaryOnly) SetParameter(ref da, "@primary_only", "Y", SqlDbType.Char);

		//		try {
		//			da.Fill(ds);
		//		}
		//		catch (Exception ex2) {
		//			SysLog.UpdateLogFile(this.ToString(), MethodBase.GetCurrentMethod().Name.ToString(), ex2.Message);
		//		}
		//		finally { CloseDBConnection(ref cn); }

		//		if (ds.Tables[0].Rows.Count != 0) {
		//			foreach (DataRow dr in ds.Tables[0].Rows) {
		//				Image i = new Image();
		//				i.ImageID = (long)dr["EventImageID"];
		//				i.ImageData = (byte[])dr["Image"];
		//				i.FileName = (string)dr["FileName"];
		//				i.Size = (long)dr["ImageSize"];
		//				if (dr["PrimaryImage"].ToString() == "Y")
		//					i.Primary = true;
		//				else
		//					i.Primary = false;
		//				imgs.Add(i);
		//			}
		//		}
		//		return imgs;
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}



		//public long InsertEventImage(Event e) {
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlCommand cm = new SqlCommand("INSERT_EVENT_IMAGE", cn);

		//		SetParameter(ref cm, "@event_image_id", null, SqlDbType.BigInt, Direction: ParameterDirection.Output);
		//		SetParameter(ref cm, "@event_id", e.ID, SqlDbType.BigInt);
		//		if (e.EventImage.Primary)
		//			SetParameter(ref cm, "@PrimaryImage", "Y", SqlDbType.Char);
		//		else
		//			SetParameter(ref cm, "@PrimaryImage", "N", SqlDbType.Char);

		//		SetParameter(ref cm, "@image", e.EventImage.ImageData, SqlDbType.VarBinary);
		//		SetParameter(ref cm, "@file_name", e.EventImage.FileName, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@image_size", e.EventImage.Size, SqlDbType.BigInt);

		//		cm.ExecuteReader();
		//		CloseDBConnection(ref cn);
		//		return (long)cm.Parameters["@event_image_id"].Value;
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}

		//public Event.ActionTypes UpdateEvent(Event e) {
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlCommand cm = new SqlCommand("UPDATE_EVENT", cn);
		//		int intReturnValue = -1;

		//		SetParameter(ref cm, "@id", e.ID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@owner_uid", e.User.UID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@title", e.Title, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@desc", e.Description, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@start", e.Start, SqlDbType.DateTime);
		//		SetParameter(ref cm, "@end", e.End, SqlDbType.DateTime);
		//		SetParameter(ref cm, "@location_title", e.Location.Title, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@location_desc", e.Location.Description, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@address1", e.Location.Address.Address1, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@address2", e.Location.Address.Address2, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@city", e.Location.Address.City, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@state", e.Location.Address.State, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@zip", e.Location.Address.Zip, SqlDbType.NVarChar);

		//		if (e.IsActive)
		//			SetParameter(ref cm, "@is_active", "Y", SqlDbType.Char);
		//		else
		//			SetParameter(ref cm, "@is_active", "N", SqlDbType.Char);

		//		SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

		//		cm.ExecuteReader();

		//		intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
		//		CloseDBConnection(ref cn);

		//		switch (intReturnValue) {
		//			case 1: //new updated
		//				return Event.ActionTypes.UpdateSuccessful;
		//			default:
		//				return Event.ActionTypes.Unknown;
		//		}
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}

		//public long UpdateEventImage(Event e) {
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlCommand cm = new SqlCommand("UPDATE_EVENT_IMAGE", cn);

		//		SetParameter(ref cm, "@event_image_id", e.EventImage.ImageID, SqlDbType.BigInt);
		//		if (e.EventImage.Primary)
		//			SetParameter(ref cm, "@PrimaryImage", "Y", SqlDbType.Char);
		//		else
		//			SetParameter(ref cm, "@PrimaryImage", "N", SqlDbType.Char);

		//		SetParameter(ref cm, "@image", e.EventImage.ImageData, SqlDbType.VarBinary);
		//		SetParameter(ref cm, "@file_name", e.EventImage.FileName, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@image_size", e.EventImage.Size, SqlDbType.BigInt);

		//		cm.ExecuteReader();
		//		CloseDBConnection(ref cn);

		//		return 0; //success	
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}


		public List<AboutUs> GetReviews()
		{

			List<AboutUs> objReviews = new List<AboutUs>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT strName, strEmailAddress, strReview, intRating FROM TReviews";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							objReviews.Add(new AboutUs()
							{
								strName = reader.GetString(0)
								,
								strEmail = reader.GetString(1)
								,
								strReview = reader.GetString(2)
								,
								intRating = reader.GetInt32(3)

							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return objReviews;
		}




		public ContactUs.ActionTypes InsertReview(ContactUs model)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_REVIEW", cn);

				SetParameter(ref cm, "@strName", model.strName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strEmail", model.strEmail, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strMessage", model.strMessage, SqlDbType.NVarChar);
				SetParameter(ref cm, "@intRating", model.intRating, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return ContactUs.ActionTypes.InsertSuccessful;

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public long InsertCustomerImage(Customer c)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_CUSTOMER_IMAGE", cn);

				SetParameter(ref cm, "@intCustomerImageID", null, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@intCustomerID", c.intCustomerID, SqlDbType.BigInt);
				if (c.UserImage.Primary)
					SetParameter(ref cm, "@PrimaryImage", "Y", SqlDbType.Char);
				else
					SetParameter(ref cm, "@PrimaryImage", "N", SqlDbType.Char);

				SetParameter(ref cm, "@Image", c.UserImage.ImageData, SqlDbType.VarBinary);
				SetParameter(ref cm, "@FileName", c.UserImage.FileName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@ImageSize", c.UserImage.Size, SqlDbType.BigInt);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);
				return (long)cm.Parameters["@intCustomerImageID"].Value;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public long UpdateCustomerImage(Customer c)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_CUSTOMER_IMAGE", cn);

				SetParameter(ref cm, "@intCustomerImageID", c.UserImage.ImageID, SqlDbType.BigInt);
				if (c.UserImage.Primary)
					SetParameter(ref cm, "@PrimaryImage", "Y", SqlDbType.Char);
				else
					SetParameter(ref cm, "@PrimaryImage", "N", SqlDbType.Char);

				SetParameter(ref cm, "@Image", c.UserImage.ImageData, SqlDbType.VarBinary);
				SetParameter(ref cm, "@FileName", c.UserImage.FileName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@ImageSize", c.UserImage.Size, SqlDbType.BigInt);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);

				return 0; //success	
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public long InsertEmployeeImage(Employee e) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE_IMAGE", cn);

				SetParameter(ref cm, "@intEmployeeImageID", null, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt);
				if (e.UserImage.Primary)
					SetParameter(ref cm, "@PrimaryImage", "Y", SqlDbType.Char);
				else
					SetParameter(ref cm, "@PrimaryImage", "N", SqlDbType.Char);

				SetParameter(ref cm, "@Image", e.UserImage.ImageData, SqlDbType.VarBinary);
				SetParameter(ref cm, "@FileName", e.UserImage.FileName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@ImageSize", e.UserImage.Size, SqlDbType.BigInt);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);
				return (long)cm.Parameters["@intEmployeeImageID"].Value;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public long UpdateEmployeeImage(Employee e) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_EMPLOYEE_IMAGE", cn);

				SetParameter(ref cm, "@intEmployeeImageID", e.UserImage.ImageID, SqlDbType.BigInt);
				if (e.UserImage.Primary)
					SetParameter(ref cm, "@PrimaryImage", "Y", SqlDbType.Char);
				else
					SetParameter(ref cm, "@PrimaryImage", "N", SqlDbType.Char);

				SetParameter(ref cm, "@Image", e.UserImage.ImageData, SqlDbType.VarBinary);
				SetParameter(ref cm, "@FileName", e.UserImage.FileName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@ImageSize", e.UserImage.Size, SqlDbType.BigInt);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);

				return 0; //success	
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public List<Image> GetCustomerImages(long UID = 0, long UserImageID = 0, bool PrimaryOnly = false)
		{
			try
			{
				DataSet ds = new DataSet();
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("SELECT_CUSTOMER_IMAGES", cn);
				List<Image> imgs = new List<Image>();

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				if (UID > 0) SetParameter(ref da, "@intCustomerID", UID, SqlDbType.BigInt);
				if (UserImageID > 0) SetParameter(ref da, "@intCustomerImageID", UserImageID, SqlDbType.BigInt);
				if (PrimaryOnly) SetParameter(ref da, "@PrimaryImage", "Y", SqlDbType.Char);

				try
				{
					da.Fill(ds);
				}
				catch (Exception ex2)
				{
					throw new Exception(ex2.Message);
				}
				finally { CloseDBConnection(ref cn); }

				if (ds.Tables[0].Rows.Count != 0)
				{
					foreach (DataRow dr in ds.Tables[0].Rows)
					{
						Image i = new Image();
						i.ImageID = (long)dr["intCustomerImageID"];
						i.ImageData = (byte[])dr["Image"];
						i.FileName = (string)dr["FileName"];
						i.Size = (long)dr["ImageSize"];
						if (dr["PrimaryImage"].ToString() == "Y")
							i.Primary = true;
						else
							i.Primary = false;
						imgs.Add(i);
					}
				}
				return imgs;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public List<Image> GetEmployeeImages(long UID = 0, long UserImageID = 0, bool PrimaryOnly = false) {
			try {
				DataSet ds = new DataSet();
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("SELECT_EMPLOYEE_IMAGES", cn);
				List<Image> imgs = new List<Image>();

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				if (UID > 0) SetParameter(ref da, "@intEmployeeID", UID, SqlDbType.BigInt);
				if (UserImageID > 0) SetParameter(ref da, "@intEmployeeImageID", UserImageID, SqlDbType.BigInt);
				if (PrimaryOnly) SetParameter(ref da, "@PrimaryImage", "Y", SqlDbType.Char);

				try {
					da.Fill(ds);
				}
				catch (Exception ex2) {
					throw new Exception(ex2.Message);
				}
				finally { CloseDBConnection(ref cn); }

				if (ds.Tables[0].Rows.Count != 0) {
					foreach (DataRow dr in ds.Tables[0].Rows) {
						Image i = new Image();
						i.ImageID = (long)dr["intEmployeeImageID"];
						i.ImageData = (byte[])dr["Image"];
						i.FileName = (string)dr["FileName"];
						i.Size = (long)dr["ImageSize"];
						if (dr["PrimaryImage"].ToString() == "Y")
							i.Primary = true;
						else
							i.Primary = false;
						imgs.Add(i);
					}
				}
				return imgs;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public bool DeleteUserImage(long ID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_USER_IMAGE", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@id", ID, SqlDbType.BigInt);
				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				if (intReturnValue == 1) return true;
				return false;

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Customer.ActionTypes InsertCustomer(Customer c)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_CUSTOMER", cn);
				int intReturnValue = -1;
				int intGenderID;

				if (c.strGender == "Female")
				{
					intGenderID = 1;
					SetParameter(ref cm, "@intCustomerID", c.intCustomerID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
					SetParameter(ref cm, "@strFirstName", c.strFirstName, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strLastName", c.strLastName, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strPassword", c.strPassword, SqlDbType.NVarChar);
					SetParameter(ref cm, "@intGenderID", intGenderID, SqlDbType.Int);
					SetParameter(ref cm, "@strPhoneNumber", c.strPhoneNumber, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strEmailAddress", c.strEmailAddress, SqlDbType.NVarChar);

					SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

					cm.ExecuteReader();

					intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
					CloseDBConnection(ref cn);

				}
				if (c.strGender == "Male")
				{
					intGenderID = 2;
					SetParameter(ref cm, "@intCustomerID", c.intCustomerID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
					SetParameter(ref cm, "@strFirstName", c.strFirstName, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strLastName", c.strLastName, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strPassword", c.strPassword, SqlDbType.NVarChar);
					SetParameter(ref cm, "@intGenderID", intGenderID, SqlDbType.Int);
					SetParameter(ref cm, "@strPhoneNumber", c.strPhoneNumber, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strEmailAddress", c.strEmailAddress, SqlDbType.NVarChar);

					SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

					cm.ExecuteReader();

					intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
					CloseDBConnection(ref cn);

				}

				switch (intReturnValue)
				{
					case 1: // new user created
						c.intCustomerID = (long)cm.Parameters["@intCustomerID"].Value;
						return Customer.ActionTypes.InsertSuccessful;
					case -1:
						return Customer.ActionTypes.DuplicateEmail;
					case -2:
						return Customer.ActionTypes.DuplicateUserID;
					default:
						return Customer.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Employee.ActionTypes InsertEmployee(Employee e)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@strFirstName", e.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strLastName", e.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPassword", e.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPhoneNumber", e.strPhoneNumber, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strEmailAddress", e.strEmailAddress, SqlDbType.NVarChar);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new user created
						e.intEmployeeID = (long)cm.Parameters["@intEmployeeID"].Value;
						return Employee.ActionTypes.InsertSuccessful;
					case -1:
						return Employee.ActionTypes.DuplicateEmail;
					case -2:
						return Employee.ActionTypes.DuplicateUserID;
					default:
						return Employee.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Customer LoginCustomer(Customer c)
		{
			try
			{
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("LOGIN_CUSTOMER", cn);
				DataSet ds;
				Customer newUser = null;

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref da, "@strEmailAddress", c.strEmailAddress, SqlDbType.NVarChar);
				SetParameter(ref da, "@strPassword", c.strPassword, SqlDbType.NVarChar);

				try
				{
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
					{
						newUser = new Customer();
						DataRow dr = ds.Tables[0].Rows[0];
						newUser.intCustomerID = (long)dr["intCustomerID"];
						newUser.strFirstName = (string)dr["strFirstName"];
						newUser.strLastName = (string)dr["strLastName"];
						newUser.strPassword = c.strPassword;
						newUser.strPhoneNumber = (string)dr["strPhoneNumber"];
						newUser.strEmailAddress = (string)dr["strEmailAddress"];
					}
				}
				catch (Exception ex) { throw new Exception(ex.Message); }
				finally
				{
					CloseDBConnection(ref cn);
				}
				return newUser; //alls well in the world
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Employee LoginEmployee(Employee e)
		{
			try
			{
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("LOGIN_EMPLOYEE", cn);
				DataSet ds;

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref da, "@strEmailAddress", e.strEmailAddress, SqlDbType.NVarChar);
				SetParameter(ref da, "@strPassword", e.strPassword, SqlDbType.NVarChar);

				try
				{
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
					{
						DataRow dr = ds.Tables[0].Rows[0];
						e.intEmployeeID = (long)dr["intEmployeeID"];
						e.strFirstName = (string)dr["strFirstName"];
						e.strLastName = (string)dr["strLastName"];
						e.strPassword = (string)dr["strPassword"];
						e.strPhoneNumber = (string)dr["strPhoneNumber"];
						e.strEmailAddress = (string)dr["strEmailAddress"];
					}
				}
				catch (Exception ex) { throw new Exception(ex.Message); }
				finally
				{
					CloseDBConnection(ref cn);
				}
				return e; //alls well in the world
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Employee SelectEmployeeRole(Employee e) {
			try {
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("SELECT_EMPLOYEE_ROLE", cn);
				DataSet ds;

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref da, "@strEmailAddress", e.strEmailAddress, SqlDbType.NVarChar);
				SetParameter(ref da, "@strPassword", e.strPassword, SqlDbType.NVarChar);

				try {
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0) {
						DataRow dr = ds.Tables[0].Rows[0];
						e.strRole = (string)dr["strRoleName"];						
					}
				}
				catch (Exception ex) { throw new Exception(ex.Message); }
				finally {
					CloseDBConnection(ref cn);
				}
				return e; //alls well in the world
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Customer.ActionTypes UpdateCustomer(Customer c) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_CUSTOMER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@intCustomerID", c.intCustomerID, SqlDbType.BigInt);
				SetParameter(ref cm, "@strPassword", c.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strFirstName", c.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strLastName", c.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strEmailAddress", c.strEmailAddress, SqlDbType.NVarChar);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Customer.ActionTypes.UpdateSuccessful;
					default:
						return Customer.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public Employee.ActionTypes UpdateEmployee(Employee e)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_EMPLOYEE", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@strFirstName", e.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strLastName", e.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strpassword", e.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPhoneNumber", e.strPhoneNumber, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strEmailAddress", e.strEmailAddress, SqlDbType.NVarChar);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: //new updated
						return Employee.ActionTypes.UpdateSuccessful;
					default:
						return Employee.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private bool GetDBConnection(ref SqlConnection SQLConn)
		{
			try
			{
				if (SQLConn == null) SQLConn = new SqlConnection();
				if (SQLConn.State != ConnectionState.Open)
				{

					SQLConn.ConnectionString = strConnectionString;

					SQLConn.Open();



				}
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private bool CloseDBConnection(ref SqlConnection SQLConn)
		{
			try
			{
				if (SQLConn.State != ConnectionState.Closed)
				{
					SQLConn.Close();
					SQLConn.Dispose();
					SQLConn = null;
				}
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private int SetParameter(ref SqlCommand cm, string ParameterName, Object Value
			, SqlDbType ParameterType, int FieldSize = -1
			, ParameterDirection Direction = ParameterDirection.Input
			, Byte Precision = 0, Byte Scale = 0)
		{
			try
			{
				cm.CommandType = CommandType.StoredProcedure;
				if (FieldSize == -1)
					cm.Parameters.Add(ParameterName, ParameterType);
				else
					cm.Parameters.Add(ParameterName, ParameterType, FieldSize);

				if (Precision > 0) cm.Parameters[cm.Parameters.Count - 1].Precision = Precision;
				if (Scale > 0) cm.Parameters[cm.Parameters.Count - 1].Scale = Scale;

				cm.Parameters[cm.Parameters.Count - 1].Value = Value;
				cm.Parameters[cm.Parameters.Count - 1].Direction = Direction;

				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private int SetParameter(ref SqlDataAdapter cm, string ParameterName, Object Value
			, SqlDbType ParameterType, int FieldSize = -1
			, ParameterDirection Direction = ParameterDirection.Input
			, Byte Precision = 0, Byte Scale = 0)
		{
			try
			{
				cm.SelectCommand.CommandType = CommandType.StoredProcedure;
				if (FieldSize == -1)
					cm.SelectCommand.Parameters.Add(ParameterName, ParameterType);
				else
					cm.SelectCommand.Parameters.Add(ParameterName, ParameterType, FieldSize);

				if (Precision > 0) cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Precision = Precision;
				if (Scale > 0) cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Scale = Scale;

				cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Value = Value;
				cm.SelectCommand.Parameters[cm.SelectCommand.Parameters.Count - 1].Direction = Direction;

				return 0;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

	}
}
///////////////////////////////////////////////////////////////////////
//Spring 2021
///////////////////////////////////////////////////////////////////////
