﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin.Constants;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Fragments
{
    public class Save_Task_Ref_Fragment : Fragment
    {
        List<InitialTaskModel> tasklist;
        List<InitialTaskModel> task_id_list;
        DbHelper dB;
        string taskid,temp_task_id;
        LinearLayout Linear1, Linear2, Linear3;
        TextView taskname, taskdescription, taskdeadlinedate, taskdeadlinetime;
        int pos;
        Button assignbtn;
        string designationid;
        ISharedPreferences prefs;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            tasklist = new List<InitialTaskModel>();
            task_id_list = new List<InitialTaskModel>();
            dB = new DbHelper();
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.save_task_reference_layout,null);
            taskname = view.FindViewById<TextView>(Resource.Id.taskname_saved);
            taskdescription = view.FindViewById<TextView>(Resource.Id.taskdescription_saved);
            taskdeadlinedate = view.FindViewById<TextView>(Resource.Id.taskdeadlinedate_saved);
            taskdeadlinetime = view.FindViewById<TextView>(Resource.Id.taskdeadlinetime_saved);
            Linear1 = view.FindViewById<LinearLayout>(Resource.Id.linear1_saved);
            Linear2 = view.FindViewById<LinearLayout>(Resource.Id.linear2_saved);
            Linear3 = view.FindViewById<LinearLayout>(Resource.Id.linear3_saved);
            assignbtn = view.FindViewById<Button>(Resource.Id.assignbtn);
            // tasklist = dB.GetFileMappingModel(taskid);
             pos  = Arguments.GetInt("pos");
            prefs = PreferenceManager.GetDefaultSharedPreferences(Activity);
            designationid = prefs.GetString("DesignationId", "");
            
            task_id_list = SavedTaskFrag.initialTasks;
            temp_task_id = task_id_list[pos].localtaskid;
            tasklist = dB.GetinitialTasks(designationid, temp_task_id);
            referencedata();
            assignbtn.Click += Assignbtn_Click;
            return view;
        }

        private void Assignbtn_Click(object sender, EventArgs e)
        {
            Fragment frag = new Assign();
            FragmentManager.BeginTransaction().Replace(Resource.Id.container, frag).Commit();
        }

        public void referencedata()
        {
                taskname.Text = tasklist[pos].taskname;
                taskdescription.Text = tasklist[pos].taskdescrip;
                taskdeadlinedate.Text = tasklist[pos].date;
                taskdeadlinetime.Text = tasklist[pos].time;

            for (int i = 0; i < tasklist[pos].taskFileMappings.Count; i++)
            {
                if (tasklist[pos].taskFileMappings[i].FileType.Equals("Image"))
                {
                    ImageView img = new ImageView(Activity);

                    Bitmap bitmap = BitmapFactory.DecodeFile(tasklist[pos].taskFileMappings[i].Path);

                    img.LayoutParameters = new LinearLayout.LayoutParams(200, 250);

                    img.SetX(10);
                    img.SetY(10);
                    //img.SetImageResource(Resource.Drawable.videofile);
                    img.SetImageBitmap(bitmap);
                    Linear1.AddView(img);
                }


                if (tasklist[pos].taskFileMappings[i].FileType.Equals("Video"))
                {
                    ImageView img = new ImageView(Activity);

                    //Bitmap bitmap = BitmapFactory.DecodeFile(Mitems[i].taskFileMappings[i].localPath);

                    img.LayoutParameters = new LinearLayout.LayoutParams(200, 250);

                    img.SetX(10);
                    img.SetY(10);
                    img.SetImageResource(Resource.Drawable.videofile);
                    //img.SetImageBitmap(bitmap);
                    Linear2.AddView(img);
                }


                if (tasklist[pos].taskFileMappings[i].FileType.Equals("Audio"))
                {
                    ImageView img = new ImageView(Activity);

                    //Bitmap bitmap = BitmapFactory.DecodeFile(Mitems[i].taskFileMappings[i].localPath);

                    img.LayoutParameters = new LinearLayout.LayoutParams(200, 250);

                    img.SetX(10);
                    img.SetY(10);
                    img.SetImageResource(Resource.Drawable.audiofile);
                    //img.SetImageBitmap(bitmap);
                    Linear3.AddView(img);
                }
            }
        }
        }
    }
