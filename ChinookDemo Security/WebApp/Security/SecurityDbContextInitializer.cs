using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

#region Additional namespaces
using WebApp.Models;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;


#endregion


namespace WebApp.Security
{
    public class SecurityDbContextInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            #region Seed the roles
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var startupRoles = ConfigurationManager.AppSettings["startupRoles"].Split(';');
            foreach (var role in startupRoles)
                roleManager.Create(new IdentityRole { Name = role });
            #endregion

            #region Seed the users
            string adminUser = ConfigurationManager.AppSettings["adminUserName"];
            string adminRole = ConfigurationManager.AppSettings["adminRole"];
            string adminEmail = ConfigurationManager.AppSettings["adminEmail"];
            string adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var result = userManager.Create(new ApplicationUser
            {
                UserName = adminUser,
                Email = adminEmail
            }, adminPassword);
            //test if Admin account creation was succesful
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(adminUser).Id, adminRole);

            //in your program, you will need to seed some users
            string customerUser = "HansenB";
            string customerRole = "Customers";
            string customerEmail = "bjorn.hansen@yahoo.no";
            string customerPassword = ConfigurationManager.AppSettings["newUserPassword"];
            int customerId = 4;
            var result2 = userManager.Create(new ApplicationUser
            {
                UserName = customerUser,
                Email = customerEmail,
                CustomerId = customerId

            }, customerPassword);
            //test if customer account creation was succesful
            if (result.Succeeded)
                userManager.AddToRole(userManager.FindByName(customerUser).Id, customerRole);

           
            #endregion

            // ... etc. ...

            base.Seed(context);
        }
    }
}