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
using SQLite;

namespace TaskAppWithLogin.Models
{
    public class InitialTaskModel
    {
        [PrimaryKey, AutoIncrement]
        public Int64 Id { get; set; }
        public string localtaskid { get; set; }
        public string taskname { get; set; }
        public string taskdescrip { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string DesignationId { get; set; }
        [Ignore]
        public List<TaskFileMapping_Model> taskFileMappings { get; set; }
    }
}