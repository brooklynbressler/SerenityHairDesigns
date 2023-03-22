﻿using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Models {

	public class User
    {
        public long intCustomerID = 0;
        public long intEmployeeID = 0;
        public string strFirstName = string.Empty;
        public string strLastName = string.Empty;
        public string strGender = string.Empty;
        public string strPassword = string.Empty;

        //[DataType(DataType.EmailAddress)]
        public string strEmailAddress = string.Empty;

        //[DataType(DataType.PhoneNumber)]
        public string strPhoneNumber = string.Empty;

        public ActionTypes ActionType = ActionTypes.NoType;
        public Image UserImage;
        public List<Image> Images;
        //public List<Event> Events = new List<Event>();


        //public List<Like> Likes;
        //public List<Rating> Ratings;

        //public bool DoesUserLike(Like.Types LikeType, long EventID)
        //{
        //    try
        //    {
        //        foreach (Like l in this.Likes)
        //        {
        //            if (l.Type == LikeType && l.ID == EventID) return true;
        //        }
        //        return false;
        //    }
        //    catch (Exception) { return false; }
        //}

        //public byte GetUserRating(Rating.Types RatingType, long EventID)
        //{
        //    try
        //    {
        //        foreach (Rating r in this.Ratings)
        //        {
        //            if (r.Type == RatingType && r.ID == EventID) return r.Rate;
        //        }
        //        return 0;
        //    }
        //    catch (Exception) { return 0; }
        //}


        public bool IsAuthenticated
        {
            get
            {
                if (intCustomerID > 0) return true;
                return false;
            }
        }

        //public List<Event> GetEvents(long ID = 0)
        //{
        //    try
        //    {
        //        Database db = new Database();
        //        return db.GetEvents(ID, this.UID);
        //    }
        //    catch (Exception ex) { throw new Exception(ex.Message); }
        //}

        public sbyte AddGalleryImage(HttpPostedFileBase f)
        {
            try
            {
                this.UserImage = new Image();
                this.UserImage.Primary = false;
                this.UserImage.FileName = Path.GetFileName(f.FileName);

                if (this.UserImage.IsImageFile())
                {
                    this.UserImage.Size = f.ContentLength;
                    Stream stream = f.InputStream;
                    BinaryReader binaryReader = new BinaryReader(stream);
                    this.UserImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
                    //this.UpdatePrimaryImage();
                }
                return 0;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

		//public sbyte UpdatePrimaryImage()
		//{
		//    try
		//    {
		//        Models.Database db = new Database();
		//        long NewUID;
		//        if (this.UserImage.ImageID == 0)
		//        {
		//            NewUID = db.InsertUserImage(this);
		//            if (NewUID > 0) UserImage.ImageID = NewUID;
		//        }
		//        else
		//        {
		//            db.UpdateUserImage(this);
		//        }
		//        return 0;
		//    }
		//    catch (Exception ex) { throw new Exception(ex.Message); }
		//}

		public User Login() {
			try {
				Database db = new Database();
				return db.Login(this);
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

        public User LoginEmployee()
        {
            try
            {
                Database db = new Database();
                return db.LoginEmployee(this);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public User.ActionTypes Save() {
			try {
				Database db = new Database();
				if (intCustomerID == 0) { //insert new user
					this.ActionType = db.InsertUser(this);
				}
				else {
					this.ActionType = db.UpdateUser(this);
				}
				return this.ActionType;
			}
			catch (Exception ex) { throw new Exception(ex.Message); }
		}

        public User.ActionTypes SaveEmployee()
        {
            try
            {
                Database db = new Database();
                if (intEmployeeID == 0)
                { //insert new user
                    this.ActionType = db.InsertEmployee(this);
                }
                else
                {
                    this.ActionType = db.UpdateUser(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool RemoveUserSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentUser"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public User GetUserSession()
        {
            try
            {
                User u = new User();
                if (HttpContext.Current.Session["CurrentUser"] == null)
                {
                    return u;
                }
                u = (User)HttpContext.Current.Session["CurrentUser"];
                return u;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveUserSession()
        {
            try
            {
                HttpContext.Current.Session["CurrentUser"] = this;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

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
///////////////////////////////////////////////////////////////////////////////
//Spring 2021
///////////////////////////////////////////////////////////////////////////////
