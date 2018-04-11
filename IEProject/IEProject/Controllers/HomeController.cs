// ***********************************************************************
// Assembly         : IEProject
// Author           : Kenneth
// Created          : 03-23-2018
//
// Last Modified By : Kenneth
// Last Modified On : 03-28-2018
// ***********************************************************************
// <copyright file="HomeController.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary>This is a controller class that determines what request to send back to the user when a request is made on the website.
// This controller is part of the MVC design pattern. This class controls all the action links on the website.</summary>
// ***********************************************************************
using IEProject.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.IO;
using System.Web.Mvc;
using System.Device.Location;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// The Controllers namespace.
/// </summary>
namespace IEProject.Controllers
{
    /// <summary>
    /// Class HomeController.
    /// </summary>
    /// <seealso cref="System.Web.Mvc.Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// The database
        /// </summary>
        private AccessibilityEntities db = new AccessibilityEntities();
        /// <summary>
        /// This method returns the landing page of the website.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method returns the About page of the website.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// This method returns the contact page of the website.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        /// <summary>
        /// This method returns the Wheelchair Public Toilet page upon clicking the menu button that is named Wheelchair public toilets.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Publictoilet()
        {
            ViewBag.Message = "Wheelchair public toilets.";
            var model = new PublicToiletForm();
            var PublicToilets = new List<PublicToilet>();
            PublicToilets = db.PublicToilets.ToList();


            return View(model);
        }

        /// <summary>
        /// This is a post method that returns the nearest Wheelchair accessibility toilets after the destination address has been typed into the search box.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        public ActionResult Publictoilet(PublicToiletForm model)
        {
            ViewBag.Message = "Wheelchair public toilets.";
            var PublicToilets = new List<PublicToilet>();
            PublicToilets = db.PublicToilets.ToList();
            GeoCoordinate distanceFrom = new GeoCoordinate();
            GeoCoordinate distanceTo = new GeoCoordinate();
            model.toilets = PublicToilets;
            distanceFrom.Latitude = model.Latitude;
            distanceFrom.Longitude = model.Longitude;
            List<ToiletNearby> ordered = new List<ToiletNearby>();

            /// <summary>
            /// This is a for loop that calculates the distance for all the wheelchair public toilets from the destination address, and adds them to a new List.
            /// </summary>
            foreach (var toilet in PublicToilets)
            {
                string[] tempLocation = toilet.location.Trim().Substring(toilet.location.IndexOf("(") + 1, toilet.location.LastIndexOf(")") - 1).Split(',');
                double latitude = Double.Parse(tempLocation[0]);
                double longitude = Double.Parse(tempLocation[1]);
                distanceTo.Latitude = latitude;
                distanceTo.Longitude = longitude;
                ordered.Add(new ToiletNearby { name = toilet.name, distance = Math.Round(distanceFrom.GetDistanceTo(distanceTo),0) });
            }

            /// <summary>
            /// This sorts all items in the list named ordered and adds the top 5 items(wheelchair public toilets) to another list.
            /// </summary>
            if (model.select == false) {
                model.Address = "";
            }
            var regex = "^[#.0-9a-zA-Z\\s,-]+$";
            Match match = Regex.Match(model.Address ?? "", regex, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                
                List<ToiletNearby> SortedList = ordered.OrderBy(a => a.distance).ToList();
                for (int i = 0; i < 5; i++)
                {
                    model.sorttoilets.Add(SortedList[i]);
                    model.sorttoilets[i].name = model.sorttoilets[i].name.Substring(model.sorttoilets[i].name.LastIndexOf('-') + 1);
                }
                
                return View(model);
            }
            else
            {
                model.sorttoilets.Clear();
                return View(model);
            }
        }

    }
}