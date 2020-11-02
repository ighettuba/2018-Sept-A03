<Query Kind="Statements">
  <Connection>
    <ID>b446378d-10eb-4861-86ea-1e2434c35c35</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>J4M35\SQLEXPRESS</Server>
    <DeferDatabasePopulation>true</DeferDatabasePopulation>
    <Database>GroceryList</Database>
  </Connection>
</Query>

//		EXCERCISE 2


//Query 1

//Create a product list which indicates what products are 
//		purchased by our customers and 
//		how many times that product was purchased. 
//		Order the list by most popular product by alphabetic description.

var productList		=	from x in Products
						orderby x.OrderLists.Count() descending,x.Description					
						select new 
						{
							Product = x.Description,
							TimesPurchased =  x.OrderLists.Count()
						};
	productList.Dump();
	
//Query 2

//We want a mailing list for a Valued Customers flyer that is being sent out. 
//		List the customer addresses for customers who have shopped at each store. 
//		List by the store. 
//		Include the store location as well as the customer's address. 
//		Do NOT include the customer name in the results.

var mailingList		=	from x in Stores
						orderby x.Location
						where x.Orders.Count() > 0 
						select new
						{
							Location = x.Location,
							Clients = 	(from y in Orders
											orderby y.Customer.Address
											where x.StoreID == y.StoreID
											select new
											{
												Address = y.Customer.Address,
												City = y.Customer.City,
												Province = y.Customer.Province
											}).Distinct()
						};
	mailingList.Dump();

//Query 3

//Create a Daily Sales per Store request for a specified month. 
//		Order stores by city by location. 
//		For Sales, show order date, number of orders, 
//			total sales without GST tax and total GST tax.

var dailyStoreSales		=	from x in Stores
							orderby x.City, x.Location
							where x.Orders.Count() > 0
							select new
							{
								City = x.City,
								Location = x.Location,
								Sales = ( from y in x.Orders
										select new
										{
											Date = y.OrderDate,
											OrderCount = 	(from z in x.Orders
															where z.OrderDate == y.OrderDate && z.StoreID == x.StoreID
															select z.OrderDate).Count(),
											TotalSales = x.Orders.Sum(g => g.SubTotal),
											TotalGST = x.Orders.Sum(f => f.GST)
										})
								
							};
	dailyStoreSales.Dump();
	
	
	
//Query 4
//Print out all product items on a requested order (use Order #33). 
//		Group by Category and order by Product Description. 
//		You do not need to format money as this would be done at the presentation level. 
//		Use the QtyPicked in your calculations. 

var productOrderList = 	from item in OrderLists 
						where item.OrderID == 33
						group item by item.Product.Category.Description into gProduct						
						orderby gProduct.Key
						select new					
						{
							Category = gProduct.Key,
							OrderProducts = from gRow in gProduct
											select new
											{
												Product = gRow.Product.Description,
												Price =gRow.Price,

												Discount = gRow.Discount,
												SubTotal = gRow.Price * (decimal)(gRow.QtyPicked),
												Tax = gRow.Price * (decimal)(gRow.QtyPicked) * (decimal)(0.05),
												ExtendedPrice = gRow.Price * (decimal)(gRow.QtyPicked) * (decimal)(1.05)
											}
						};
	
productOrderList.Dump();


//Query 5
//Select all orders a picker has done on a particular week (Sunday through Saturday). 
//		List by picker and order by picker and date. 



					
var ordersPicked =	(from picks in Pickers.AsEnumerable()
					join odr in Orders on picks.PickerID equals odr.PickerID					 
					select new
					{
						picker = picks.LastName + ", " + picks.FirstName,
						pickDates = (from y in Orders
									where y.PickerID == picks.PickerID
									orderby y.PickedDate
									select new
									{
										ID = y.OrderID,
										Date = y.PickedDate
									}).Take(7)
					}).Distinct().OrderBy(x => x.picker);
	ordersPicked.Dump();				


//Query 6
//List all the products a customer (use Customer #1) has purchased and the number of times the product was purchased. 
//		Order by number of times purchased then description.

var customerSearch = 4;
var customerProducts = from cust in Customers
						where cust.CustomerID == customerSearch
						select new
						{
							Customer =  cust.LastName + ", " + cust.FirstName,
							OrderCount = cust.Orders.Count(),
							Items	=	from item in OrderLists
										where item.Order.CustomerID == customerSearch
										orderby item.Product.Description 
										select new
										{
											Description = item.Product.Description,
											TimesBought = (from y in OrderLists 
															where y.Order.CustomerID == customerSearch
															select y.ProductID).Distinct().Count(),
										
										}
						};
	customerProducts.Dump();



































