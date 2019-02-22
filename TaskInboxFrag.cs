using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using TaskAppWithLogin.Adapter;
using TaskAppWithLogin.Constants;
using TaskAppWithLogin.Models;
using static Android.App.ActionBar;


namespace TaskAppWithLogin.Fragments
{
    public class TaskInboxFrag: Fragment
    {
        private RecyclerView recyclerview;
        RecyclerView.LayoutManager recyclerview_layoutmanger;
         public static TaskInboxAdapter recyclerview_adapter;
        ServiceHelper restService = new ServiceHelper();
        List<TaskInboxModel> taskdata = new List<TaskInboxModel>();
        List<TaskInboxModel> result = new List<TaskInboxModel>();
        public static DateTime deadline;
        DbHelper dbHelper = new DbHelper();
        RecyclerAdapter<TaskInboxModel> im_model;
        public static TaskInboxModel detail;
        Android.Widget.SearchView search;
        string u_id = "";
        string l_id = "";
        Geolocation geo;
        string geolocation;
        ISharedPreferences prefs;
        Android.App.ProgressDialog progress;
        LinearLayout linearLayout;
        List<TaskInboxModel> freq;
        InternetConnection con = new InternetConnection();
        List<TaskFileMapping_Model> listmapping2 = new List<TaskFileMapping_Model>();

        

        //public static List<SubmitModel> summarydata = new List<SubmitModel>();


