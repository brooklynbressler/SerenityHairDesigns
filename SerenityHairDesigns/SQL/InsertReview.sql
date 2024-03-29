USE [SerenityHairDesigns]
GO
/****** Object:  StoredProcedure [dbo].[INSERT_REVIEW]    Script Date: 3/28/2023 7:19:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[INSERT_REVIEW] 
	 @strName nvarchar(100)
	,@strEmail nvarchar(20)
	,@strMessage nvarchar(400)
	,@intRating int
       

AS
SET XACT_ABORT ON --terminate and rollback if any errors

BEGIN TRANSACTION

	--DECLARE @intReviewID AS INTEGER
	--SET @intReviewID = MAX(intReviewID) + 1 
    
 --   FROM TReviews (TABLOCKX) -- lock table until end of transaction
    
 --   SELECT intReviewID = COALESCE(intReviewID, 1) -- default to 1 if table is empty

    INSERT INTO TReviews(strName, strEmailAddress, strReview, intRating)
    VALUES (@strName, @strEmail, @strMessage, @intRating)

COMMIT TRANSACTION
