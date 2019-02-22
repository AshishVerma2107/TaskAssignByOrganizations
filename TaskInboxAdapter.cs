
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin;
using TaskAppWithLogin.Adapter;
using TaskAppWithLogin.Fragments;
using TaskAppWithLogin.Models;
using static Android.App.ActionBar;
using Object = Java.Lang.Object;

namespace TaskAppWithLogin.Adapter
{
    public class TaskInboxAdapter : RecyclerView.Adapter,IFilterable
    {
        public List<TaskInboxModel> Mitems;
        public List<TaskInboxModel> AllItem;
        Context context;
        RecyclerView mrecycle;
        TextView task, description, dead_line, mark_by, timeleft;
        LinearLayout linear;
        TextView tv;
        ISharedPreferences prefs;
        public static long ques, ans, correct;
        FragmentManager fragment;
        //List<AnswerMasterModel> answer_model = new List<AnswerMasterModel>();


        public TaskInboxAdapter(Context context, List<TaskInboxModel> Mitems, RecyclerView recyler, FragmentManager fm )
        {
            this.Mitems = Mitems;
            this.AllItem = Mitems;
            this.context = context;
            NotifyDataSetChanged();
            mrecycle = recyler;
            prefs = PreferenceManager.GetDefaultSharedPreferences(context);
            this.fragment = fm;
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

            public TextView Mark_by
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
            View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.taskinboxdata, parent, false);
            task = listitem.FindViewById<TextView>(Resource.Id.tv1);
            description = listitem.FindViewById<TextView>(Resource.Id.tv2);
            dead_line = listitem.FindViewById<TextView>(Resource.Id.tv3);
            mark_by = listitem.FindViewById<TextView>(Resource.Id.tv4);
            //timeleft = listitem.FindViewById<TextView>(Resource.Id.tv5);
            linear = listitem.FindViewById<LinearLayout>(Resource.Id.ll);
            Filter1 = new ChemicalFilter1(this);
            //TimeSpan span = (TaskInboxFrag.deadline - DateTime.Now);
            //String.Format("{0} days, {1} hours, {2} minutes, {3} seconds",

            //span.Days, span.Hours, span.Minutes, span.Seconds);
            //timeleft.Text = span.ToString();
            //TextView txtnumber = listitem.FindViewById<TextView>(Resource.Id.txtnumber);
            MyView view = new MyView(listitem)
            {
                Task = task,
                Description = description,
                Deadline_date = dead_line,
                Mark_by = mark_by,
                Linear = linear,
                Text1 = tv,
            };
            return view;
        }
        public Filter Filter1 { get; private set; }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            MyView myholder = holder as MyView;
            myholder.Task.Text = Mitems[position].task_name;
            myholder.Description.Text = Mitems[position].description;
            myholder.Deadline_date.Text = Mitems[position].deadlineDate.ToString();
            //myholder.Mark_by.Text = Mitems[position].mark_to;
            // myholder.mainview.Click += Mainview_Click;
            var local = new LocalOnClickListener();
            myholder.Linear.SetOnClickListener(local);
            local.HandleOnClick = () =>
            {
                //int position = mrecycle.GetChildAdapterPosition((View)sender);

                string id = Mitems[position].task_id;
                string task_descrip = Mitems[position].description;
                DateTime deadline = Mitems[position].deadlineDate;
                string task_name = Mitems[position].task_name;
                string mark_by = Mitems[position].task_mark_by;
                string creation_date = Mitems[position].task_creation_date;
                string created_by = Mitems[position].task_created_by;
                DateTime current = DateTime.Now;

                TimeSpan span = deadline - current;
                String.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
                span.Days, span.Hours, span.Minutes, span.Seconds);
                //timeleft.Text = span.ToString();

                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutInt("position", position);
                editor.Apply();

