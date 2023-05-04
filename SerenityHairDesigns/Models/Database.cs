using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using SerenityHairDesigns.Models;
using System.Web.Mvc;
using System.Collections;
using System.Web.UI.WebControls;
using System.Web.Mvc;

namespace SerenityHairDesigns.Models
{
	public class Database
	{



		//public SelectList ListAppointmentTypes() {
		//	List<AppointmentTypes> objAppointmentTypes = new List<AppointmentTypes>();
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

		//		string query = "SELECT * FROM TAppointmentTypes";

		//		SqlCommand cmd = new SqlCommand(query, cn);

		//		using (IDataReader reader = cmd.ExecuteReader()) {
		//			while (reader.Read()) {
		//				objAppointmentTypes.Add(new AppointmentTypes() {
		//					intAppointmentTypeID = reader.GetInt64(0)
		//					,
		//					strAppointmentName = reader.GetString(1)
		//					,
		//					intEstTimeInMins = reader.GetInt32(2)

		//				});

		//			}
		//			reader.Close();

		//		}

		//		CloseDBConnection(ref cn);

		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//	{
		//	}

		//	var model = new Appointments();
		//	model.AppointmentTypesDropDownList = new SelectList(objAppointmentTypes, "intAppointmentTypeID", "strAppointmentName");

		//	return model.AppointmentTypesDropDownList;
		//}

		//public SelectList ListServices() {
		//	List<Services> objServices = new List<Services>();
		//	try {
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

		//		string query = "SELECT * FROM TServices";

		//		SqlCommand cmd = new SqlCommand(query, cn);

		//		using (IDataReader reader = cmd.ExecuteReader()) {
		//			while (reader.Read()) {
		//				objServices.Add(new Services() {
		//					intServiceID = reader.GetInt64(0)
		//					,
		//					strServiceName = reader.GetString(1)
		//					,
		//					monServiceCost = reader.GetDecimal(2)
		//					,
		//					intEstTimeSpent = reader.GetInt32(3)

		//				});

		//			}
		//			reader.Close();

		//		}

		//		CloseDBConnection(ref cn);

		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//	{
		//	}

		//	var model = new Appointments();
		//	model.ServicesDropDownList = new SelectList(objServices, "intServiceID", "strServiceName");

		//	return model.ServicesDropDownList;
		//}

		string strConnectionString = @"Data Source=DESKTOP-GOI89LE;Initial Catalog=SerenityHairDesigns;Integrated Security=True";
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

