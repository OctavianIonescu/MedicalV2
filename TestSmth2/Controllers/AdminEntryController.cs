﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using TestSmth2.Models.ViewModels;
using TestSmth2.Models.Domain;
using TestSmth2.Repos;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace TestSmth2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminEntryController : Controller
    {
        private readonly IEntryRepo entryRepo;
        private readonly IAntiBioticRepo antibioticRepo;
        private readonly IPatientRepo patientRepo;
        private readonly IResistanceRepo resistanceRepo;

        public AdminEntryController(IEntryRepo entryRepo, IAntiBioticRepo antibioticRepo, IPatientRepo patientRepo, IResistanceRepo resistanceRepo)
        {
            this.entryRepo = entryRepo;
            this.antibioticRepo = antibioticRepo;
            this.patientRepo = patientRepo;
            this.resistanceRepo = resistanceRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var Antibiotics = await antibioticRepo.GetAllAsync();
            var ResistanceMechanisms = await resistanceRepo.GetAllAsync();

            var model = new BigView()
            {
                OperateEntry = new OperateEntryRequest
                {
                    Antibiotics = Antibiotics.Select(x => new SelectListItem { Text = x.name, Value = x.ID.ToString() }),
                    Resistance = ResistanceMechanisms.Select(x => new SelectListItem { Text = x.Name, Value = x.ID.ToString() })
                },
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Add(BigView req, string[] SelectedAntibiotics, string[] SelectedResistance, string SelectedType)
        {
            var patient = await patientRepo.GetAsync(req.Patient.PatientCNP);
            if(patient == null)
            {
                var newpat = new Patient
                {
                    PatientCNP = req.Patient.PatientCNP,
                    PatientFirstName = req.Patient.PatientFirstName,
                    PatientLastName = req.Patient.PatientLastName
                };
                await patientRepo.AddAsync(newpat);
            }
            var newEntry = new Entry
            {
                entryCode = req.OperateEntry.entryCode,
                PatientCNP = req.Patient.PatientCNP,         
                PathologicProduct = req.OperateEntry.PathologicProduct,
                Microbe = req.OperateEntry.Microbe,
                sectiaDeProvenienta = req.OperateEntry.sectiaDeProvenienta,
                shortDescription = req.OperateEntry.shortDescription,
                medicSolicitant = req.OperateEntry.medicSolicitant,
                URLHandle = req.OperateEntry.entryCode,
                Type = SelectedType,
                FileURL = req.OperateEntry.FileURL,
                collectionDate = req.OperateEntry.collectionDate,
                validationDate = req.OperateEntry.validationDate,
            };
            var antibiotics = new List<AntiBiotic>();
            foreach (var item in SelectedAntibiotics)
            {
                var itemGuid = Guid.Parse(item);
                var existingAntibiotic = await antibioticRepo.GetAsync(itemGuid);
                if (existingAntibiotic != null)
                {
                    antibiotics.Add(existingAntibiotic);
                }
            }
            newEntry.Tags = antibiotics;
            var resistance = new List<ResistanceMechanism>();
            foreach (var item in SelectedResistance)
            {
                var itemGuid = Guid.Parse(item);
                var existingResistance = await resistanceRepo.GetAsync(itemGuid);
                if (existingResistance != null)
                {
                    resistance.Add(existingResistance);
                }
            }
            newEntry.Resistance = resistance;
            await entryRepo.AddAsync(newEntry);
            return RedirectToAction("Add");
        }
        [HttpGet]
        [ActionName("EditGet")]
        public async Task<IActionResult> Edit(Guid ID)
        {
            var entry = await entryRepo.GetAsync(ID);
            var antibioticList = await antibioticRepo.GetAllAsync();
            var resistanceList = await resistanceRepo.GetAllAsync();
            if (entry != null)
            {
                var model = new BigView
                {
                    OperateEntry = new OperateEntryRequest
                    {
                        ID = entry.ID,
                        entryCode = entry.entryCode,
                        PathologicProduct = entry.PathologicProduct,
                        PatientCNP = entry.PatientCNP,
                        Microbe = entry.Microbe,
                        sectiaDeProvenienta = entry.sectiaDeProvenienta,
                        shortDescription = entry.shortDescription,
                        medicSolicitant = entry.medicSolicitant,
                        URLHandle = entry.URLHandle,
                        FileURL = entry.FileURL,
                        Type = entry.Type,
                        collectionDate = entry.collectionDate,
                        validationDate = entry.validationDate,
                        Antibiotics = antibioticList.Select(t => new SelectListItem { Text = t.name, Value = t.ID.ToString() }),
                        SelectedAntibiotics = entry.Tags.Select(x => x.ID.ToString()).ToArray(),
                        Resistance = resistanceList.Select(t => new SelectListItem { Text = t.Name, Value = t.ID.ToString() }),
                        SelectedResistance = entry.Resistance.Select(x => x.ID.ToString()).ToArray()
                    }
                };
                return View(model);
            }
            return View(null);
        }
        [HttpPost]
        [ActionName("Edit")]
        public async Task<IActionResult> Edit(BigView req, string[] SelectedAntibiotics, string[] SelectedResistance, string SelectedType)
        {
            var newEntry = new Entry
            {
                ID = req.OperateEntry.ID,
                entryCode = req.OperateEntry.entryCode,
                PatientCNP = req.OperateEntry.PatientCNP,
                Patient = patientRepo.GetAsync(req.OperateEntry.PatientCNP).Result,
                PathologicProduct = req.OperateEntry.PathologicProduct,
                Microbe = req.OperateEntry.Microbe,
                sectiaDeProvenienta = req.OperateEntry.sectiaDeProvenienta,
                shortDescription = req.OperateEntry.shortDescription,
                Type = SelectedType,
                medicSolicitant = req.OperateEntry.medicSolicitant,
                URLHandle = req.OperateEntry.entryCode,
                FileURL = req.OperateEntry.FileURL,
                collectionDate = req.OperateEntry.collectionDate,
                validationDate = req.OperateEntry.validationDate,
                Tags = new List<AntiBiotic>()
            };
            var antibiotics = new List<AntiBiotic>();
            foreach (var item in SelectedAntibiotics)
            {
                var itemGuid = Guid.Parse(item);
                var existingAntibiotic = await antibioticRepo.GetAsync(itemGuid);
                if (existingAntibiotic != null)
                {
                    antibiotics.Add(existingAntibiotic);
                }
            }
            newEntry.Tags = antibiotics;
            var resistance = new List<ResistanceMechanism>();
            foreach (var item in SelectedResistance)
            {
                var itemGuid = Guid.Parse(item);
                var existingResistance = await resistanceRepo.GetAsync(itemGuid);
                if (existingResistance != null)
                {
                    resistance.Add(existingResistance);
                }
            }
            newEntry.Resistance = resistance;
            var x = newEntry.ID;
            var updateList = await entryRepo.UpdateAsync(newEntry);
            if(updateList != null)
            {
                //success
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit");
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var entries = await entryRepo.GetAllAsync();
            return View(entries);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(OperateEntryRequest req)
        {
            var delEntry = await entryRepo.DeleteAsync(req.ID);
            if(delEntry != null)
            {
                return RedirectToAction("List");
            }
            return RedirectToAction("Edit", new {id = req.ID });
        }

    }
}

