using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Maria_Sons.Models;

namespace Maria_Sons.Controllers;

public class HomeController : Controller
{

    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }

    public IActionResult Index(string name)
    {
        IEnumerable<CallLogs> callLogs = _db.CallLogs;
        if (name == null)
        {
            return View(callLogs);
        }
        return View(callLogs.Where(x => x.CallerId.Contains(name)));

    }

    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(CallLogs obj)
    {
        
       
        if (ModelState.IsValid)
        {
            obj.CostOfCall = Math.Round(CalculateCallCost(obj.Duration, obj.CallType), 3);
            
            obj.CallerId = obj.CallerId.ToLower();
            _db.CallLogs.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(obj);

    }

    private double CalculateCallCost(int callDuration, CallType callType)
    {
        double cost = 0.0;
        int rem = callDuration % 60;
        if (callDuration <= 60)
        {

            switch (callType)
            {
                case CallType.CellPhone:
                    cost = (60 * 0.10)/60;
                    break;
                case CallType.FixedLine:
                    cost = (60 * 0.08)/60;
                    break;
                case CallType.International:
                    cost = (60 * 2.0)/60;
                    break;
            }
            return cost;
        }

        else if (callDuration > 60 && rem < 30)
        {
            int newCallduration = callDuration - rem;
            switch (callType)
            {
                case CallType.CellPhone:
                    cost = (newCallduration * 0.10)/60;
                    break;
                case CallType.FixedLine:
                    cost = (newCallduration * 0.08)/60;
                    break;
                case CallType.International:
                    cost = (newCallduration * 2.0)/60;
                    break;
            }
            return cost;

        }
        else if(callDuration > 60 && rem > 30)
        {
            int newCallDuration = (callDuration - rem) + 60;
            switch (callType)
            {
                case CallType.CellPhone:
                    cost = (newCallDuration * 0.10) / 60;
                    break;
                case CallType.FixedLine:
                    cost = (newCallDuration * 0.08) / 60;
                    break;
                case CallType.International:
                    cost = (newCallDuration * 2.0) / 60;
                    break;
            }
            return cost;
        }


        return cost;
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        var logs = _db.CallLogs.Find(id);
        if (logs== null)
        {
            return NotFound();
        }
        return View(logs);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeletePost(int? id)
    {
        var log = _db.CallLogs.Find(id);

        if (log == null)
        {
            return NotFound();
        }

        _db.CallLogs.Remove(log);
        _db.SaveChanges();
        //TempData["success"] = "Category deleted succesfully";
        return RedirectToAction("Index");


    }
}