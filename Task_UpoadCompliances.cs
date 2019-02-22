using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace TaskAppWithLogin.Models
{
    public class Task_UpoadCompliances
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }
        public string taskId { get; set; }
        public string file_type { get; set; }
        public string file_format { get; set; }
        public string max_numbers { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string Path { get; set; }
        public string GeoLocation { get; set; }

    }
}