                ComplainceFrag nextFrag = new ComplainceFrag();
                FragmentTransaction ft = fragment.BeginTransaction();
                ft.Replace(Resource.Id.container, nextFrag);
                ft.AddToBackStack(null);
                ft.Commit();
                // fragment.BeginTransaction().Replace(Resource.Id.container, nextFrag).Commit();
                Bundle bundle = new Bundle();
                bundle.PutString("task_id", id);
                nextFrag.Arguments = bundle;
            };
        }
        //var local = new LocalOnClickListener();
        //holder.View.SetOnClickListener(local);
        //    local.HandleOnClick = () =>
        //    {
        //        VideoFragment nextFrag = new VideoFragment();
        //FragmentTransaction ft = fragment.BeginTransaction();
        //ft.Replace(Resource.Id.container, nextFrag);
        //        ft.AddToBackStack(null);
        //        ft.Commit();
        //        // Fragment.BeginTransaction().Replace(Resource.Id.container, nextFrag).Commit();
        //        //FragmentTransaction ft = Fragment.PopBackStack();
        //        //   Fragment.PopBackStack();
        //        Bundle bundle = new Bundle();
        //bundle.PutString("Path", myList[position].Path);
        //        nextFrag.Arguments = bundle;
        //    };
    //private void Mainview_Click(object sender, EventArgs e)
    //    {
    //        int position = mrecycle.GetChildAdapterPosition((View)sender);

    //        string id = Mitems[position].task_id;
    //        string task_descrip = Mitems[position].description;
    //        DateTime deadline = Mitems[position].deadlineDate;
    //        string task_name = Mitems[position].task_name;
    //        string mark_by = Mitems[position].task_mark_by;
    //        string creation_date = Mitems[position].task_creation_date;
    //        string created_by = Mitems[position].task_created_by;
    //        DateTime current = DateTime.Now;

    //        TimeSpan span = deadline - current;
    //        String.Format("{0} days, {1} hours, {2} minutes, {3} seconds",
    //        span.Days, span.Hours, span.Minutes, span.Seconds);
    //        //timeleft.Text = span.ToString();
 
    //        ISharedPreferencesEditor editor = prefs.Edit();
    //        editor.PutInt("position", position);
    //        editor.Apply();

            
    //        ComplainceFrag nextFrag = new ComplainceFrag();
    //        FragmentTransaction ft = fragment.BeginTransaction();
    //        ft.Replace(Resource.Id.container, nextFrag);
    //        ft.AddToBackStack(null);
    //        ft.Commit();
    //        // fragment.BeginTransaction().Replace(Resource.Id.container, nextFrag).Commit();
    //        Bundle bundle = new Bundle();
    //        bundle.PutString("task_id", id);
    //        nextFrag.Arguments = bundle;

    //        //Intent intent = new Intent(context, typeof( Activity_Compliance));
    //        //intent.PutExtra("task_id", id);
    //        ////intent.PutExtra("task_descrip", task_descrip);
    //        ////intent.PutExtra("deadline", deadline);
    //        ////intent.PutExtra("task_name", task_name);
    //        ////intent.PutExtra("mark_by", mark_by);
    //        ////intent.PutExtra("creation_date", creation_date);
    //        ////intent.PutExtra("created_by", created_by);
    //        //context.StartActivity(intent);
    //    }
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
class ChemicalFilter1 : Filter
{
    readonly TaskInboxAdapter _adapter;

    public ChemicalFilter1(TaskInboxAdapter adapter) : base()
    {
        _adapter = adapter;
    }

    protected override Filter.FilterResults PerformFiltering(Java.Lang.ICharSequence constraint)
    {
        FilterResults returnObj = new FilterResults();

        var matchList = new List<TaskInboxModel>();


        var results = new List<TaskInboxModel>();


        if (_adapter.AllItem == null)
            _adapter.AllItem = _adapter.Mitems;

        if (constraint == null) return returnObj;

        if (_adapter.AllItem != null && _adapter.AllItem.Any())
        {
            results.AddRange(
                _adapter.AllItem.Where(
                    chemical1 => chemical1.task_name.ToLower().Contains(constraint.ToString().ToLower())));
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
                .Select(r => r.ToNetObject<TaskInboxModel>()).ToList();

        _adapter.NotifyDataSetChanged();
    }
}