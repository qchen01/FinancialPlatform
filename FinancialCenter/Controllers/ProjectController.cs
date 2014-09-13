using FinancialCenter.Models;
using FinancialCenter.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FinancialCenter.Controllers
{
    public class ProjectController : Controller
    {
        [AuthenticationFilter]
        public ActionResult Index()
        {
            Account account = Session["account"] as Account;

            FinancialPlatformEntities pe = new FinancialPlatformEntities();
            var result = pe.Projects.Select(x => new { x.ID, x.Name, x.NeededLevel, x.PublishTime, x.TotalAmount }).Where(y => y.NeededLevel == account.AccountLevel).ToList();
            Projects projects = new Projects();
            projects.Level = account.AccountLevel;

            List<Project> projectlist = new List<Project>();
            foreach (var r in result)
            {
                Project p = new Project()
                {
                    ID = r.ID,
                    Name = r.Name,
                    NeededLevel = r.NeededLevel,
                    PublishTime = r.PublishTime,
                    TotalAmount = r.TotalAmount
                };

                projectlist.Add(p);
            }

            projects.ProjectList = projectlist;

            return View(projects);
        }

        public ActionResult PurchaseProject() 
        {
            return View();
        }
	}
}