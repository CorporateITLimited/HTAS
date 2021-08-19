using HoldingTaxWebApp.Manager.Tax;
using HoldingTaxWebApp.Models.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HoldingTaxWebApp.Controllers.Tax
{
    public class PrimaryTransactionController : Controller
    {
        private readonly PrimaryTransactionManager _PrimaryTransactionManager;


        public PrimaryTransactionController()
        {
            _PrimaryTransactionManager = new PrimaryTransactionManager();
        }

        // GET: PrimaryTransaction
        public ActionResult Index()
        {
            try
            {
                var PrimaryTransactionList = new List<PrimaryTransaction>();
                PrimaryTransactionList = _PrimaryTransactionManager.GetAllPrimaryTransaction();

                List<PrimaryTransaction> PrimaryTransactionListVM = new List<PrimaryTransaction>();
                foreach (var item in PrimaryTransactionList)
                {
                    PrimaryTransaction PrimaryTransactionVM = new PrimaryTransaction()
                    {

                       PrimaryTransactionId  = item.PrimaryTransactionId,
                       Status  = item.Status,
                       TranDate  = item.TranDate,
                       TranId  = item.TranId,
                       ValId  = item.ValId,
                       Amount  = item.Amount,
                       StoreAmount  = item.StoreAmount,
                       CardType  = item.CardType,
                       CardNo  = item.CardNo,
                       Currency  = item.Currency,
                       BankTranId  = item.BankTranId,
                       CardIssuer  = item.CardIssuer,
                       CardBrand  = item.CardBrand,
                       CardIssuerCountry  = item.CardIssuerCountry,
                       CardIssuerCountryCode  = item.CardIssuerCountryCode,
                       CurrencyType  = item.CurrencyType,
                       CurrencyAmount  = item.CurrencyAmount,
                       EmiInstalment  = item.EmiInstalment,
                       EmiAmount  = item.EmiAmount,
                       DiscountAmount  = item.DiscountAmount,
                       DiscountPercentage  = item.DiscountPercentage,
                       DiscountRemarks  = item.DiscountRemarks,
                       ValueA  = item.ValueA,
                       ValueB  = item.ValueB,
                       ValueC  = item.ValueC,
                       ValueD  = item.ValueD,
                       RiskLevel  = item.RiskLevel,
                       SecondaryStatus  = item.SecondaryStatus,
                       RiskTitle  = item.RiskTitle,
                       CreateDate = item.CreateDate,
                       StringCreateDate = $"{item.CreateDate:dd/MM/yyyy}",
                       StringTranDate = $"{item.TranDate:dd/MM/yyyy}",
                       HolderName = item.HolderName,
                       FinancialYear = item.FinancialYear

                    };
                    PrimaryTransactionListVM.Add(PrimaryTransactionVM);
                }
                return View(PrimaryTransactionListVM.ToList());
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }

        // GET: PrimaryTransaction/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var item = _PrimaryTransactionManager.GetPrimaryTransactionById(id);

               
                PrimaryTransaction PrimaryTransactionVM = new PrimaryTransaction()
                {
                    PrimaryTransactionId = item.PrimaryTransactionId,
                    Status = item.Status,
                    TranDate = item.TranDate,
                    TranId = item.TranId,
                    ValId = item.ValId,
                    Amount = item.Amount,
                    StoreAmount = item.StoreAmount,
                    CardType = item.CardType,
                    CardNo = item.CardNo,
                    Currency = item.Currency,
                    BankTranId = item.BankTranId,
                    CardIssuer = item.CardIssuer,
                    CardBrand = item.CardBrand,
                    CardIssuerCountry = item.CardIssuerCountry,
                    CardIssuerCountryCode = item.CardIssuerCountryCode,
                    CurrencyType = item.CurrencyType,
                    CurrencyAmount = item.CurrencyAmount,
                    EmiInstalment = item.EmiInstalment,
                    EmiAmount = item.EmiAmount,
                    DiscountAmount = item.DiscountAmount,
                    DiscountPercentage = item.DiscountPercentage,
                    DiscountRemarks = item.DiscountRemarks,
                    ValueA = item.ValueA,
                    ValueB = item.ValueB,
                    ValueC = item.ValueC,
                    ValueD = item.ValueD,
                    RiskLevel = item.RiskLevel,
                    SecondaryStatus = item.SecondaryStatus,
                    RiskTitle = item.RiskTitle,
                    CreateDate = item.CreateDate,
                    StringCreateDate = $"{item.CreateDate:dd/MM/yyyy}",
                    StringTranDate = $"{item.TranDate:dd/MM/yyyy}",
                    HolderName = item.HolderName,
                    FinancialYear = item.FinancialYear

                };



                return View(PrimaryTransactionVM);
            }
            catch (Exception exception)
            {
                TempData["EM"] = "error | " + exception.Message.ToString();
                return View();
                //return RedirectToAction("Error", "Home");
            }
        }





        // GET: PrimaryTransaction/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PrimaryTransaction/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrimaryTransaction/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PrimaryTransaction/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: PrimaryTransaction/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PrimaryTransaction/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
