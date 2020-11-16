<Query Kind="Statements">
  <Connection>
    <ID>126838e6-919c-4c32-ba3c-cf72d74c783a</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>WorkSchedule</Database>
  </Connection>
</Query>

var employeeid = 23;
var results = 	from workday in Schedules						
				where workday.EmployeeID == employeeid 
				&&  workday.Shift.PlacementContract.EndDate > DateTime.Today
				select new
				{
					Title = workday.Shift.PlacementContract.Title,
					From = workday.Shift.PlacementContract.StartDate,
					To = workday.Shift.PlacementContract.EndDate,
				};
results.Dump();