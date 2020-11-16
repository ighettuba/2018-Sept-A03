using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.DAL;
using ChinookSystem.Entities;
using System.ComponentModel; //needed for wizard impllementation of ObjecDataSource
using ChinookSystem.ViewModels;
#endregion
namespace ChinookSystem.BLL
{
    //Must be public.....Accessible 
    //expose the library class for the wizard
    [DataObject]
    public class ArtistController
    {
        //expose the class method for the wizard
        [DataObjectMethod(DataObjectMethodType.Select,false)]
        public List <SelectionList> Artist_List()
        {
            using (var context = new ChinookSystemContext())
            {
                //due to the fact that the entities are internal
                // you will NOT be able to use the entity definitions (classes)
                // as return datatypes

                //instead, we will create ViewModel classes that will contain
                // the data defintion for your retun data types

                //to fill the view model classes, we will use Linq Queries
                //Linq Queries return their data as IEnumerable or IQueryable datasets
                //you can use var when declaring your query receiving variable
                //this Linq query uses the syntax method for coding
                var results = from x in context.Artists
                              select new SelectionList
                              {
                                  ValueId = x.ArtistId,
                                  DisplayText = x.Name
                              };
                return results.OrderBy(x => x.DisplayText).ToList();
            }
        }
    }
}
