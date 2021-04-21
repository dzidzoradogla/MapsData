-- Fetch location data by page

Create Proc [dbo].[Usp_GetLocationDataByPage]  
 @PageNo INT ,  
 @PageSize INT 
As  
Begin  
Select * From   (
	Select ROW_NUMBER() Over ( Order by d.locationID ) AS 'RowNum', 
		d.AtmosphericPressure,
		d.Gust,
		d.locationID,
		d.id,
		d.time,
		d.WindDirection,
		d.WindSpeed
	from   locationData d ) t
	where t.RowNum Between ((@PageNo-1)*@PageSize +1) AND (@PageNo*@pageSize)  
End   

--EXEC Usp_GetLocationDataByPage 1,500