		public List<EmployeeProducts> GetEmployeesProducts(long lngEmployeeID)
		{

			List<EmployeeProducts> objEmployeeProducts = new List<EmployeeProducts>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TEmployeeProducts INNER JOIN TProducts ON TProducts.intproductID = TEmployeeProducts.intProductID WHERE intEmployeeID = " + lngEmployeeID;
				 
				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
							objEmployeeProducts.Add(new EmployeeProducts()
							{
								intEmployeeProductID = reader.GetInt32(0)
								,
								intEmployeeID = reader.GetInt64(1)
								,
								intProductID = reader.GetInt32(2)
								,
								intProductInventory = reader.GetInt32(3)
								,
								product = new Products()
								{ 
									intProductID = reader.GetInt32(4)
									,
									strProductName = reader.GetString(5)
									,
									intTotalInventory = reader.GetInt32(6)
									, 
									blnNeedsRestocking = reader.GetBoolean(7)

								}

							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return objEmployeeProducts;
		}


		public List<Products> GetAllProducts()
		{

			List<Products> Products = new List<Products>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TProducts";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Products.Add(new Products()
						{
							intProductID = reader.GetInt32(0)
								,
							strProductName = reader.GetString(1)
								,
							intTotalInventory = reader.GetInt32(2)
								,
							blnNeedsRestocking = reader.GetBoolean(3)

						}) ;

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return Products;
		}


		public bool UpdateEmployeeItemInventory(int intEmployeeProductID, int intItemQuantityChange)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_EMPLOYEE_INVENTORY", cn);

				SetParameter(ref cm, "@intEmployeeProductID", intEmployeeProductID, SqlDbType.Int);

				SetParameter(ref cm, "@intItemQuantityChange", intItemQuantityChange, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		public bool AddEmployeeProduct(long lngEmployeeID, int intProductID, int intItemQuantityChange)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("ADD_PRODUCT_TO_EMPLOYEE", cn);

				SetParameter(ref cm, "@lngEmployeeID", lngEmployeeID, SqlDbType.BigInt);

				SetParameter(ref cm, "@intProductID", intProductID, SqlDbType.Int);

				SetParameter(ref cm, "@intItemQuantityChange", intItemQuantityChange, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		public bool AddNewProduct(long lngEmployeeID, string strNewProductName, int intItemQuantityChange)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("ADD_NEW_PRODUCT", cn);

				SetParameter(ref cm, "@lngEmployeeID", lngEmployeeID, SqlDbType.BigInt);

				SetParameter(ref cm, "@strNewProductName", strNewProductName, SqlDbType.VarChar);

				SetParameter(ref cm, "@intItemQuantityChange", intItemQuantityChange, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}


		public bool EnterEmployeeCost(long intEmployeeID, DateTime dteStartDate, DateTime dteEndDate, int intBoothRental)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE_COST", cn);

				SetParameter(ref cm, "@intEmployeeID", intEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@dtmStartDate", dteStartDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@dtmEndDate", dteEndDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@intBoothRental", intBoothRental, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex) 
			{ 
				throw new Exception(ex.Message);
			}

		}

		//public Employee InsertAvailability(Employee e)
		//{
		//	try
		//	{
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

		//		SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE_AVAILABILITY", cn);

		//		SetParameter(ref cm, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@dtmStartTime", e.dtmStartTime, SqlDbType.DateTime);
		//		SetParameter(ref cm, "@dtmEndTime", e.dtmEndTime, SqlDbType.DateTime);

		//		SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

		//		cm.ExecuteReader();

		//		CloseDBConnection(ref cn);

		//		return e;

		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}

		public Employee InsertAvailability(DateTime dtmStartTime, DateTime dtmEndTime, long lngEmployeeID)
		{
			Employee e = new Employee();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				SqlCommand cm = new SqlCommand("INSERT_STYLIST_AVAILABILITY", cn);

				SetParameter(ref cm, "@intEmployeeID", lngEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@dtmStartTime", dtmStartTime, SqlDbType.DateTime);
				SetParameter(ref cm, "@dtmEndTime", dtmEndTime, SqlDbType.DateTime);
				SetParameter(ref cm, "@blnIsAvailable", 1, SqlDbType.Bit);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return e;

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public void InsertAvailabilitySchedule(DateTime dtmStartTime, DateTime dtmEndTime, long lngEmployeeID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				SqlCommand cm = new SqlCommand("INSERT_SCHEDULE_AVAILABILITY", cn);

				SetParameter(ref cm, "@intEmployeeID", lngEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@dtmStartTime", dtmStartTime, SqlDbType.DateTime);
				SetParameter(ref cm, "@dtmEndTime", dtmEndTime, SqlDbType.DateTime);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public bool EnterAdminCosts(long intEmployeeID, DateTime dteStartDate, DateTime dteEndDate, int intBoothRental, int intBuildingRental, int intBuildingUtilities)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_ADMIN_COST", cn);

				SetParameter(ref cm, "@intEmployeeID", intEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@dtmStartDate", dteStartDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@dtmEndDate", dteEndDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@intBoothRental", intBoothRental, SqlDbType.Int);
				SetParameter(ref cm, "@intBuildingRental", intBuildingRental, SqlDbType.Int);
				SetParameter(ref cm, "@intBuildingUtilities", intBuildingUtilities, SqlDbType.Int);


				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}

		public bool EnterEmployeeEarning(long intEmployeeID, DateTime dteStartDate, DateTime dteEndDate, int intAppointmentPay, int intTipPay)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE_EARNING", cn);

				SetParameter(ref cm, "@intEmployeeID", intEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@dtmStartDate", dteStartDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@dtmEndDate", dteEndDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@intAppointmentPay", intAppointmentPay, SqlDbType.Int);
				SetParameter(ref cm, "@intTipPay", intTipPay, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}


		public bool DeleteSchedule(int Schedule)
		{

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("DELETE_SCHEDULE", cn);

					SetParameter(ref cm, "@intScheduleID", Schedule, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return true;
			}
			catch (Exception ex)
			{
				throw new Exception(ex.Message);
			}

		}


		public List<Schedules> GetEmployeesSchedule(long lngEmployeeID, DateTime CurrentDate)
		{

			List<Schedules> Schedules = new List<Schedules>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TSchedules WHERE lngEmployeeID = " + lngEmployeeID + " AND dtmEndTime > '" + CurrentDate + "'";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
							if (!reader.IsDBNull(0))
								Schedules.Add(new Schedules()
								{
									intScheduleID = reader.GetInt32(0)
									,
									dteEndTime = reader.GetDateTime(1)
									,
									dteStartTime = reader.GetDateTime(2)
									,
									lngEmployeeID = reader.GetInt64(3)

								});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return Schedules;
		}



		public EmployeesMoneyInfo EmployeeInfo(long lngEmployeeID, DateTime dtmStartTime, DateTime dtmEndTime)
		{

			EmployeesMoneyInfo EmployeeInfo = new EmployeesMoneyInfo();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT ISNULL(TEmployeeCosts.decBoothRentel, 0), ISNULL(TEmployeeCosts.decBuildingRental, 0), ISNULL(TEmployeeCosts.decBuildingUtilities, 0), ISNULL(intAppointmentPay, 0), ISNULL(intTipPay, 0)" +
								"FROM TEarnings " +
								"JOIN TEmployees ON TEmployees.intEmployeeID = TEarnings.intEmployeeID " +
								"JOIN TEmployeeCosts ON TEmployees.intEmployeeID = TEmployeeCosts.intEmployeeID " +
								"WHERE TEarnings.intEmployeeID = " + lngEmployeeID +
								" AND TEmployeeCosts.intEmployeeID = " + lngEmployeeID +
								" AND TEarnings.dteStartTime >= '" + dtmStartTime +
								"' AND TEmployeeCosts.dtmStartDate >= '" + dtmStartTime +
								"' AND TEmployeeCosts.dtmEndDate <= '" + dtmEndTime +
								"' AND TEarnings.dteEndTime <= '" + dtmEndTime + "'";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							
							{
							EmployeeInfo.decBoothRental = reader.GetDecimal(0);

							EmployeeInfo.decBuildingRental = reader.GetDecimal(1);

							EmployeeInfo.decBuildingUtilities = reader.GetDecimal(2);

							EmployeeInfo.intAppointmentPay = reader.GetInt32(3);

							EmployeeInfo.intTipPay = reader.GetInt32(4);
								
							};

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return EmployeeInfo;
		}



		//public List<StylistAvailability> GetStylistAvailability(long lngEmployeeID, string dtmStartTime, string dtmEndTime)
		//{

		//	List<StylistAvailability> stylistAvailabilities = new List<StylistAvailability>();
		//	try
		//	{
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

		//		string query = "SELECT * FROM TStylistAvailability WHERE intEmployeeID = " + lngEmployeeID + " AND dtmStartTime > '" + dtmStartTime + "' AND dtmStartTime < '" + dtmEndTime + "' AND blnIsAvailable = 1";

		//		SqlCommand cmd = new SqlCommand(query, cn);

		//		using (IDataReader reader = cmd.ExecuteReader())
		//		{
		//			while (reader.Read())
		//			{
		//				if (!reader.IsDBNull(0))
		//					stylistAvailabilities.Add(new StylistAvailability()
		//					{
		//						intStylistAvailability = reader.GetInt32(0)
		//						,
		//						lngEmployeeID = reader.GetInt64(1)
		//						,
		//						dteStartTime = reader.GetDateTime(2)
		//						,
		//						dteEndTime = reader.GetDateTime(3)
		//						,
		//						blnIsAvailable = reader.GetBoolean(4)

		//					});

		//			}
		//			reader.Close();

		//		}

		//		CloseDBConnection(ref cn);

		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//	{
		//	}
		//	return stylistAvailabilities;
		//}

		public EmployeesMoneyInfo AdminInfo(long lngEmployeeID, DateTime dtmStartTime, DateTime dtmEndTime)
		{

			EmployeesMoneyInfo EmployeeInfo = new EmployeesMoneyInfo();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT TEmployeeCosts.decBoothRentel, intAppointmentPay, intTipPay " +
								"FROM TEarnings " +
								"JOIN TEmployees ON TEmployees.intEmployeeID = TEarnings.intEmployeeID " +
								"JOIN TEmployeeCosts ON TEmployees.intEmployeeID = TEmployeeCosts.intEmployeeID " +
								"WHERE TEarnings.intEmployeeID = " + lngEmployeeID +
								" AND TEmployeeCosts.intEmployeeID = " + lngEmployeeID +
								" AND TEarnings.dteStartTime >= '" + dtmStartTime +
								"' AND TEmployeeCosts.dtmStartDate >= '" + dtmStartTime +
								"' AND TEmployeeCosts.dtmEndDate <= '" + dtmEndTime +
								"' AND TEarnings.dteEndTime <= '" + dtmEndTime + "'";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))

						{
							EmployeeInfo.decBoothRental = reader.GetDecimal(0);

							EmployeeInfo.intAppointmentPay = reader.GetInt32(1);

							EmployeeInfo.intTipPay = reader.GetInt32(2);

						};

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return EmployeeInfo;
		}


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

        public List<Appointments> GetAppointments(long CustomerID)
        {

            List<Appointments> objAppointments = new List<Appointments>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("SELECT_APPOINTMENT_INFORMATION", cn);

				SetParameter(ref cm, "@intCustomerID", CustomerID, SqlDbType.BigInt);



				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);



				using (IDataReader reader = cm.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							objAppointments.Add(new Appointments()
							{
								dtmAppointmentDate = reader.GetDateTime(0)
								,
								intEstTimeInMins = reader.GetInt32(1)
								,
								strAppointmentName = reader.GetString(2)
								,
                                monAppointmentCost = reader.GetDecimal(3)
								

							});

					}
					reader.Close();

					CloseDBConnection(ref cn);

				}
			}
			catch
			{

			}
			return objAppointments;
		}


		public List<Appointments> GetEmployeeAppointments(long intEmployeeID) {

			List<Appointments> objAppointments = new List<Appointments>();
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("SELECT_EMPLOYEE_APPOINTMENT_INFORMATION", cn);

				SetParameter(ref cm, "@intEmployeeID", intEmployeeID, SqlDbType.BigInt);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				using (IDataReader reader = cm.ExecuteReader()) {
					while (reader.Read()) {
						if (!reader.IsDBNull(0))
							objAppointments.Add(new Appointments() {
								intAppointmentID = reader.GetInt32(0)
								,
								dtmAppointmentDate = reader.GetDateTime(1)
								,
								intEstTimeInMins = reader.GetInt32(2)
								,
								strAppointmentName = reader.GetString(3)
								,
								monAppointmentCost = reader.GetDecimal(4)
								,
								monAppointmentTip = reader.GetDecimal(5)
								,
								strCustomerFirstName = reader.GetString(6)
								,
								strCustomerLastName = reader.GetString(7)
								,
								strCustomerPhone = reader.GetString(8)
								,
								strCustomerEmail = reader.GetString(9)

							});

					}
					reader.Close();

					CloseDBConnection(ref cn);

				}
			}
			catch {

			}
			return objAppointments;
		}



		public List<Services> GetAllServices()
		{

			List<Services> Services = new List<Services>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TServices";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							Services.Add(new Services()
							{
								intServiceID = reader.GetInt32(0)
								,
								strServiceName = reader.GetString(1)
								,
								decServiceCost = reader.GetDecimal(2)
								,
								intMinutes = reader.GetInt32(3)
								,
								intGenderID = reader.GetInt32(4)

							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{

			}
			return Services;
		}


		public Services GetSelectedServices(int intServiceID)
		{

			Services Service = new Services();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TServices WHERE intServiceID = " + intServiceID;

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							{
							Service.intServiceID = reader.GetInt32(0);

							Service.strServiceName = reader.GetString(1);

							Service.decServiceCost = reader.GetDecimal(2);

							Service.intMinutes = reader.GetInt32(3);

							Service.intGenderID = reader.GetInt32(4);

							};

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{

			}
			return Service;
		}

		public Customer GetLastCustomer()
		{
			Customer Customer = new Customer();


			try
			{

				Genders Gender = new Genders();

				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT TOP 1 * FROM TCustomers ORDER BY intCustomerID DESC";


				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
						{
							Customer.intCustomerID = reader.GetInt64(0);

						};

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{

			}
			return Customer;
		}


		//public void InsertAppointment(Appointments model, Customer customer, long lngEmployeeID, int intServiceID, long lngBeforePicID, long lngAfterPicID)
		//{
		//	try
		//	{
		//		SqlConnection cn = null;
		//		if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//		SqlCommand cm = new SqlCommand("INSERT_APPOINTMENT", cn);

		//		SetParameter(ref cm, "@dtmAppointmentDate", model.dtmAppointmentDate, SqlDbType.DateTime);
		//		SetParameter(ref cm, "@intCustomerID", customer.intCustomerID, SqlDbType.Int);
		//		SetParameter(ref cm, "@lngEmployeeID", lngEmployeeID, SqlDbType.Int);
		//		SetParameter(ref cm, "@intServiceID", intServiceID, SqlDbType.Decimal);
		//		SetParameter(ref cm, "@lngBeforePicID", lngEmployeeID, SqlDbType.BigInt);
		//		SetParameter(ref cm, "@lngAfterPicID", lngEmployeeID, SqlDbType.BigInt);



		//		SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

		//		cm.ExecuteReader();

		//		CloseDBConnection(ref cn);


		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}

		public void InsertAppointmentNoPic(Appointments model, Customer customer, long lngEmployeeID, int intServiceID, int intTimeID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("CUSTOMER_INSERT_APPOINTMENT", cn);

				SetParameter(ref cm, "@intTimeID", intTimeID, SqlDbType.Int);
				SetParameter(ref cm, "@intAppointmentTypeID", model.intAppointmentType, SqlDbType.Int);
				SetParameter(ref cm, "@intCustomerID", customer.intCustomerID, SqlDbType.Int);
				SetParameter(ref cm, "@lngEmployeeID", lngEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@intServiceID", intServiceID, SqlDbType.Int);



				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}



		public void InsertAppointmentNoPic(Appointments model, Customer customer, long lngEmployeeID, int intServiceID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_APPOINTMENT", cn);

				string dteDate = model.dtmAppointmentDate.ToString("yyyy-MM-dd HH:mm:ss.fff");

				SetParameter(ref cm, "@dtmAppointmentDate", dteDate, SqlDbType.DateTime);
				SetParameter(ref cm, "@intAppointmentTypeID", 1, SqlDbType.Int);
				SetParameter(ref cm, "@intCustomerID", customer.intCustomerID, SqlDbType.Int);
				SetParameter(ref cm, "@lngEmployeeID", lngEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@intServiceID", intServiceID, SqlDbType.Int);



				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}


		public void CancelAppointment(int intAppointmentID) 
		{
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("CANCEL_APPOINTMENT", cn);

				SetParameter(ref cm, "@intAppointmentID", intAppointmentID, SqlDbType.Int);


				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			
		}

		public void InsertService(Services model)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_SERVICE", cn);

				SetParameter(ref cm, "@strServiceName", model.strServiceName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@decServiceCost", model.decServiceCost, SqlDbType.Decimal);
				SetParameter(ref cm, "@intMinutes", model.intMinutes, SqlDbType.Int);
				SetParameter(ref cm, "@intGenderID", model.intGenderID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}


		public void RemoveService(int intServiceID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("REMOVE_SERVICE", cn);

				SetParameter(ref cm, "@intServiceID", intServiceID, SqlDbType.Int);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}


		public List<Genders> GetGenders()
		{

			List<Genders> Genders = new List<Genders>();
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TGenders";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							Genders.Add(new Models.Genders()
							{
								intGenderID = reader.GetInt32(0)
								,
								strGenderDesc = reader.GetString(1)

							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return Genders;
		}


		public List<Employee> GetEmployees()
        {

            List<Employee> objEmployees = new List<Employee>();
            try
            {
                SqlConnection cn = null;
                if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT TE.intEmployeeID, TE.strFirstName, TE.strLastName, TE.strPhoneNumber, TG.strGender, TE.intGenderID, TE.strEmailAddress, TE.strPassword "+
								"FROM TEmployees AS TE JOIN TGenders AS TG ON TG.intGenderID = TE.intGenderID ";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						if (!reader.IsDBNull(0))
							objEmployees.Add(new Employee() {
								intEmployeeID = reader.GetInt64(0),
								strFirstName = reader.GetString(1),
								strLastName = reader.GetString(2),
								strPhoneNumber = reader.GetString(3),
								strGender = reader.GetString(4),
								intGenderID = reader.GetInt32(5),
								strEmailAddress = reader.GetString(6),
								strPassword = reader.GetString(7)
							});
					}
					reader.Close();
				}

                CloseDBConnection(ref cn);				

            } catch(Exception ex) 
			{

			}

			return objEmployees;
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


		public Employee InsertAvailability(Employee e)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE_AVAILABILITY", cn);

				SetParameter(ref cm, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt);
				SetParameter(ref cm, "@dtmStartTime", e.dtmStartTime, SqlDbType.DateTime);
				SetParameter(ref cm, "@dtmEndTime", e.dtmEndTime, SqlDbType.DateTime);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				CloseDBConnection(ref cn);

				return e;

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



		public void InsertCustomerManually(Customer c, int intGenderID, long lngEmployeeID)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_CUSTOMER", cn);

					SetParameter(ref cm, "@strFirstName", c.strFirstName, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strLastName", c.strLastName, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strPassword", "NOLOGIN", SqlDbType.NVarChar);
					SetParameter(ref cm, "@intGenderID", intGenderID, SqlDbType.Int);
					SetParameter(ref cm, "@strPhoneNumber", c.strPhoneNumber, SqlDbType.NVarChar);
					SetParameter(ref cm, "@strEmailAddress", "NOLOGIN", SqlDbType.NVarChar);


					cm.ExecuteReader();

					CloseDBConnection(ref cn);


			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}


		public List<string> SelectEmployeeSkill(Employee e)
		{
			try
			{
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				SqlDataAdapter da = new SqlDataAdapter("SELECT_EMPLOYEE_SKILLS", cn);
				DataSet ds = new DataSet();

				da.SelectCommand.CommandType = CommandType.StoredProcedure;
				SetParameter(ref da, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt);

				List<string> skills = new List<string>(); // Create a list to store skills

				try
				{
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0)
					{
						// Iterate through each row in the result set and add skill names to the list
						foreach (DataRow dr in ds.Tables[0].Rows)
						{
							string SkillName = (string)dr["strSkillName"];
							skills.Add(SkillName);
						}
					}
				}
				catch (Exception ex) { throw new Exception(ex.Message); }
				finally
				{
					CloseDBConnection(ref cn);
				}

				return skills;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		//public void UpdateEmployeeSkills(Employee e)
		//{
		//	SqlConnection cn = null;

		//	if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
		//	SqlCommand cm = new SqlCommand("UpdateEmployeeSkills", cn);

		//	// Add parameters to the stored procedure
		//	SetParameter(ref cm, "@intEmployeeID", e.intEmployeeID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
		//	SetParameter(ref cm, "@intSkillID", e.intSkillID, SqlDbType.Int, Direction: ParameterDirection.Output);

		//	cm.ExecuteReader();

		//	CloseDBConnection(ref cn);
		//}


		public Employee.ActionTypes InsertEmployee(Employee e)
		{
			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_EMPLOYEE", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@strFirstName", e.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strLastName", e.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPassword", e.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPhoneNumber", e.strPhoneNumber, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strEmailAddress", e.strEmailAddress, SqlDbType.NVarChar);
                SetParameter(ref cm, "@intGender", e.intGenderID, SqlDbType.Int);

                SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue)
				{
					case 1: // new user created
						e.intEmployeeID = (int)cm.Parameters["@intEmployeeID"].Value;
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
						e.strPassword = e.strPassword;
						e.strPhoneNumber = (string)dr["strPhoneNumber"];
						e.strEmailAddress = (string)dr["strEmailAddress"];
						e.strYearsOfExperience = (string)dr["strYearsOfExperience"];
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

		public Employee SelectEmployee(Employee e, long lngID) {
			try {
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("SELECT_EMPLOYEE", cn);
				DataSet ds;

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref da, "@intEmployeeID", lngID, SqlDbType.NVarChar);

				try {
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0) {
						DataRow dr = ds.Tables[0].Rows[0];
						e.strFirstName = (string)dr["strFirstName"];
						e.strLastName = (string)dr["strLastName"];
						e.strPhoneNumber = (string)dr["strPhoneNumber"];
						//e.strEmailAddress = (string)dr["strEmailAddress"];
						e.strGender = (string)dr["strGender"];

						e.UserImage = new Image();

						e.UserImage.ImageID = (long)dr["intEmployeeImageID"];
						e.UserImage.ImageData = (byte[])dr["Image"];
						e.UserImage.FileName = (string)dr["FileName"];
						e.UserImage.Size = (long)dr["ImageSize"];
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

        public Employee SelectEmployeeDropDownList(Employee e, long lngID)
        {
            try
            {
                SqlConnection cn = new SqlConnection();
                if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
                SqlDataAdapter da = new SqlDataAdapter("SELECT_EMPLOYEES", cn);
                DataSet ds;

                da.SelectCommand.CommandType = CommandType.StoredProcedure;

                SetParameter(ref da, "@intEmployeeID", lngID, SqlDbType.NVarChar);

                try
                {
                    ds = new DataSet();
                    da.Fill(ds);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataRow dr = ds.Tables[0].Rows[0];
                        e.strFirstName = (string)dr["strFirstName"];
                        e.strLastName = (string)dr["strLastName"];
                        e.strPhoneNumber = (string)dr["strPhoneNumber"];
						e.strEmailAddress = (string)dr["strEmailAddress"];
						// dont know if this has to be genderID or strGender in ""
						e.strGender = (string)dr["strGender"];
						e.strPassword = (string)dr["strPassword"];

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

        public List<string> SelectSkills(long lngID) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				SqlCommand cmd = new SqlCommand("SELECT_EMPLOYEE_SKILLS", cn);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.AddWithValue("@intEmployeeID", lngID);

				List<string> Skills = new List<string>();

				using (IDataReader reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						if (!reader.IsDBNull(0))
							Skills.Add(reader.GetString(0));
					}
					reader.Close();
				}

				CloseDBConnection(ref cn);

				return Skills;
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
				SetParameter(ref cm, "@intGenderID", c.Gender.intGenderID, SqlDbType.Int);
				SetParameter(ref cm, "@strLastName", c.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@intGenderID", c.Gender.intGenderID, SqlDbType.Int);
				SetParameter(ref cm, "@strEmailAddress", c.strEmailAddress, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPhoneNumber", c.strPhoneNumber, SqlDbType.NVarChar);

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
				SetParameter(ref cm, "@strPassword", e.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strFirstName", e.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@intGenderID", e.intGenderID, SqlDbType.Int);
				SetParameter(ref cm, "@strLastName", e.strLastName, SqlDbType.NVarChar);
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


		public List<StylistAvailability> GetTimesByDate(long lngEmployeeID, string dteDate, string enddate)
		{

			List<StylistAvailability> stylistAvailabilities = new List<StylistAvailability>();

			try
			{
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TStylistAvailability WHERE intEmployeeID = " + lngEmployeeID + " AND dtmStartTime > '" + dteDate + "' AND dtmStartTime < '" + enddate + "' AND blnIsAvailable = 1";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						if (!reader.IsDBNull(0))
							stylistAvailabilities.Add(new Models.StylistAvailability()
							{
								intStylistAvailability = reader.GetInt32(0)
								,
								lngEmployeeID = reader.GetInt64(1)
								,
								dteStartTime = reader.GetDateTime(2)
								,
								dteEndTime = reader.GetDateTime(3)

							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return stylistAvailabilities;
		}



		public List<AppointmentTypes> GetAppointmentTypes() {

			List<AppointmentTypes> AppointmentTypes = new List<AppointmentTypes>();

			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TAppointmentTypes";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						if (!reader.IsDBNull(0))
							AppointmentTypes.Add(new Models.AppointmentTypes() {
								intAppointmentTypeID = reader.GetInt32(0)
								,
								strAppointmentName = reader.GetString(1)
								,
								intEstTimeInMins = reader.GetInt32(2)

							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return AppointmentTypes;
		}

		public List<Services> GetServiceTypes() {

			List<Services> ServiceTypes = new List<Services>();

			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");

				string query = "SELECT * FROM TServices";

				SqlCommand cmd = new SqlCommand(query, cn);

				using (IDataReader reader = cmd.ExecuteReader()) {
					while (reader.Read()) {
						if (!reader.IsDBNull(0))
							ServiceTypes.Add(new Models.Services() {
								intServiceID = reader.GetInt32(0)
								,
								strServiceName = reader.GetString(1)
								,
								decServiceCost = reader.GetDecimal(2)
								,
								intMinutes = reader.GetInt32(3)
								,
								intGenderID = reader.GetInt32(4)
							});

					}
					reader.Close();

				}

				CloseDBConnection(ref cn);

			}
			catch (Exception ex) { throw new Exception(ex.Message); }
			{
			}
			return ServiceTypes;
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

