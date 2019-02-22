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
    class GridAudiocomplianceOutbox : BaseAdapter
    {
       // MediaPlayer player;

        private Context mContext;
        public static List<Task_UpoadCompliances> myList = new List<Task_UpoadCompliances>();



        public GridAudiocomplianceOutbox(Context c, List<Task_UpoadCompliances> mList)
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
        public override int Count => myList.Count;
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var grid = convertView;
            LayoutInflater inflater = (LayoutInflater)mContext.GetSystemService(Context.LayoutInflaterService);

            OutboxViewHolder3 holder;
            if (grid == null)
            {
                holder = new OutboxViewHolder3();
                grid = inflater.Inflate(Resource.Layout.Attachment_layout, null);
                var view = grid.FindViewById<ImageView>(Resource.Id.imageview_attach);
                grid.Tag = new OutboxViewHolder3() { View = view };
            }

            holder = (OutboxViewHolder3)grid.Tag;


            holder.View.SetImageResource(Resource.Drawable.audiofile);


            return grid;
        }



        public void setNewSelection(int position)
        {
            //myList[position].Checked = 1;
            NotifyDataSetChanged();
        }



        public void removeSelection(int position)
        {
           // myList[position].Checked = 0;
            NotifyDataSetChanged();

        }
    }

    public class OutboxViewHolder3 : Java.Lang.Object
    {

        public ImageView View { get; set; }
    }
}