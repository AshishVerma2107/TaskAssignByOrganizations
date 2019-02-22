using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Adapter
{
    class GridImagecomplianceoutbox : BaseAdapter
    {
        public override int Count => myList.Count;
        private Context mContext;
        public static List<Task_UpoadCompliances> myList;



        public GridImagecomplianceoutbox(Context c, List<Task_UpoadCompliances> mList)
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

            OutViewHolder1 holder;
            if (grid == null)
            {
                holder = new OutViewHolder1();
                grid = inflater.Inflate(Resource.Layout.Attachment_layout, null);
                var view = grid.FindViewById<ImageView>(Resource.Id.imageview_attach);
                grid.Tag = new OutViewHolder1() { View = view };
            }

            holder = (OutViewHolder1)grid.Tag;

            Bitmap bitmap = BitmapFactory.DecodeFile(myList[position].Path);
            holder.View.SetImageBitmap(bitmap);

            return grid;
        }

        public void setNewSelection(int position)
        {
           // myList[position].Checked = 1;
            NotifyDataSetChanged();
        }



        public void removeSelection(int position)
        {
          //  myList[position].Checked = 0;
            NotifyDataSetChanged();

        }
    }

    public class OutViewHolder1 : Java.Lang.Object
    {

        public ImageView View { get; set; }
    }
}