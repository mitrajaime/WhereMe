using Android;
using Android.App;
using Android.Gms.Location;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using AndroidX.Core.App;
using WhereMe.Helpers;

namespace WhereMe
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, IOnMapReadyCallback
    {
        readonly string[] permissionGroup =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
        };
        ImageButton locationButton;
        TextView placeTextView;
        GoogleMap map;
        IFusedLocationProviderClient flpc;
        Android.Locations.Location myLastLocation;
        private LatLng myPosition;
        MapHelpers mapHelper = new MapHelpers();   
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            RequestPermissions(permissionGroup, 0);

            SupportMapFragment mapFragment = (SupportMapFragment)SupportFragmentManager.FindFragmentById(Resource.Id.map);
            mapFragment.GetMapAsync(this);
            placeTextView = FindViewById<TextView>(Resource.Id.placeTextView);
            locationButton = FindViewById<ImageButton>(Resource.Id.locationButton);

            locationButton.Click += LocationButton_Click;
        }

        private void LocationButton_Click(object sender, System.EventArgs e)
        {
            if (checkPermission())
            {
                displayLocationAsync();
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            var mapStyle = MapStyleOptions.LoadRawResourceStyle(this, Resource.Raw.mapStyles);
            googleMap.SetMapStyle(mapStyle);
            map = googleMap;

            map.UiSettings.ZoomControlsEnabled = true;
            map.CameraMoveStarted += Map_CameraMoveStarted;
            map.CameraIdle += Map_CameraIdle;
            if (checkPermission())
            {
                displayLocationAsync();
            }
        }

        private async void Map_CameraIdle(object sender, System.EventArgs e)
        {
            var position = map.CameraPosition.Target;
            string key = Resources.GetString(Resource.String.mapkey);
            string address = await mapHelper.FindCoordinateAddress(position, key);

            if (!string.IsNullOrEmpty(address))
            {
                placeTextView.Text = address;

            }
            else
            {
                placeTextView.Text = "Where to?";
            }
        }

        private void Map_CameraMoveStarted(object sender, GoogleMap.CameraMoveStartedEventArgs e)
        {
            placeTextView.Text = "Setting new location";
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            if(grantResults.Length < 1)
            {
                return;
            }

            if (grantResults[0] == Android.Content.PM.Permission.Granted)
            {
                displayLocationAsync();
            }

        }

        bool checkPermission()
        {
            bool permissionGranted = false;
            if(ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Android.Content.PM.Permission.Granted &&
                ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Android.Content.PM.Permission.Granted)
            {
                permissionGranted = false;
            }
            else
            {
                permissionGranted = true;
            }
            return permissionGranted;
        }

        async void displayLocationAsync()
        {
            if (flpc == null)
            {
                flpc = LocationServices.GetFusedLocationProviderClient(this);
            }

            myLastLocation = await flpc.GetLastLocationAsync();

            if (myLastLocation != null)
            {
                myPosition = new LatLng(myLastLocation.Latitude, myLastLocation.Longitude);
                map.AnimateCamera(CameraUpdateFactory.NewLatLngZoom(myPosition, 20));
            }
        }
    }
}