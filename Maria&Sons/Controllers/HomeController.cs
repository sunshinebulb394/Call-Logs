﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Maria_Sons.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;

namespace Maria_Sons.Controllers;

public class HomeController : Controller
{

    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }


    [Authorize(Roles = "CEO")]
    public IActionResult Index(string name)
    {
        IEnumerable<CallLogs> callLogs = _db.CallLogs;
        if (name == null)
        {
            return View(callLogs);
        }
       
        return View(callLogs.Where(x => x.CallerId.Contains(name.ToLower())));

    }

  

    public IActionResult Create()
    {
        return View();
    }

    //POST
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Roles = "CEO,RECEPTIONIST")]
    public IActionResult Create(CallLogs obj)
    {
        
       
        if (ModelState.IsValid)
        {
            obj.CostOfCall = Math.Round(CalculateCallCost(obj.Duration, obj.CallType), 3);
            
            obj.CallerId = obj.CallerId.ToLower();
            _db.CallLogs.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Call added succesfully";
            return RedirectToAction("Index");
        }
        return View(obj);

    }

    private double CalculateCallCost(int callDuration, CallType callType)
    {
        double cost = 0;
        int secondsPerMinute = 60;
        double costPerMinute = 0;
        if (callDuration <= 0)
        {
            return cost;
        }

        int rem = callDuration % 60;

        switch (callType)
        {
            case CallType.CellPhone:
                costPerMinute = 0.10;
                break;
            case CallType.FixedLine:
                costPerMinute = 0.08;
                break;
            case CallType.International:
                costPerMinute = 2.0;
                break;

        }

        if (rem >= 30)
        {
            callDuration += secondsPerMinute - rem;
        }
        else
        {
            callDuration -= rem;
        }
        cost = costPerMinute * callDuration / secondsPerMinute;
        //cost *= callDuration / 60.0;
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
    [Authorize(Roles = "CEO")]
    public IActionResult DeletePost(int? id)
    {
        var log = _db.CallLogs.Find(id);

        if (log == null)
        {
            return NotFound();
        }

        _db.CallLogs.Remove(log);
        _db.SaveChanges();
        TempData["success"] = "Call deleted succesfully";
        return RedirectToAction("Index");


    }
}