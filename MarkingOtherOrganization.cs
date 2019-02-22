using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Newtonsoft.Json;
using TaskAppWithLogin.Constants;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Fragments
{
    public class MarkingOtherOrganization : Fragment
    {
        Context context;
        //  ProgressBar progress;
        ServiceHelper restservice;
        List<MarkingListModel> markinglist;
        DbHelper db;
        Geolocation geo;
        string location;
        public MarkingOtherOrganization(Context context)
        {
            this.context = context;
        }
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            geo = new Geolocation();
            restservice = new ServiceHelper();
            location = geo.GetGeoLocation(Activity);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.marking_other_org,null);
            return view;
        }
        public async Task GetOrganizationDetail()
        {

        }
        public async Task getData()
        {
            //progress = new Android.App.ProgressDialog(Activity);
            //progress.Indeterminate = true;
            //progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            //progress.SetCancelable(false);
            //progress.SetMessage("Please wait...");
            //progress.Show();

            dynamic value = new ExpandoObject();
            value.OrgId = "1";

            string json = JsonConvert.SerializeObject(value);
            try
            {
                string item = await restservice.MarkingList(Activity, json, location).ConfigureAwait(false);
                markinglist = JsonConvert.DeserializeObject<List<MarkingListModel>>(item);
                db.InsertMarkingList(markinglist);

               // progress.Dismiss();
            }
            catch (Exception ex)
            {
               // progress.Dismiss();
            }
        }
        }
}