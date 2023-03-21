using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Net;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Models {
	public class Database {


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





		public bool DeleteEvent(long ID) {
			try {
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

		public bool DeleteEventImage(long ID) {
			try {
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
		//			SetParameter(ref cm, "@primary_image", "Y", SqlDbType.Char);
		//		else
		//			SetParameter(ref cm, "@primary_image", "N", SqlDbType.Char);

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
		//			SetParameter(ref cm, "@primary_image", "Y", SqlDbType.Char);
		//		else
		//			SetParameter(ref cm, "@primary_image", "N", SqlDbType.Char);

		//		SetParameter(ref cm, "@image", e.EventImage.ImageData, SqlDbType.VarBinary);
		//		SetParameter(ref cm, "@file_name", e.EventImage.FileName, SqlDbType.NVarChar);
		//		SetParameter(ref cm, "@image_size", e.EventImage.Size, SqlDbType.BigInt);

		//		cm.ExecuteReader();
		//		CloseDBConnection(ref cn);

		//		return 0; //success	
		//	}
		//	catch (Exception ex) { throw new Exception(ex.Message); }
		//}


		public long InsertUserImage(User u) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_USER_IMAGE", cn);

				SetParameter(ref cm, "@user_image_id", null, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@uid", u.intCustomerID, SqlDbType.BigInt);
				if (u.UserImage.Primary)
					SetParameter(ref cm, "@primary_image", "Y", SqlDbType.Char);
				else
					SetParameter(ref cm, "@primary_image", "N", SqlDbType.Char);

				SetParameter(ref cm, "@image", u.UserImage.ImageData, SqlDbType.VarBinary);
				SetParameter(ref cm, "@file_name", u.UserImage.FileName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@image_size", u.UserImage.Size, SqlDbType.BigInt);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);
				return (long)cm.Parameters["@user_image_id"].Value;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public long UpdateUserImage(User u) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_USER_IMAGE", cn);

				SetParameter(ref cm, "@user_image_id", u.UserImage.ImageID, SqlDbType.BigInt);
				if (u.UserImage.Primary)
					SetParameter(ref cm, "@primary_image", "Y", SqlDbType.Char);
				else
					SetParameter(ref cm, "@primary_image", "N", SqlDbType.Char);

				SetParameter(ref cm, "@image", u.UserImage.ImageData, SqlDbType.VarBinary);
				SetParameter(ref cm, "@file_name", u.UserImage.FileName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@image_size", u.UserImage.Size, SqlDbType.BigInt);

				cm.ExecuteReader();
				CloseDBConnection(ref cn);

				return 0; //success	
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public List<Image> GetUserImages(long UID = 0, long UserImageID = 0, bool PrimaryOnly = false) {
			try {
				DataSet ds = new DataSet();
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("SELECT_USER_IMAGES", cn);
				List<Image> imgs = new List<Image>();

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				if (UID > 0) SetParameter(ref da, "@uid", UID, SqlDbType.BigInt);
				if (UserImageID > 0) SetParameter(ref da, "@user_image_id", UserImageID, SqlDbType.BigInt);
				if (PrimaryOnly) SetParameter(ref da, "@primary_only", "Y", SqlDbType.Char);

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
						i.ImageID = (long)dr["UserImageID"];
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

		public bool DeleteUserImage(long ID) {
			try {
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
		public User.ActionTypes InsertUser(User u) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("INSERT_USER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@intCustomerID", u.intCustomerID, SqlDbType.BigInt, Direction: ParameterDirection.Output);
				SetParameter(ref cm, "@strFirstName", u.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strLastName", u.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPassword", u.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strPhoneNumber", u.strPhoneNumber, SqlDbType.NVarChar);
				SetParameter(ref cm, "@strEmailAddress", u.strEmailAddress, SqlDbType.NVarChar);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.TinyInt, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue) {
					case 1: // new user created
						u.intCustomerID = (long)cm.Parameters["@intCustomerID"].Value;
						return User.ActionTypes.InsertSuccessful;
					case -1:
						return User.ActionTypes.DuplicateEmail;
					case -2:
						return User.ActionTypes.DuplicateUserID;
					default:
						return User.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public User Login(User u) {
			try {
				SqlConnection cn = new SqlConnection();
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlDataAdapter da = new SqlDataAdapter("LOGIN_USER", cn);
				DataSet ds;
				User newUser = null;

				da.SelectCommand.CommandType = CommandType.StoredProcedure;

				SetParameter(ref da, "@strEmailAddress", u.strEmailAddress, SqlDbType.NVarChar);
				SetParameter(ref da, "@strPassword", u.strPassword, SqlDbType.NVarChar);

				try {
					ds = new DataSet();
					da.Fill(ds);
					if (ds.Tables[0].Rows.Count > 0) {
						newUser = new User();
						DataRow dr = ds.Tables[0].Rows[0];
						newUser.intCustomerID = (long)dr["intCustomerID"];
						newUser.strFirstName = (string)dr["strFirstName"];
						newUser.strLastName = (string)dr["strLastName"];
						newUser.strPassword = u.strPassword;
						newUser.strPhoneNumber = (string)dr["strPhoneNumber"];
						newUser.strEmailAddress = (string)dr["strEmailAddress"];
					}
				}
				catch (Exception ex) { throw new Exception(ex.Message); }
				finally {
					CloseDBConnection(ref cn);
				}
				return newUser; //alls well in the world
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		public User.ActionTypes UpdateUser(User u) {
			try {
				SqlConnection cn = null;
				if (!GetDBConnection(ref cn)) throw new Exception("Database did not connect");
				SqlCommand cm = new SqlCommand("UPDATE_USER", cn);
				int intReturnValue = -1;

				SetParameter(ref cm, "@intCustomerID", u.intCustomerID, SqlDbType.BigInt);
				SetParameter(ref cm, "@password", u.strPassword, SqlDbType.NVarChar);
				SetParameter(ref cm, "@first_name", u.strFirstName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@last_name", u.strLastName, SqlDbType.NVarChar);
				SetParameter(ref cm, "@email", u.strEmailAddress, SqlDbType.NVarChar);

				SetParameter(ref cm, "ReturnValue", 0, SqlDbType.Int, Direction: ParameterDirection.ReturnValue);

				cm.ExecuteReader();

				intReturnValue = (int)cm.Parameters["ReturnValue"].Value;
				CloseDBConnection(ref cn);

				switch (intReturnValue) {
					case 1: //new updated
						return User.ActionTypes.UpdateSuccessful;
					default:
						return User.ActionTypes.Unknown;
				}
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private bool GetDBConnection(ref SqlConnection SQLConn) {
			try {
				if (SQLConn == null) SQLConn = new SqlConnection();
				if (SQLConn.State != ConnectionState.Open) {
					//SQLConn.ConnectionString = strConnectionString;
					SQLConn.Open();
				}
				return true;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

		private bool CloseDBConnection(ref SqlConnection SQLConn) {
			try {
				if (SQLConn.State != ConnectionState.Closed) {
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
			, Byte Precision = 0, Byte Scale = 0) {
			try {
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
			, Byte Precision = 0, Byte Scale = 0) {
			try {
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
