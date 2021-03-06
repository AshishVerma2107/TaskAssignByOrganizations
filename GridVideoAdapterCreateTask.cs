﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Adapter
{
 public   class GridVideoAdapterCreateTask : BaseAdapter
    {
        public override int Count => myList.Count;
        private Context mContext;
        public static List<TaskFileMapping_Model> myList;



        public GridVideoAdapterCreateTask(Context c, List<TaskFileMapping_Model> mList)
        {
            mContext = c;
            myList = mList;
        }
        public override Java.Lang.Object GetItem(int position)
        {
            return position;
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var grid = convertView;
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);

            VideoViewHolder2 holder;
            if (grid == null)
            {
                holder = new VideoViewHolder2();
                grid = inflater.Inflate(Resource.Layout.Attachment_layout, null);
                var view = grid.FindViewById<ImageView>(Resource.Id.imageview_attach);
                grid.Tag = new VideoViewHolder2() { View = view, };
            }

            holder = (VideoViewHolder2)grid.Tag;

            holder.View.SetImageResource(Resource.Drawable.videofile);

            return grid;
        }
        public void setNewSelection(int position)
        {
            myList[position].Checked = 1;
            NotifyDataSetChanged();
        }



        public void removeSelection(int position)
        {
            myList[position].Checked = 0;
            NotifyDataSetChanged();

        }

    }

    public class VideoViewHolder2 : Java.Lang.Object
    {

        public ImageView View { get; set; }
    }
}