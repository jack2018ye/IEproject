// ***********************************************************************
// Assembly         : IEProject
// Author           : Kenneth
// Created          : 03-24-2018
//
// Last Modified By : Kenneth
// Last Modified On : 03-28-2018
// ***********************************************************************
// <copyright file="autoapi.js">
// <summary>This is a javascript file that uses google's api to autosuggest addresses when input is being
//made on the destination address text field. It also Collects the laitude and longitude for the address 
//and stores it in the public toilet model.</summary>
// ***********************************************************************

    google.maps.event.addDomListener(window, 'load', function () {  
        var options = {  
            types: ['address'],  
            componentRestrictions: { country: "AU" }  
        };  
  
        var input = document.getElementById('Address');
       
        var places = new google.maps.places.Autocomplete(input, options);
        
        
        
        geocoder = new google.maps.Geocoder();
        
        var address = document.getElementById('Address').value;
       
        
        var selectstatus = false;
        document.getElementById('select').value = selectstatus;

        google.maps.event.addListener(places, 'place_changed', function () {
            selectstatus = true;
            document.getElementById('select').value = selectstatus;
            var place = places.getPlace();
            var lat = place.geometry.location.lat();
            var lng = place.geometry.location.lng();
           
            document.getElementById('Latitude').value = lat;
            document.getElementById('Longitude').value = lng;
        });

        
        


  
    });

    