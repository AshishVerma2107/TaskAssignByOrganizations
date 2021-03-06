﻿using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Adapter
{
   public class GridViewAdapter_Image : BaseAdapter
    {
        public override int Count => myList.Count;
        private Context mContext;
        public static List<Comp_AttachmentModel> myList;



        public GridViewAdapter_Image(Context c, List<Comp_AttachmentModel> mList)
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

            ViewHolder1 holder;
            if (grid == null)
            {
                holder = new ViewHolder1();
                grid = inflater.Inflate(Resource.Layout.Attachment_layout, null);
                var view = grid.FindViewById<ImageView>(Resource.Id.imageview_attach);
                grid.Tag = new ViewHolder1() { View = view };
            }

            holder = (ViewHolder1)grid.Tag;

            Bitmap bitmap = BitmapFactory.DecodeFile(myList[position].localPath);
            holder.View.SetImageBitmap(bitmap);

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

    public class ViewHolder1 : Java.Lang.Object
    {
        
        public ImageView View { get; set; }
    }
}