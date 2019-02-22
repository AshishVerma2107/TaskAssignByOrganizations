﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin;
using TaskAppWithLogin.Models;
using Object = Java.Lang.Object;

using static Android.App.ActionBar;
using TaskAppWithLogin.Adapter;
using TaskAppWithLogin.Fragments;
using Android.Support.V4.App;

namespace TaskAppWithLogin.Adapter
{
    public class TaskOutboxAdapter : RecyclerView.Adapter,IFilterable
    {
       public List<TaskOutboxModel> Mitems;
        public List<TaskOutboxModel> AllItem;
        Context context;
        RecyclerView mrecycle;
        TextView task, description, dead_line, mark_to, task_status, task_created_by, task_creation_date;
        LinearLayout linear;
        TextView tv;
        FragmentManager fragment;

        public static long ques, ans, correct;
        //List<AnswerMasterModel> answer_model = new List<AnswerMasterModel>();


        public TaskOutboxAdapter(Context context, List<TaskOutboxModel> Mitems, RecyclerView recyler, FragmentManager fm)
        {
            this.Mitems = Mitems;
            this.AllItem = Mitems;
            this.context = context;
            NotifyDataSetChanged();
            mrecycle = recyler;
            fragment = fm;
            //radioButton = btn;
        }
        public class MyView : RecyclerView.ViewHolder
        {
            public View mainview
            {
                get;
                set;
            }
            public TextView Task
            {
                get;
                set;
            }
            public TextView Description
            {
                get;
                set;
            }
            public TextView Deadline_date
            {
                get;
                set;
            }

            public TextView Mark_to
            {
                get;
                set;
            }
            public TextView Task_status
            {
                get;
                set;
            }
            public TextView Task_created_by
            {
                get;
                set;
            }
            public TextView Task_creation_date
            {
                get;
                set;
            }
            public LinearLayout Linear
            {
                get;
                set;
            }
            public TextView Text1
            {
                get;
                set;
            }

            public MyView(View view) : base(view)
            {
                mainview = view;
            }
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.taskoutboxdata, parent, false);
            task = listitem.FindViewById<TextView>(Resource.Id.tv1);
            description = listitem.FindViewById<TextView>(Resource.Id.tv2);
            dead_line = listitem.FindViewById<TextView>(Resource.Id.tv3);
            mark_to = listitem.FindViewById<TextView>(Resource.Id.tv4);
            task_status = listitem.FindViewById<TextView>(Resource.Id.tv5);
            task_created_by = listitem.FindViewById<TextView>(Resource.Id.tv6);
            task_creation_date = listitem.FindViewById<TextView>(Resource.Id.tv7);
            linear = listitem.FindViewById<LinearLayout>(Resource.Id.ll);
            Filter2 = new ChemicalFilter2(this);
            //TextView txtnumber = listitem.FindViewById<TextView>(Resource.Id.txtnumber);
            MyView view = new MyView(listitem)
            {
                Task = task,
                Description = description,
                Deadline_date = dead_line,
                Mark_to = mark_to,
                Task_status = task_status,
                Task_created_by = task_created_by,
                Task_creation_date = task_creation_date,
                Linear = linear,
                Text1 = tv,
            };
            return view;
        }
        public Filter Filter2 { get; private set; }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myholder = holder as MyView;
            myholder.Task.Text = Mitems[position].Task_name;
            myholder.Description.Text = Mitems[position].Description;
            myholder.Deadline_date.Text = Mitems[position].deadline_date;
            myholder.Mark_to.Text = Mitems[position].mark_to;
            myholder.Task_status.Text = Mitems[position].task_status;
            myholder.Task_created_by.Text = Mitems[position].Task_created_by;
            myholder.Task_created_by.Text = Mitems[position].Task_creation_date; myholder.mainview.Click += Mainview_Click;
        }
        private void Mainview_Click(object sender, EventArgs e)
        {
            int position = mrecycle.GetChildAdapterPosition((View)sender);

            string id = Mitems[position].Task_id;
            //string task_descrip = Mitems[position].description;
            //deadline = Mitems[position].deadlineDate;
            //string task_name = Mitems[position].task_name;
            //string mark_by = Mitems[position].task_mark_by;
            //string creation_date = Mitems[position].task_creation_date;
            //string created_by = Mitems[position].task_created_by;

            //ISharedPreferencesEditor editor = prefs.Edit();
            //editor.PutInt("position", position);
            //editor.Apply();

            ComplainceFrag_OutBox nextFrag = new ComplainceFrag_OutBox();
            FragmentTransaction ft = fragment.BeginTransaction();
            ft.Replace(Resource.Id.container, nextFrag);
            ft.AddToBackStack(null);
            ft.Commit();
            // fragment.BeginTransaction().Replace(Resource.Id.container, nextFrag).Commit();
            Bundle bundle = new Bundle();
            bundle.PutString("task_id", id);
            nextFrag.Arguments = bundle;
        }
        public void getList(int pos, MyView myholder)
        {

            tv = new TextView(context);

            linear.AddView(tv);

        }


        public override int ItemCount
        {
            get
            {
                return Mitems.Count;
            }
        }

        public Filter Filter => throw new NotImplementedException();
    }
}
class ChemicalFilter2 : Filter
{
    readonly TaskOutboxAdapter _adapter;

    public ChemicalFilter2(TaskOutboxAdapter adapter) : base()
    {
        _adapter = adapter;
    }

    protected override Filter.FilterResults PerformFiltering(Java.Lang.ICharSequence constraint)
    {
        FilterResults returnObj = new FilterResults();

        var matchList = new List<TaskOutboxModel>();


        var results = new List<TaskOutboxModel>();


        if (_adapter.AllItem == null)
            _adapter.AllItem = _adapter.Mitems;

        if (constraint == null) return returnObj;

        if (_adapter.AllItem != null && _adapter.AllItem.Any())
        {
            results.AddRange(
                _adapter.AllItem.Where(
                    chemical2 => chemical2.deadline_date.ToLower().Contains(constraint.ToString().ToLower())));
        }
        returnObj.Values = FromArray(results.Select(r => r.ToJavaObject()).ToArray());
        returnObj.Count = results.Count;

        constraint.Dispose();

        return returnObj;
    }

    protected override void PublishResults(Java.Lang.ICharSequence constraint, Filter.FilterResults results)
    {
        using (var values = results.Values)
            _adapter.Mitems = values.ToArray<Object>()
                .Select(r => r.ToNetObject<TaskOutboxModel>()).ToList();

        _adapter.NotifyDataSetChanged();
    }
}