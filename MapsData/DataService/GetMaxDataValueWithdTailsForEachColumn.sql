-- Max gust value per day at location M1
select locationid, time , Max(Gust) maxgust from locationData where locationID = 'M1'
group by locationId, time
order by  time

-- Max AtmosphericPressure value per day at location M1
select locationid, time , Max(AtmosphericPressure) maxgust from locationData where locationID = 'M1'
group by locationId, time
order by time

select * from locationData d 
join locationMap m
on m.locationID = d.locationID
where Gust in (select Max(Gust) maxgust from locationData )

select * from locationData d 
join locationMap m
on m.locationID = d.locationID
where AtmosphericPressure in (select Max(AtmosphericPressure) maxgust from locationData)

select * from locationData d 
join locationMap m
on m.locationID = d.locationID
where WindSpeed in (select Max(WindSpeed) maxgust from locationData)


