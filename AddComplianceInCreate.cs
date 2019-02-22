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
using Android.Widget;
using Newtonsoft.Json;
using TaskAppWithLogin.Adapter;
using TaskAppWithLogin.Constants;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Fragments
{
    public class AddComplianceInCreate : Fragment
    {
        RadioButton checkBox1, checkBox2;
        Spinner spinnertype, spinnerextension;
        string compliancetype;
        string max_num;
        int max_numbers;
        string filetype, file_format;
        Button Addtolist;
        public static ExpandableHeightGridView complianceGridview;
        EditText  max_number;
        ServiceHelper restservice;
        Geolocation geo;
        string geolocation;
        InternetConnection ic;
        GridForAttachmentCreateReference gridattachmentlist;
        public List<FileTypeModel> filetypelist;
        public List<FileExtension> fileExtensions;
        string file_type1;
        string file_extensions;
        string selecteditem;
        string selectedextensions;
      public static  List<ComplianceJoinTable> modelsaddcompliance;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            restservice = new ServiceHelper();
            geo = new Geolocation();
            ic = new InternetConnection();
            getfiletypemethodAsync().ConfigureAwait(false);
            //getextensionmethodAsync().ConfigureAwait(false);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.addcompliance_createlayout, null);
            checkBox1 = view.FindViewById<RadioButton>(Resource.Id.mandatory);
            checkBox2 = view.FindViewById<RadioButton>(Resource.Id.not);
            spinnerextension = view.FindViewById<Spinner>(Resource.Id.spiner_format);
            spinnertype = view.FindViewById<Spinner>(Resource.Id.spinner_type);
            Addtolist = view.FindViewById<Button>(Resource.Id.btn_addtolist);
            complianceGridview = view.FindViewById<ExpandableHeightGridView>(Resource.Id.grid_compliance);
            max_number = view.FindViewById<EditText>(Resource.Id.maxnumberedit);
            filetypelist = new List<FileTypeModel>();
            modelsaddcompliance = new List<ComplianceJoinTable>();
            fileExtensions = new List<FileExtension>();
            checkBox1.Click += RadioButtonClick;
            checkBox2.Click += RadioButtonClick;
          
            Addtolist.Click += delegate
            {
                addtolistcompliance();
                selecteditem = null;
                selectedextensions = null;
                max_num = null;
                compliancetype = null;
                
            };
            
            return view;
        }

        private void Spinnertype_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

            selecteditem = string.Format("{0}", spinner.GetItemAtPosition(e.Position));
            getextensionmethodAsync(selecteditem);
        }

        private void Spinnertype_ItemSelected2(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;

             selectedextensions = string.Format("{0}", spinner.GetItemAtPosition(e.Position));

        }

        private void addtolistcompliance()
        {
            max_num = max_number.Text;
            max_numbers = Convert.ToInt32(max_num);
            ComplianceJoinTable addtolistcompliace = new ComplianceJoinTable();
            addtolistcompliace.compliance_type = compliancetype;
            addtolistcompliace.file_format = selectedextensions;
            addtolistcompliace.file_type = selecteditem;
            addtolistcompliace.max_numbers = max_numbers;
           
             modelsaddcompliance.Add(addtolistcompliace);
            gridattachmentlist = new GridForAttachmentCreateReference(Activity, modelsaddcompliance);
            complianceGridview.Adapter = gridattachmentlist;
            gridattachmentlist.NotifyDataSetChanged();
            complianceGridview.setExpanded(true);
           

        }

        private void RadioButtonClick(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            compliancetype = rb.Text;
            Toast.MakeText(Activity, rb.Text, ToastLength.Short).Show();
        }
        public async Task getextensionmethodAsync(string filetype)
        {
           
            if (ic.connectivity())
            {
                geolocation = geo.GetGeoLocation(Context);
                string file_extension = await restservice.FileExtension(Activity, filetype, geolocation);
                fileExtensions = JsonConvert.DeserializeObject<List<FileExtension>>(file_extension);
            }
            else
            {

            }
          
            spinnerextension.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinnertype_ItemSelected2);
            ArrayAdapter adapter1 = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, fileExtensions);
            spinnerextension.Adapter = adapter1;
        }
        public async Task getfiletypemethodAsync()
        {
            if (ic.connectivity())
            {
                geolocation = geo.GetGeoLocation(Context);
                string filetype = await restservice.FileTypelist(Activity, "", geolocation);
                filetypelist = JsonConvert.DeserializeObject<List<FileTypeModel>>(filetype);
            }
            else
            {

            }
         
            spinnertype.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(Spinnertype_ItemSelected);
            ArrayAdapter adapter = new ArrayAdapter(Activity, Android.Resource.Layout.SimpleListItem1, filetypelist);
            spinnertype.Adapter = adapter;
         


        }
    }
}