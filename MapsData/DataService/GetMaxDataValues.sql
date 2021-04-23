select * from locationData d
join (
select count(d.locationid) dataCount, 
	d.locationid,
	locationname, 
	max(Gust) MaxGust, 
	max(WindSpeed) MaxWindSpeed, 
	max(AtmosphericPressure) MaxAtmosphericPressure	
from locationData d
join locationmap m
on d.locationID = m.locationID
group by d.locationID, locationName) m
on d.locationID = m.locationID and d.Gust = m.MaxGust 

select Max(gust) from locationData