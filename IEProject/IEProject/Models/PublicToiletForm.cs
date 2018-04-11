// ***********************************************************************
// Assembly         : IEProject
// Author           : Kenneth
// Created          : 03-23-2018
//
// Last Modified By : Kenneth
// Last Modified On : 03-28-2018
// ***********************************************************************
// <copyright file="PublicToiletForm.cs" company="">
//     Copyright ©  2018
// </copyright>
// <summary> This is a model class that stores all the variable values for the Wheelchair Public Toilet page. This makes it possible to store variables and
// pass them for use in the controller. The model class follows the MVC design pattern.</summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

/// <summary>
/// The Models namespace.
/// </summary>
namespace IEProject.Models
{
    /// <summary>
    /// Class PublicToiletForm.
    /// </summary>
    public class PublicToiletForm
    {

        public bool select { get; set; }

        /// <summary>
        /// Gets or sets the distance.
        /// </summary>
        /// <value>The distance.</value>
        public double Distance { get; set; }
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }
        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        [Required(ErrorMessage = "Required")]
        [RegularExpression("^[#.0-9a-zA-Z\\s,-]+$", ErrorMessage = "Invalid Address or Postcode") ]      
       [StringLength(100, ErrorMessage = "Invalid Input")]
        public string Address { get; set; }






        /// <summary>
        /// Gets or sets the toilets.
        /// </summary>
        /// <value>The toilets.</value>
        public List<PublicToilet> toilets { get; set; }
        /// <summary>
        /// Gets or sets the sorttoilets.
        /// </summary>
        /// <value>The sorttoilets.</value>
        public List<ToiletNearby> sorttoilets { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="PublicToiletForm"/> class.
        /// </summary>
        public PublicToiletForm()
        {
            toilets = new List<PublicToilet>();
            sorttoilets = new List<ToiletNearby>();






        }
    }
}