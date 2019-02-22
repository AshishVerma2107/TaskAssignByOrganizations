using System;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using System.Collections.Generic;
using Android.Content.PM;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using TaskAppWithLogin.Constants;
using TaskAppWithLogin.Fragments;
using Android.Gms.Common.Apis;
using Android.Gms.Location;

namespace TaskAppWithLogin
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class LoginMain_Activity : AppCompatActivity 
    {
        Context context;
        Geolocation geo;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.loginmainlayout);

            SupportFragmentManager.BeginTransaction().Replace(Resource.Id.container_mainlogin, new LoginFrag()).Commit();
            DisplayLocationSettingsRequest();
            permissionmethodAsync();
            //if (permissionmethodAsync() == true)
            //{

            //}
            //else
            //{
            //    permissionmethodAsync();
            //}
        }

        public async void permissionmethodAsync()
        {

        await  GetPermissionAsync();
            //return true;
        }
        private bool DisplayLocationSettingsRequest()
        {
            bool islocationOn = false;
            var googleApiClient = new GoogleApiClient.Builder(this).AddApi(LocationServices.API).Build();
            googleApiClient.Connect();

            var locationRequest = LocationRequest.Create();
            locationRequest.SetPriority(LocationRequest.PriorityHighAccuracy);
            locationRequest.SetInterval(10000);
            locationRequest.SetFastestInterval(10000 / 2);

            var builder = new LocationSettingsRequest.Builder().AddLocationRequest(locationRequest);
            builder.SetAlwaysShow(true);

            var result = LocationServices.SettingsApi.CheckLocationSettings(googleApiClient, builder.Build());
            result.SetResultCallback((LocationSettingsResult callback) =>
            {
                switch (callback.Status.StatusCode)
                {
                    case LocationSettingsStatusCodes.Success:
                        {
                            islocationOn = true;
                            break;
                        }
                    case LocationSettingsStatusCodes.ResolutionRequired:
                        {
                            try
                            {
                                callback.Status.StartResolutionForResult(this, 100);
                            }
                            catch (IntentSender.SendIntentException e)
                            {
                            }

                            break;
                        }
                    default:
                        {

                            StartActivity(new Intent(Android.Provider.Settings.ActionLocationSourceSettings));
                            break;
                        }
                }
            });
            return islocationOn;
        }

        private async Task GetPermissionAsync()
        {
            List<String> permissions = new List<String>();
            try
            {

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.AccessFineLocation);
                }
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.RecordAudio) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.RecordAudio);
                }
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.AccessCoarseLocation);
                }

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.Camera);
                }

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.ReadExternalStorage);
                }

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.WriteExternalStorage);
                }

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.CallPhone) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.CallPhone);
                }

                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadPhoneState) == Permission.Denied)
                {
                    permissions.Add(Manifest.Permission.ReadPhoneState);
                }



                if (permissions.Count > 0)
                {
                    ActivityCompat.RequestPermissions(this, permissions.ToArray(), 100);
                }
                if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == Permission.Granted)
                {
                    geo = new Geolocation();
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("Error", e.Message);
            }

        }

    }
}