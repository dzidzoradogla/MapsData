-- Fetch location data by page

alter Proc [dbo].[Usp_GetLocationDataByPage]  
 @PageNo INT ,  
 @PageSize INT ,
 @SortOrder NVARCHAR(100) NULL,
 @SearchString NVARCHAR(100) NULL
As  
Begin 
	Select t.AtmosphericPressure,
		t.Gust,
		t.locationID,
		t.id,
		t.[time],
		t.WindDirection,
		t.WindSpeed  From   (
		Select ROW_NUMBER() Over (ORDER BY			
				(CASE
				   WHEN @SortOrder is null THEN d.locationID
				   WHEN @SortOrder = 'AtmosphericPressure' THEN CONVERT(VARCHAR(32), d.AtmosphericPressure)
				   WHEN @SortOrder = 'Gust' THEN CONVERT(VARCHAR(32), Gust ) 
				   WHEN @SortOrder = 'LocationID' THEN d.locationID				   
				   WHEN @SortOrder = 'LocationName' THEN m.locationName
				   WHEN @SortOrder = 'Time' THEN CONVERT(VARCHAR(32),time, 121) 
				   WHEN @SortOrder = 'WindDirection' THEN CONVERT(VARCHAR(32), WindDirection)
				   WHEN @SortOrder = 'WindSpeed' THEN CONVERT(VARCHAR(32), WindSpeed )
				   ELSE d.locationID
				END) 
				)AS 'RowNum', 
				d.AtmosphericPressure,
				d.Gust,
				d.locationID,
				m.locationName,
				d.id,
				d.[time],
				d.WindDirection,
				d.WindSpeed 
		from 	locationData d
				join locationMap m
				on m.locationID = d.locationID
		where m.locationName like '%' +  ISNULL(@SearchString, m.locationName) + '%' 
		or d.locationID like  '%'+ ISNULL(@SearchString, d.locationID) + '%'				
		) t
		where t.RowNum Between ((@PageNo-1)*@PageSize +1) AND (@PageNo*@pageSize)  

End   

--EXEC Usp_GetLocationDataByPage 1,10,'AtmosphericPressure', 'ball'

--EXEC Usp_GetLocationDataByPage 1,10, null, null

	--select top 10 * from locationData order by AtmosphericPressure asc 

	--select * from locationMap

