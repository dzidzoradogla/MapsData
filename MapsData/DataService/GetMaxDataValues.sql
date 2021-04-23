
CREATE Proc [dbo].[Usp_GetMaxDataValues]
AS
BEGIN
select count(d.locationid) dataCount, 
	d.locationid,
	locationname, 
	max(Gust) MaxGust, 
	max(WindDirection) MaxWindDirection, 
	max(WindSpeed) MaxWindSpeed, 
	max(AtmosphericPressure) MaxAtmosphericPressure	
from locationData d
join locationmap m
on d.locationID = m.locationID
group by d.locationID, locationName
order by MaxWindSpeed desc
END