using FinancialCenter.Models;
using FinancialCenter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialCenter.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            FinancialPlatformEntities pe = new FinancialPlatformEntities();

            var result = pe.Accounts.Select(x => new { x.ID, x.Name, x.Email, x.Password, x.AccountLevel, x.Code }).Where(y => y.Email == email && y.Password == password).ToList();

            if (result!= null && result.Count == 1)
            {
                Account account = new Account() { ID = result[0].ID, 
                    Name = result[0].Name, 
                    Password = result[0].Password, 
                    Email = result[0].Email,
                   AccountLevel = result[0].AccountLevel,
                 Code = result[0].Code};

                Session["account"] = account;
                return RedirectToAction("AccountProfile", "Account", null); 
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Register(string email, string password, string name, string referenceCode)
        {
            Account account = new Account();
            account.Code = referenceCode;
            account.Email = email;
            account.Name = name;
            account.Password = password;
            account.AccountLevel = 0;

            FinancialPlatformEntities pe = new FinancialPlatformEntities();
            pe.Accounts.Add(account);
            pe.SaveChanges();

            Session["account"] = account;

            return RedirectToAction("AccountProfile", "Account", null); 
            
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpGet]
        [AuthenticationFilter]
        public ActionResult AccountProfile()
        {
            FinancialPlatformEntities pe = new FinancialPlatformEntities();


            Account account =  Session["account"] as Account;
            
            return View(account);
        }
	}
}