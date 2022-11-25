using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication7;

namespace BankSite.Controllers
{
    public class BankFunctionController : Controller
    {
        // GET: BankFunction
        public ActionResult ShowWallets()
        {
            Wallets wallets = new Wallets();
            
            return View(wallets.TakeWallets());
        }

        
    }
}