        public override void OnCreate(Bundle savedInstanceState)
        {
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            im_model = new RecyclerAdapter<TaskInboxModel>();
            StrictMode.SetVmPolicy(builder.Build());
            StrictMode.ThreadPolicy.Builder builder1 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder1.Build());
            base.OnCreate(savedInstanceState);

           
        }

       

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.taskin_layout, null);
            recyclerview = view.FindViewById<RecyclerView>(Resource.Id.recyclerview1);
            linearLayout = view.FindViewById<LinearLayout>(Resource.Id.linearlayout1);
            prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);
            u_id = prefs.GetString("DesignationId", "");

            geo = new Geolocation();
            geolocation = geo.GetGeoLocation(Activity);

           
           

            HasOptionsMenu = true;
            //DividerItemDecoration itemDecor = new DividerItemDecoration(Activity, Orientation.Horizontal);
            //recyclerview.AddItemDecoration(itemDecor);
            DividerItemDecoration horizontalDecoration = new DividerItemDecoration(recyclerview.Context,
            DividerItemDecoration.Vertical);
            Drawable horizontalDivider = ContextCompat.GetDrawable(Activity, Resource.Drawable.divider);
            horizontalDecoration.SetDrawable(horizontalDivider);
            recyclerview.AddItemDecoration(horizontalDecoration);
            search = view.FindViewById<Android.Widget.SearchView>(Resource.Id.searchview);


            search.QueryTextChange += sv_QueryTextChange;

            //recyclerview.Click += (sender, e) =>
            //{
            //    //var position1 = result[e.Position];
            //    Intent intent = new Intent(Activity, typeof(complianceActivity));
            //    StartActivity(intent);

            //};

            // recyclerview_adapter.ItemClick
            //{

            //};
            checkInternet();
           // getData();
            return view;
        }
        void sv_QueryTextChange(object sender, Android.Widget.SearchView.QueryTextChangeEventArgs e)
        {
            //FILTER
            recyclerview_adapter.Filter1.InvokeFilter(e.NewText);
        }


        public override void OnResume()
        {
            base.OnResume();

            if (FilterByDate_Activity.FromDateGlobal!= null && FilterByDate_Activity.ToDateGlobal != null)
            {
                List<TaskInboxModel> orderlist2 = dbHelper.getDataByDate(FilterByDate_Activity.FromDateGlobal, FilterByDate_Activity.ToDateGlobal);

                recyclerview_layoutmanger = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
                recyclerview.SetLayoutManager(recyclerview_layoutmanger);
                recyclerview_adapter = new TaskInboxAdapter(Activity, orderlist2, recyclerview, FragmentManager);
                recyclerview.SetAdapter(recyclerview_adapter);
            }
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {

            // View view = item.ActionView;
            int id = item.ItemId;

            if (id == Resource.Id.filtr)
            {

                Intent intent = new Intent(Activity, typeof(FilterByDate_Activity));

                StartActivity(intent);
            }
            else if (id == Resource.Id.namewise)
            {
             

                 List<TaskInboxModel> orderlist = new List<TaskInboxModel>(freq.OrderBy(x => x.task_name).ToList());

                recyclerview_layoutmanger = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
                recyclerview.SetLayoutManager(recyclerview_layoutmanger);
                recyclerview_adapter = new TaskInboxAdapter(Activity, orderlist, recyclerview, FragmentManager);
                recyclerview.SetAdapter(recyclerview_adapter);

            }

            else if (id == Resource.Id.creationdatewise)
            {


                List<TaskInboxModel> orderlist = new List<TaskInboxModel>(freq.OrderBy(x => x.deadlineDate).ToList());

                recyclerview_layoutmanger = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
                recyclerview.SetLayoutManager(recyclerview_layoutmanger);
                recyclerview_adapter = new TaskInboxAdapter(Activity, orderlist, recyclerview, FragmentManager);
                recyclerview.SetAdapter(recyclerview_adapter);

            }


            else if (id == Resource.Id.datewiseDESC)
            {

                //  Compare(deadline, deadline);



                  List<TaskInboxModel> orderlist = new List<TaskInboxModel>(freq.OrderByDescending(x => x.deadlineDate).ToList());


              




                recyclerview_layoutmanger = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
                recyclerview.SetLayoutManager(recyclerview_layoutmanger);
                recyclerview_adapter = new TaskInboxAdapter(Activity, orderlist, recyclerview, FragmentManager);
                recyclerview.SetAdapter(recyclerview_adapter);

            }



            //else if (id == Resource.Id.order)
            //{

            //    RegisterForContextMenu(view);

            //    //Android.Widget.PopupMenu menu = new Android.Widget.PopupMenu(Activity, View);
            //    // menu.Inflate(Resource.Menu.popup_menu);


            //    // menu.Show();


            //    //  List<TaskInboxModel> orderlist = new List<TaskInboxModel>(freq.OrderBy(x => x.task_name).ToList());
            //}

            return base.OnOptionsItemSelected(item);

        }

        

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            base.OnCreateOptionsMenu(menu, inflater);
             inflater.Inflate(Resource.Menu.menu_main, menu);

            //inflater.Inflate(Resource.Menu.options_menu, menu);

            menu.FindItem(Resource.Id.filtr).SetVisible(true);
            menu.FindItem(Resource.Id.order).SetVisible(true);

            var item = menu.FindItem(Resource.Id.filtr);
            IMenuItem item1 = menu.FindItem(Resource.Id.order);

            //ImageButton locButton = (ImageButton)menu.FindItem(Resource.Id.order).ActionView;

            //item1.Click += delegate
            //{
            //    RegisterForContextMenu(locButton);
            //};



        }



        public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
        {
            base.OnCreateContextMenu(menu, v, menuInfo);

            MenuInflater menuInflater = new MenuInflater(Activity);
            menuInflater.Inflate(Resource.Menu.popup_menu, menu);

            //menu.FindItem(Resource.Id.filtr).SetVisible(true);
           // menu.FindItem(Resource.Id.order).SetVisible(true);
        }

        public override bool OnContextItemSelected(IMenuItem item)
        {
            base.OnContextItemSelected(item);


            View view = item.ActionView;
            switch (item.ItemId)
            {
                case Resource.Id.action_setting1:
                    {
                        // this.textView.Text = "Add option selected";
                        break;
                    }
                case Resource.Id.action_settings2:
                    {
                        RegisterForContextMenu(view);
                        break;
                    }
                default:
                    break;
            }

            return true;
        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    base.OnOptionsItemSelected(item);

        //    switch (item.ItemId)
        //    {
        //        case Resource.Id.action_setting1:
        //            break;

        //        case Resource.Id.action_settings2:
        //            {
        //                // Finish();
        //                //StartActivity(typeof(ListManagerActivity));
        //                break;
        //            }
        //        default:
        //            break;
        //    }

        //    return true;
        //}

       

        public async Task getData()
        {
            progress = new Android.App.ProgressDialog(Activity);
            progress.Indeterminate = true;
            progress.SetProgressStyle(Android.App.ProgressDialogStyle.Spinner);
            progress.SetCancelable(false);
            progress.SetMessage("Please wait...");
            progress.Show();
            dynamic value = new ExpandoObject();
            value.UserId = u_id;

            string json = JsonConvert.SerializeObject(value);
            try
            {
                JsonValue item = await restService.TaskInbox(Activity, json, geolocation);
                freq = JsonConvert.DeserializeObject<List<TaskInboxModel>>(item);
                dbHelper.insertdatainbox(freq);
                progress.Dismiss();
                //for (int i = 0; i < freq.Count; i++)
                //{

                //    detail = new TaskInboxModel
                //    {
                //        TaskPercentage = freq[i].TaskPercentage,
                //        task_id = freq[i].task_id,
                //        task_name = freq[i].task_name,
                //        description = freq[i].description,
                //      //  deadline_date = freq[i].deadline_date,
                //        mark_to = freq[i].mark_to,
                //        taskStatus = freq[i].taskStatus,
                //        task_created_by = freq[i].task_created_by,
                //        task_creation_date = freq[i].task_creation_date,
                //        task_mark_by = freq[i].task_mark_by,
                //        MarkingDate = freq[i].MarkingDate,
                //        task_marking_type = freq[i].task_marking_type

                //    };
                //   // deadline = Convert.ToDateTime(detail.deadline_date);
                //  //  im_model.Add(detail);
                //}
                if (freq.Count != 0)
                {
                    recyclerview_layoutmanger = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
                    recyclerview.SetLayoutManager(recyclerview_layoutmanger);
                    recyclerview_adapter = new TaskInboxAdapter(Activity, freq, recyclerview, FragmentManager);
                    recyclerview.SetAdapter(recyclerview_adapter);
                }

                else
                {
                    LayoutParams lparams = new LayoutParams(LayoutParams.MatchParent, LayoutParams.WrapContent);
                    TextView textView = new TextView(Activity);
                    textView.LayoutParameters = lparams;
                    lparams.Gravity = GravityFlags.Center;
                    textView.Text = "Oops ! You haven't assigned any task yet";
                    linearLayout.AddView(textView);
                }
                progress.Dismiss();

            }
            catch (Exception ex)
            {
                progress.Dismiss();
            }

        }

        public void checkInternet()
        {
            Boolean connectivity = con.connectivity();
            if (connectivity)
            {
                getData();
            }
            else
            {
                taskdata = dbHelper.GetTaskInbox();
                if (taskdata.Count != 0)
                {
                    recyclerview_layoutmanger = new LinearLayoutManager(Activity, LinearLayoutManager.Vertical, false);
                    recyclerview.SetLayoutManager(recyclerview_layoutmanger);
                    recyclerview_adapter = new TaskInboxAdapter(Activity, taskdata, recyclerview, FragmentManager);
                    recyclerview.SetAdapter(recyclerview_adapter);
                }

                else
                {
                    LayoutParams lparams = new LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);
                    TextView textView = new TextView(Activity);
                    textView.LayoutParameters = lparams;
                    textView.Text = "Oops ! You haven't assigned any task yet";
                    linearLayout.AddView(textView);
                }

            }

        }

        //public override bool OnOptionsItemSelected(IMenuItem item)
        //{
        //    int id = item.ItemId;

        //    if (id == Resource.Id.filtr)
        //    {

        //        return true;
        //    }

        //    return base.OnOptionsItemSelected(item);

        //}

        //public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        //{
        //    base.OnCreateOptionsMenu(menu, inflater);
        //    inflater.Inflate(Resource.Menu.menu_main, menu);
        //    menu.FindItem(Resource.Id.filtr).SetVisible(true);
        //    menu.FindItem(Resource.Id.order).SetVisible(true);

        //    var item = menu.FindItem(Resource.Id.filtr);
        //    var item1 = menu.FindItem(Resource.Id.order);


        //}
    }
}