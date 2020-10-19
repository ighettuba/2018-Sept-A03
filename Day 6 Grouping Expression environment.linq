<Query Kind="Expression">
  <Connection>
    <ID>e5f07e69-57d0-45d8-9f20-7bc256434fd6</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>Chinook</Database>
  </Connection>
</Query>

//GROUPING

//is the technique of placing a large pile of data into smaller piles of data depending
//on a criteria

//navigational properties allow for natural grouping of
//	parent to child (pkey-fkey/) collections
//pinstance.childnavproperty.count() counts all
//	child records associated with the parent instance

//problem: What if there is no navigational properties for
//			the grouping of the data collection

//here you can use the group clause to create a set of
//	smaller collections based on a set of criteria
//
//Its important to remember that once the smaller groups 
//are created, all reporting must use the smalller groups 
//as the collection reference


//Grouping IS NOT the same as  ordering

//sytax
//	group instance by criteria [into group reference name]

//the instance is one record from the original pile of data
//the criterion can be:
//	a.	A single attribute {...}
//	b.	Multiiple attributes new{...,...,...}
//	c.	A class {classname}

//if you wish to do processing on the smaller groupp
//you will place the grouping results into a smaller
//pile of data referenced by a specified name

//groups have 2 components
//	a.	Key component
//	b.	the data component

//report albums by ReleaseYear showing the year and 
// the number of albums for the year. Order by the most albums,
// then by the year within count

//my process to creating group queries
//`	a.	Create and display the grouping
from x in Albums
group x by x.ReleaseYear into gYear
select gYear

//	b.	Create the reporting row for a group
from x in Albums
group x by x.ReleaseYear into gYear
select new
{
	year = gYear.Key,
	albumCount = gYear.Count()
}


//	c.	Put any report customizations
from x in Albums
group x by x.ReleaseYear into gYear
orderby gYear.Count() descending, gYear.Key
select new
{
	year = gYear.Key,
	albumCount = gYear.Count()
}







