﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TrainingCentreManagement.BLL.Contracts;
using TrainingCentreManagement.DatabaseContext.DatabaseContext;
using TrainingCentreManagement.Models.EntityModels.Scheduls;

namespace TrainingCentreManagement.Controllers
{
    public class SchedulesController : Controller
    {
        private readonly IScheduleManager _iScheduleManager;
        private readonly IScedhuleTypeManager _iScedhuleTypeManager;

        public SchedulesController(IScheduleManager iScheduleManager,IScedhuleTypeManager iScedhuleTypeManager)
        {
            _iScheduleManager = iScheduleManager;
            _iScedhuleTypeManager = iScedhuleTypeManager;
        }

        // GET: Schedules
        public IActionResult Index()
        {
            return View(_iScheduleManager.GetAll().ToList());
        }

        // GET: Schedules/Details/5
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _iScheduleManager.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedules/Create
        public IActionResult Create()
        {
            ViewData["ScheduleTypeId"] = new SelectList(_iScedhuleTypeManager.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                schedule.UpdatedAt=DateTime.Now;
                schedule.CreatedAt=DateTime.Now;
                
                _iScheduleManager.Add(schedule);
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedules/Edit/5
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _iScheduleManager.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, Schedule schedule)
        {
            if (id != schedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _iScheduleManager.Update(schedule);
                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(schedule);
        }

        // GET: Schedules/Delete/5
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = _iScheduleManager.GetById(id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(long id)
        {
            var schedule = _iScheduleManager.GetById(id);
            _iScheduleManager.Remove(schedule);
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleExists(long id)
        {
            return _iScheduleManager.GetAll().Any(e => e.Id == id);
        }
    }
}
