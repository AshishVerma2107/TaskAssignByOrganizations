using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;

namespace TaskAppWithLogin.Fragments
{
    public class MainFrag: Fragment
    {
        public override void OnCreate(Bundle savedInstanceState)
        {
            StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
            StrictMode.SetVmPolicy(builder.Build());
            StrictMode.ThreadPolicy.Builder builder1 = new StrictMode.ThreadPolicy.Builder().PermitAll();
            StrictMode.SetThreadPolicy(builder1.Build());
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {

            View view = inflater.Inflate(Resource.Layout.mainfrag_layout, null);
            var bottomBar = view.FindViewById<BottomNavigationView>(Resource.Id.navigation);
            FragmentManager.BeginTransaction()
                .Replace(Resource.Id.container_mainfrag, new CreateTaskFrag())
                .Commit();
            bottomBar.NavigationItemSelected += (s, a) =>
            {
                LoadFragment(a.Item.ItemId);
            };
            FragmentManager.BeginTransaction().Replace(Resource.Id.container_mainfrag , new CreateTaskFrag()).Commit();

            return view;
        }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {
            LoadFragment(e.Item.ItemId);
        }

        void LoadFragment(int id)
        {
            Android.Support.V4.App.Fragment fragment = null;
            switch (id)
            {
                case Resource.Id.navigation_home:
                    fragment = new CreateTaskFrag();
                    break;
                //case Resource.Id.menuItem2:
                //    fragment = new Frequent();
                //    break;
                case Resource.Id.navigation_dashboard:
                    fragment = new TaskInboxFrag();
                    break;
                case Resource.Id.navigation_notifications:
                    fragment = new TaskOutboxFrag();
                    break;
            }
            if (fragment == null)
                return;

            FragmentManager.BeginTransaction()
               .Replace(Resource.Id.container_mainfrag, fragment)
               .Commit();
        }

    
    }
}