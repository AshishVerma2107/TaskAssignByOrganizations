using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Adapter
{
    public class GridViewAdapter_Audio : BaseAdapter
    {
        MediaPlayer player;
        
        private Context mContext;
        public static List<Comp_AttachmentModel> myList = new List<Comp_AttachmentModel>();
       


        public GridViewAdapter_Audio(Context c, List<Comp_AttachmentModel> mList)
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

            ViewHolder3 holder;
            if (grid == null)
            {
                holder = new ViewHolder3();
                grid = inflater.Inflate(Resource.Layout.Attachment_layout, null);
                var view = grid.FindViewById<ImageView>(Resource.Id.imageview_attach);
                grid.Tag = new ViewHolder3() { View = view };
            }

            holder = (ViewHolder3)grid.Tag;
            
            
            holder.View.SetImageResource(Resource.Drawable.audiofile);
         
           
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

    public class ViewHolder3 : Java.Lang.Object
    {
       
        public ImageView View { get; set; }
    }
}