﻿using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static WhereMe.Helpers.GeocodingParser;

namespace WhereMe.Helpers
{
    public class AddressComponent
    {
        public string long_name { get; set; }
        public string short_name { get; set; }
        public IList<string> types { get; set; }
    }

    public class Location
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Northeast
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Southwest
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }

    public class Viewport
    {
        public Northeast northeast { get; set; }
        public Southwest southwest { get; set; }
    }

    public class Geometry
    {
        public Location location { get; set; }
        public string location_type { get; set; }
        public Viewport viewport { get; set; }
    }

    public class Result
    {
        public IList<AddressComponent> address_components { get; set; }
        public string formatted_address { get; set; }
        public Geometry geometry { get; set; }
        public string place_id { get; set; }
        public IList<string> types { get; set; }
    }

    public class GeocodingParser
    {
        public IList<Result> results { get; set; }
        public string status { get; set; }
    }
    //public class PlusCode
    //{
    //    public string compound_code { get; set; }
    //    public string global_code { get; set; }
    //}

    //public class AddressComponent
    //{
    //    public string long_name { get; set; }
    //    public string short_name { get; set;}

    //    public IList<string> types { get; set; }
    //}

    //public class Location
    //{
    //    public double lat { get; set; }
    //    public double lng { get; set; }
    //}
    //public class Northeast
    //{
    //    public double lat { get; set; }
    //    public double lng { get; set; }
    //}
    //public class Southwest
    //{
    //    public double lat { get; set; }
    //    public double lng { get; set; }
    //}

    //public class Viewport
    //{
    //    public Northeast northeast { get; set; }
    //    public Southwest southwest { get; set; }
    //}

    //public class Bounds
    //{
    //    public Northeast northeast { get; set; }
    //    public Southwest southwest { get; set; }
    //}

    //public class Geometry
    //{
    //    public Location location { get; set; }
    //    public string location_type { get; set; }
    //    public Viewport viewport { get; set; }
    //    public Bounds bounds { get; set; }  
    //}

    //public class Result
    //{
    //    public IList<AddressComponent> address_components { get; set; }
    //    public string formatted_address { get; set; }
    //    public Geometry geometry { get; set; }

    //}
    //public class GeocodingParser
    //{
    //    public PlusCode plus_code { get; set; }
    //    public IList<Result> results { get; set; }
    //    public string status { get; set; }
    //}

}