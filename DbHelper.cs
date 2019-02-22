using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Util;
using SQLite;
using TaskAppWithLogin.Models;

namespace TaskAppWithLogin.Constants
{
    public class DbHelper
    {
        SQLiteConnection db;
        //public static List<ImageModel> latLnglist = new List<ImageModel>();
        public DbHelper()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "taskapp.db3");
            db = new SQLiteConnection(dbPath);
            try
            {
                db.CreateTable<LoginModel>();
                db.CreateTable<ComplianceModel>();
                db.CreateTable<CreateTaskModel>();
                db.CreateTable<ComplianceJoinTable>();
                db.CreateTable<CommunicationModel>();
                db.CreateTable<Shapes>();
                db.CreateTable<ComplianceLatlngPath>();
                db.CreateTable<MarkingListModel>();
                db.CreateTable<FrequentList>();
                db.CreateTable<Comp_AttachmentModel>();
                db.CreateTable<TaskInboxModel>();
                db.CreateTable<TaskOutboxModel>();
                db.CreateTable<TaskFileMapping_Model>();
                db.CreateTable<CreateTaskLicenceIdReturnModel>();
                db.CreateTable<TaskFilemappingModel2>();
                db.CreateTable<Task_UpoadCompliances>();
                db.CreateTable<InitialTaskModel>();
            }
            catch (Exception e)
            {

            }
        }

        public void insertCreateTasklicenceid(string lid)
        {
            int i = 0;
            try
            {
                CreateTaskLicenceIdReturnModel model = new CreateTaskLicenceIdReturnModel();
                //model.taskid
                model.licenceId = lid;
                i = db.Insert(model);

            }
            catch (Android.Database.Sqlite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);

            }
        }
        public List<CreateTaskLicenceIdReturnModel> Getlicneceidcreate()
        {
            try
            {
                List<CreateTaskLicenceIdReturnModel> data1 = db.Query<CreateTaskLicenceIdReturnModel>("SELECT * from [CreateTaskLicenceIdReturnModel] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void taskuploadinsert(List<Task_UpoadCompliances>taskuploaded)
        {
            int i = 0;
            try
            {
               i= db.Insert(taskuploaded);
            }catch(Exception e)
            {

            }
           
        }
        public List<Task_UpoadCompliances> gettaskuploaded(string taskid)
        {
            try
            {
                List<Task_UpoadCompliances> data1 = db.Query<Task_UpoadCompliances>("SELECT * from [Task_UpoadCompliances] where taskId ");
                return data1;
            }
            catch (Exception ex) 
            {
                return null; 
            }
        }

        public void insertIntoTable(LoginModel model)
        {
            int i = 0;
            try
            {
                i = db.Insert(model);

            }
            catch (Android.Database.Sqlite.SQLiteException ex)
            {
                Log.Info("SQLiteEx", ex.Message);

            }
        }

        public int Insert_InitialTask1(string taskname, string taskdescrip, string date, string time)
        {
            int i = 0;
            try
            {

                InitialTaskModel tbl = new InitialTaskModel();
                tbl.taskname = taskname;
                tbl.taskdescrip = taskdescrip;
                tbl.date = date;
                tbl.time = time;
                i = db.Insert(tbl);


                return i;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public void InsertMarkingList(List<MarkingListModel> markingLists)
        {
            int i = 0;
            for (int j = 0; j <= markingLists.Count; j++)
            {
                try
                {
                    var data1 = db.Query<MarkingListModel>("SELECT * from [MarkingListModel] where [NPID]='" + markingLists[j].NPID + "'");
                    if (data1.Count <= 0)
                    {

                        MarkingListModel marking = new MarkingListModel();
                        marking.NPName = markingLists[j].NPName;
                        marking.DesignationId = markingLists[j].DesignationId;
                        marking.DesignationName = markingLists[j].DesignationName;
                        i = db.Insert(marking);

                    }
                }
                catch (Exception e)
                {

                }
            }
        }

            public List<MarkingListModel> GetMarkingList()
        {
            try
            {
                List<MarkingListModel> data1 = db.Query<MarkingListModel>("SELECT * from [MarkingListModel] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertFrequentList(List<FrequentList> frequentLists)
        {
            int i = 0;
            for (int j = 0; j <= frequentLists.Count; j++)
            {
                try
                {
                    var data1 = db.Query<FrequentList>("SELECT * from [FrequentList] where [NPID]='" + frequentLists[j].NPID + "'");
                    if (data1.Count <= 0)
                    {

                        FrequentList frequent = new FrequentList();
                        frequent.NPName = frequentLists[j].NPName;
                        frequent.DesignationId = frequentLists[j].DesignationId;
                        frequent.Mobile = frequentLists[j].Mobile;
                        frequent.PhotoPath = frequentLists[j].PhotoPath;

                        i = db.Insert(frequent);
                    }

                }
                catch (Exception e)
                {

                }
            }
        }

        public List<FrequentList> GetFrequentList()
        {
            try
            {
                List<FrequentList> data1 = db.Query<FrequentList>("SELECT * from [FrequentList] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void Insert_InitialTask(string taskname,string taskid, string taskdescrip, string date, string time, List<TaskFileMapping_Model> mapping_Models, string DesignationId)
        {
            int i = 0;
            try
            {

                InitialTaskModel tbl = new InitialTaskModel();
                tbl.taskname = taskname;
                tbl.localtaskid = taskid;
                tbl.taskdescrip = taskdescrip;
                tbl.date = date;
                tbl.time = time;
                tbl.taskFileMappings = mapping_Models;
                tbl.DesignationId = DesignationId;
                i = db.Insert(tbl);


                //return i;
            }
            catch (Exception e)
            {
                //return 0;
            }
        }

        public void Insert_InitialMapping(List<TaskFileMapping_Model> joinlist,string temp_id)
        {
            int i = 0;
            for (int j = 0; j < joinlist.Count; j++)
            {
                try
                {

                    TaskFileMapping_Model joinTable = new TaskFileMapping_Model();
                    joinTable.FileName = joinlist[j].FileName;
                    joinTable.fileId = joinlist[j].fileId;
                    joinTable.FileType = joinlist[j].FileType;
                    joinTable.FileSize = joinlist[j].FileSize;
                    joinTable.Path = joinlist[j].Path;
                    joinTable.localtaskId = temp_id;
                    joinTable.localPath = joinlist[j].Path;
                    joinTable.Checked = joinlist[j].Checked;
                    i = db.Insert(joinTable);

                }
                catch (Exception e)
                {

                }
            }

        }
        public List<InitialTaskModel> GetinitialTaskssavelist(string desig)
        {
            try
            {
                List<InitialTaskModel> data1 = db.Query<InitialTaskModel>("SELECT * from [InitialTaskModel] where [DesignationId]='" + desig + "'");
                List<InitialTaskModel> da = new List<InitialTaskModel>();
                for (int i = 0; i < data1.Count; i++)
                {
                    InitialTaskModel compliance = new InitialTaskModel();
                    compliance.taskname = data1[i].taskname;
                    compliance.taskdescrip = data1[i].taskdescrip;
                    compliance.date = data1[i].date;
                    compliance.time = data1[i].time;
                    compliance.localtaskid = data1[i].localtaskid;
                    compliance.DesignationId = data1[i].DesignationId;
                   // compliance.taskFileMappings = GetFileMappingModel(temp);

                    da.Add(compliance);
                }
                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<InitialTaskModel> GetinitialTasks(string desig,string temp)
        {
            try
            {
                List<InitialTaskModel> data1 = db.Query<InitialTaskModel>("SELECT * from [InitialTaskModel] where [DesignationId]='" + desig + "'");
                List<InitialTaskModel> da = new List<InitialTaskModel>();
                for (int i = 0; i < data1.Count; i++)
                {
                    InitialTaskModel compliance = new InitialTaskModel();
                    compliance.taskname = data1[i].taskname;
                    compliance.taskdescrip = data1[i].taskdescrip;
                    compliance.date = data1[i].date;
                    compliance.time = data1[i].time;
                    compliance.localtaskid = data1[i].localtaskid;
                    compliance.DesignationId = data1[i].DesignationId;
                   compliance.taskFileMappings = GetFileMappingModel(temp);

                    da.Add(compliance);
                }
                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<TaskFileMapping_Model> GetFileMappingModel(string tempdata)
        {
            try
            {
                List<TaskFileMapping_Model> data1 = db.Query<TaskFileMapping_Model>("SELECT * from [TaskFileMapping_Model] where [localtaskId]='" + tempdata + "'" );

                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ComplianceModel> GetCompliance()
        {
            try
            {
                List<ComplianceModel> data1 = db.Query<ComplianceModel>("SELECT * from [ComplianceModel] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<LoginModel> getDetail()
        {
            try
            {
                List<LoginModel> data1 = db.Query<LoginModel>("SELECT * from [LoginModel] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void deleteTables()
        {
            int i =0;
            try
            {
                //var data1 = db.Query<LoginModel>("SELECT * from [LoginModel] ");
                //i = db.Delete(data1);

                db.DropTable<LoginModel>();
                
            }
            catch (Exception ex)
            {
                
            }
        }

        public int InsertCreateTaskData(string taskname, string taskdescription, string deadline,string through, string  markto, string status,List<TaskFileMapping_Model> filemapping)
        {
            int i = 0;
            try
            {
                CreateTaskModel createTask = new CreateTaskModel();
                createTask.taskname = taskname;
                createTask.taskdescrip = taskdescription;
                createTask.deadline = deadline;
                createTask.through = through;
                createTask.markto = markto;
                createTask.status = status;
                createTask.lstTaskFileMapping = filemapping;
                i = db.Insert(createTask);
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<CreateTaskModel> getCreateTaskData(string status)
        {
            try
            {
                List<CreateTaskModel> data1 = db.Query<CreateTaskModel>("SELECT * from [CreateTaskModel] where [status]='" + status + "'");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long updatetaskstatus(long id)
        {
            try
            {

                var data = db.Table<CreateTaskModel>();
                int idvalue = Convert.ToInt32(id);

                var data1 = (from values in data
                             where values.Id == idvalue
                             select values).FirstOrDefault();
                data1.status = "yes";
                long i = db.Update(data1);
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public void ComplianceInsert(string taskId, string markingtype, string taskstatus, string task_created_by, string taskcreationdate, string taskname, string taskdescription, string markingDate, string rownum, string meetingId, string deadlineDate, List<ComplianceJoinTable> lstAddedCompliances, string geolocation, string status, string shapes)
        {
            int j = 0;
            //for (int k = 0; k < comModel.Count; k++)
            //{
            try
            {

                ComplianceModel com = new ComplianceModel();

                com.task_id = taskId;
                //com.fileType =file_type;
                //com.fileName = FileName;
                //com.fileSize = Filesize;
                com.status = status;
                // com.markTo = markTo;
                // com.marking_Type = markingtype;
                com.taskStatus = taskstatus;
                com.task_created_by = task_created_by;
                com.task_creation_date = taskcreationdate;
                com.task_marking_type = markingtype;
                com.task_name = taskname;
                com.description = taskdescription;
                com.MarkingDate = markingDate;
                com.RowNo = rownum;
                com.Meeting_ID = meetingId;
                com.deadline_date = deadlineDate;
                com.lstAddedCompliance = lstAddedCompliances;
               // com.lstUploadedCompliance = lstuploadedcompliance;
                com.shapes = shapes;
                com.GeoLocation = geolocation;
                j = db.Insert(com);

            }
            catch (Exception e)
            {

            }


        }
        //public void ComplianceInsertforoutbox(string taskId, string markingtype, string taskstatus, string task_created_by, string taskcreationdate, string taskname, string taskdescription, string markingDate, string rownum, string meetingId, string deadlineDate, List<ComplianceJoinTable> lstAddedCompliances,List<Task_UpoadCompliances> lstuploadedcompliance string geolocation, string status, string shapes)
        //{
        //    int j = 0;
        //    //for (int k = 0; k < comModel.Count; k++)
        //    //{
        //    try
        //    {

        //        ComplianceModel com = new ComplianceModel();

        //        com.task_id = taskId;
        //        //com.fileType =file_type;
        //        //com.fileName = FileName;
        //        //com.fileSize = Filesize;
        //        com.status = status;
        //        // com.markTo = markTo;
        //        // com.marking_Type = markingtype;
        //        com.taskStatus = taskstatus;
        //        com.task_created_by = task_created_by;
        //        com.task_creation_date = taskcreationdate;
        //        com.task_marking_type = markingtype;
        //        com.task_name = taskname;
        //        com.description = taskdescription;
        //        com.MarkingDate = markingDate;
        //        com.RowNo = rownum;
        //        com.Meeting_ID = meetingId;
        //        com.deadline_date = deadlineDate;
        //        com.lstAddedCompliance = lstAddedCompliances;
        //         com.lstUploadedCompliance = lstuploadedcompliance;
        //        com.shapes = shapes;
        //        com.GeoLocation = geolocation;
        //        j = db.Insert(com);

        //    }
        //    catch (Exception e)
        //    {

        //    }


        //}
        public List<ComplianceModel> GetComplianceforoutbox(string type, string taskId)
        {
            try
            {
                List<ComplianceModel> data1 = db.Query<ComplianceModel>("SELECT * from [Compliance] where task_id ='" + taskId + "'");
                List<ComplianceModel> da = new List<ComplianceModel>();
                for (int i = 0; i < data1.Count; i++)
                {
                    ComplianceModel compliance = new ComplianceModel();
                    compliance.task_id = data1[i].task_id;
                    compliance.markTo = data1[i].markTo;
                    // compliance.markingType = data1[i].markingType;
                    compliance.taskStatus = data1[i].taskStatus;
                    compliance.task_created_by = data1[i].task_created_by;
                    compliance.task_creation_date = data1[i].task_creation_date;
                    compliance.task_marking_type = data1[i].task_marking_type;
                    compliance.task_name = data1[i].task_name;
                    compliance.description = data1[i].description;
                    compliance.MarkingDate = data1[i].MarkingDate;
                    compliance.RowNo = data1[i].RowNo;
                    compliance.Meeting_ID = data1[i].Meeting_ID;
                    compliance.deadline_date = data1[0].deadline_date;
                    compliance.lstAddedCompliance = GetComplianceJoinTable(taskId);
                    compliance.lstUploadedCompliance = gettaskuploaded(taskId);
                    da.Add(compliance);
                }
                // cursor= db.Query<Compliance>("SELECT * from [Compliance] where taskId ='" + taskId + "'");
                // List<Compliance> data1 = db.Query<Compliance>("SELECT * from Compliance from  join Compliance.taskId =ComplianceJoinTable.taskId");
                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void insertshapes(List<ComplianceShapes> shapes)
        {
            int i = 0;
            for (int j = 0; j <= shapes.Count; j++)
            {
                try
                {

                    ComplianceShapes complianceShapes = new ComplianceShapes();
                    complianceShapes.type = shapes[j].type;
                    complianceShapes.color = shapes[j].color;
                    complianceShapes.paths = shapes[j].paths;

                }
                catch (Exception e)
                {

                }
            }

        }
        public void insertshapepath(List<ComplianceShapePath> shapespath)
        {
            int i = 0;
            for (int j = 0; j <= shapespath.Count; j++)
            {
                try
                {

                    ComplianceShapePath complianceShapes = new ComplianceShapePath();
                    complianceShapes.path = shapespath[j].path;

                }
                catch (Exception e)
                {

                }
            }
        }
        public List<ComplianceLatlngPath> GetComplianceLatLngpath(string taskId)
        {
            try
            {
                List<ComplianceLatlngPath> data1 = db.Query<ComplianceLatlngPath>("SELECT * from [ComplianceLatlngPath] ");
                //List<ComplianceLatlngPath> cp = new List<ComplianceLatlngPath>();
                //for (int i = 0; i < data1.Count; i++)
                //{
                //    ComplianceLatlngPath shapePath = new ComplianceLatlngPath();
                //    shapePath.Id = data1[i].Id;
                //    //shapePath.paths = data1[i].paths;
                //    shapePath.lat = data1[i].lat;
                //    shapePath.lng = data1[i].lng;

                //}

                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ComplianceShapePath> GetComplianceShapespath(string taskId)
        {
            try
            {
                List<ComplianceShapePath> data1 = db.Query<ComplianceShapePath>("SELECT * from [ComplianceShapePath] ");
                List<ComplianceShapePath> cp = new List<ComplianceShapePath>();
                for (int i = 0; i < data1.Count; i++)
                {
                    ComplianceShapePath shapePath = new ComplianceShapePath();
                    shapePath.Id = data1[i].Id;
                    shapePath.path = data1[i].path;
                }

                return cp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ComplianceShapes> GetComplianceShapesTable(string taskId)
        {
            try
            {
                List<ComplianceShapes> data1 = db.Query<ComplianceShapes>("SELECT * from [ComplianceShapes] ");
                List<ComplianceShapes> ds = new List<ComplianceShapes>();
                for (int i = 0; i < data1.Count; i++)
                {
                    ComplianceShapes complianceShapes = new ComplianceShapes();
                    complianceShapes.Id = data1[i].Id;
                    complianceShapes.type = data1[i].type;
                    complianceShapes.color = data1[i].color;
                    complianceShapes.paths = data1[i].paths;
                    ds.Add(complianceShapes);
                }
                return ds;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertcompliancejoinTable(List<ComplianceJoinTable> joinlist,string status)
        {
            int i = 0;
            for (int j = 0; j <= joinlist.Count; j++)
            {
                try
                {

                    ComplianceJoinTable joinTable = new ComplianceJoinTable();
                    joinTable.compliance_type = joinlist[j].compliance_type;
                    joinTable.file_format = joinlist[j].file_format;
                    joinTable.file_type = joinlist[j].file_type;
                    joinTable.task_id = joinlist[j].task_id;
                    joinTable.max_numbers = joinlist[j].max_numbers;
                    joinTable.task_overview = joinlist[j].task_overview;
                    joinTable.Uploaded = joinlist[j].Uploaded;
                    joinTable.status = status;
                    i = db.Insert(joinTable);

                }
                catch (Exception e)
                {

                }
            }

        }
        public long updateComplianceStatus(long id)
        {
            try
            {

                var data = db.Table<ComplianceModel>();
                int idvalue = Convert.ToInt32(id);

                var data1 = (from values in data
                             where values.id == idvalue
                             select values).FirstOrDefault();
                data1.status = "yes";

                long i = db.Update(data1);
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public List<ComplianceModel> GetCompliance(string type, string taskId)
        {
            try
            {
                List<ComplianceModel> data1 = db.Query<ComplianceModel>("SELECT * from [Compliance] where task_id ='" + taskId + "'");
                List<ComplianceModel> da = new List<ComplianceModel>();
                for (int i = 0; i < data1.Count; i++)
                {
                    ComplianceModel compliance = new ComplianceModel();
                    compliance.task_id = data1[i].task_id;
                    compliance.markTo = data1[i].markTo;
                    // compliance.markingType = data1[i].markingType;
                    compliance.taskStatus = data1[i].taskStatus;
                    compliance.task_created_by = data1[i].task_created_by;
                    compliance.task_creation_date = data1[i].task_creation_date;
                    compliance.task_marking_type = data1[i].task_marking_type;
                    compliance.task_name = data1[i].task_name;
                    compliance.description = data1[i].description;
                    compliance.MarkingDate = data1[i].MarkingDate;
                    compliance.RowNo = data1[i].RowNo;
                    compliance.Meeting_ID = data1[i].Meeting_ID;
                    compliance.deadline_date = data1[0].deadline_date;
                    compliance.lstAddedCompliance = GetComplianceJoinTable(taskId);
                   // compliance.lstUploadedCompliance = gettaskuploaded(taskId);
                    da.Add(compliance);
                }
                // cursor= db.Query<Compliance>("SELECT * from [Compliance] where taskId ='" + taskId + "'");
                // List<Compliance> data1 = db.Query<Compliance>("SELECT * from Compliance from  join Compliance.taskId =ComplianceJoinTable.taskId");
                return da;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ComplianceJoinTable> GetComplianceJoinTable(string taskId)
        {
            try
            {
                List<ComplianceJoinTable> data1 = db.Query<ComplianceJoinTable>("SELECT * from [ComplianceJoinTable] where task_id ='" + taskId + "'");
 
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public void InsertCreatecomplianceAttachData(List<TaskFilemappingModel2> create_Attachment,string taskid)
        {
            int i = 0;
            try
            {
                for(int j=0; j < create_Attachment.Count; j++)
                {
                    //var data1 = db.Query<TaskFilemappingModel2>("SELECT * from [TaskFilemappingModel2] where [taskid]='" + taskid + "'");
                    //if (data1.Count <= 0)
                    //{
                        TaskFilemappingModel2 model2 = new TaskFilemappingModel2();
                        model2.taskId = taskid;
                        model2.FileName = create_Attachment[j].FileName;
                        model2.FileType = create_Attachment[j].FileType;
                        model2.FileSize = create_Attachment[j].FileSize;
                        model2.Path = create_Attachment[j].Path;


                        db.Insert(model2);
                   // }
                }
            }
            catch (Exception ex)
            {

            }
        }
        public List<TaskFilemappingModel2> GetFullCreatecomplianceAttachmentData(string task_id)
        {
            try
            {
                List<TaskFilemappingModel2> data1 = db.Query<TaskFilemappingModel2>("SELECT * from [TaskFilemappingModel2] where taskId ='" + task_id + "'");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void InsertCreateAttachData(TaskFileMapping_Model create_Attachment)
        {
            int i = 0;
            try
            {
                db.Insert(create_Attachment);
            }
            catch (Exception ex)
            {

            }
        }

        public List<TaskFileMapping_Model> GetFullCreateAttachmentData(string task_id)
        {
            try
            {
                List<TaskFileMapping_Model> data1 = db.Query<TaskFileMapping_Model>("SELECT * from [TaskFileMapping_Model] where localtaskId ='" + task_id + "'");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TaskFileMapping_Model> GetCreateAttachmentData(string type,string taskid)
        {
            try
            {
                List<TaskFileMapping_Model> data1 = db.Query<TaskFileMapping_Model>("SELECT * from [TaskFileMapping_Model] where FileType ='" + type + "'");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public long updateComplianceattachmentstatus(string stauscheck)
        {
            try
            {

                var data = db.Table<Comp_AttachmentModel>();
               // int idvalue = Convert.ToInt32(id);

                var data1 = (from values in data
                             where values.status == stauscheck
                             select values).FirstOrDefault();
                data1.status = "yes";

                int i = db.Update(data1);
                return i;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public void InsertAttachmentData(Comp_AttachmentModel comp_Attachment,string checkstatus)
        {
            int i = 0;
            try
            {
                
                Comp_AttachmentModel compmodel = new Comp_AttachmentModel();
                compmodel.taskId = comp_Attachment.taskId;
                compmodel.FileName = comp_Attachment.FileName;
                compmodel.FileSize = comp_Attachment.FileSize;
                compmodel.file_format = comp_Attachment.file_format;
                compmodel.file_type = comp_Attachment.file_type;
                compmodel.Path = comp_Attachment.Path;
                compmodel.localPath = comp_Attachment.localPath;
                compmodel.GeoLocation = comp_Attachment.GeoLocation;
                compmodel.max_numbers = comp_Attachment.max_numbers;
                compmodel.status = checkstatus;


               i= db.Insert(comp_Attachment);
            }
            catch (Exception ex)
            {

            }
        }

        public List<Comp_AttachmentModel> GetFullAttachmentData(string task_id)
        {
            try
            {
                List<Comp_AttachmentModel> data1 = db.Query<Comp_AttachmentModel>("SELECT * from [Comp_AttachmentModel] where taskId ='" + task_id + "'");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public void GetAttachmentData(string type)
        //{
        //    try
        //    {
        //        var data1 = db.Query<Comp_AttachmentModel>("SELECT * from [Comp_AttachmentModel] where file_type ='" + type + "'");
        //        // d
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        public List<Comp_AttachmentModel> GetAttachmentData(string name)
        {
            try
            {
                List<Comp_AttachmentModel> data1 = db.Query<Comp_AttachmentModel>("SELECT * from [Comp_AttachmentModel] where file_type ='" + name + "'");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void DeleteRow( string filename)
        {
            var data = db.Table<Comp_AttachmentModel>();
            var data1 = data.Where(x => x.FileName == filename).FirstOrDefault();
            if (data1 != null)
            {
                db.Delete(data1);
                //var data = <Comp_AttachmentModel>("SELECT * from [Comp_AttachmentModel] where Attachment_Name ='" + filename + "'");
            }
        }

        public void insertdatainbox(List<TaskInboxModel> inbox)
        {
            int i = 0;

            for (int j = 0; j < inbox.Count; j++)
            {
                var data1 = db.Query<TaskInboxModel>("SELECT * from [TaskInboxModel] where [task_id]='" + inbox[j].task_id + "'");
                if (data1.Count <= 0)
                {
                    TaskInboxModel inboxList = new TaskInboxModel();
                    inboxList.id = inbox[j].id;
                    inboxList.TaskPercentage = inbox[j].TaskPercentage;
                    inboxList.task_id = inbox[j].task_id;
                    inboxList.task_name = inbox[j].task_name;
                    inboxList.description = inbox[j].description;

                    inboxList.deadlineDate = inbox[j].deadlineDate;
                    inboxList.mark_to = inbox[j].mark_to;
                    inboxList.taskStatus = inbox[j].taskStatus;
                    inboxList.task_created_by = inbox[j].task_created_by;

                    inboxList.task_creation_date = inbox[j].task_creation_date;
                    inboxList.lstAddedCompliance = inbox[j].lstAddedCompliance;
                    inboxList.ip = inbox[j].ip;
                    inboxList.requestfordeadline = inbox[j].requestfordeadline;
                    inboxList.task_mark_by = inbox[j].task_mark_by;
                    inboxList.MarkingDate = inbox[j].MarkingDate;
                    inboxList.task_marking_type = inbox[j].task_marking_type;
                    inboxList.lstCommunication = inbox[j].lstCommunication;
                    inboxList.lstTaskDocumentMapping = inbox[j].lstTaskDocumentMapping;
                    i = db.Insert(inboxList);
                }



                //}
            }



        }
        public List<TaskInboxModel> GetTaskInbox()
        {
            try
            {
                List<TaskInboxModel> data1 = db.Query<TaskInboxModel>("SELECT * from [TaskInboxModel] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void insertdataoutbox(List<TaskOutboxModel> outbox)
        {
            int i = 0;
            try
            {
                for (int j = 0; j < outbox.Count; j++)
                {
                    var data1 = db.Query<TaskOutboxModel>("SELECT * from [TaskOutboxModel] where [Task_id]='" + outbox[j].Task_id + "'");
                    if (data1.Count <= 0)
                    {

                        TaskOutboxModel outboxList = new TaskOutboxModel();
                        // outboxList.id = outbox[j].id;
                        outboxList.TaskPercentage = outbox[j].TaskPercentage;
                        outboxList.Task_id = outbox[j].Task_id;
                        outboxList.Task_name = outbox[j].Task_name;
                        outboxList.Description = outbox[j].Description;

                        outboxList.deadline_date = outbox[j].deadline_date;
                        outboxList.mark_to = outbox[j].mark_to;
                        outboxList.task_status = outbox[j].task_status;
                        //  outboxList.task_created_by = outbox[j].task_created_by;

                        outboxList.Task_creation_date = outbox[j].Task_creation_date;
                        // inboxList.lstAddedCompliance = inbox[j].lstAddedCompliance;
                        outboxList.ip = outbox[j].ip;
                        //outboxList.requestfordeadline = outbox[j].requestfordeadline;
                        outboxList.task_mark_by = outbox[j].task_mark_by;
                        outboxList.MarkingDate = outbox[j].MarkingDate;
                        outboxList.task_marking_type = outbox[j].task_marking_type;
                        //  inboxList.lstCommunication = inbox[j].lstCommunication;
                        // inboxList.lstTaskDocumentMapping = inbox[j].lstTaskDocumentMapping;
                        i = db.Insert(outboxList);

                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        public List<TaskOutboxModel> Getoutboxdata()
        {
            try
            {
                List<TaskOutboxModel> data1 = db.Query<TaskOutboxModel>("SELECT * from [TaskOutboxModel] ");
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<TaskInboxModel> getDataByDate(string FromDate, string ToDate)
        {
            try
            {

                List<TaskInboxModel> data21 = db.Query<TaskInboxModel>("SELECT * FROM TaskInboxModel WHERE deadlineDate > ? AND deadlineDate < ?", Convert.ToDateTime(FromDate).Ticks, Convert.ToDateTime(ToDate).Ticks);

             //   List<TaskInboxModel> data21 = db.Query<TaskInboxModel>("SELECT * FROM TaskInboxModel WHERE deadlineDate > ? AND deadlineDate < ?", Convert.ToDateTime(FromDate).Ticks, Convert.ToDateTime(ToDate).Ticks);

              //  data21 += db.Query<OrgModel>("SELECT * From OrgModel,  where organizationName =  );

                return data21;


                // List<TaskInboxModel> data2 = db.Query<TaskInboxModel>("SELECT* from[TaskInboxModel] where date(deadlineDate) BETWEEN date('" + FromDate + "') AND date('" + ToDate + "') ");

                // List<TaskInboxModel> data2 = db.Query<TaskInboxModel>("SELECT * from [TaskInboxModel]");
                //List<TaskInboxModel> data2 = db.Query<TaskInboxModel>("SELECT* FROM [TaskInboxModel] WHERE deadlineDate BETWEEN date(:" + FromDate+") AND date(:"+ToDate+")");



                //List<TaskInboxModel> data21 = db.Query<TaskInboxModel>("SELECT * from [TaskInboxModel] where deadlineDate>='" + FromDate + " 00:00:00' AND deadlineDate<='" + ToDate + " 23:59:59'");

            }
            catch (Exception ex1)
            {
                return null;
            }
        }

        public List<Comp_AttachmentModel> GetComp_Attachments(string task_id)
        {
            try
            {
                List<Comp_AttachmentModel> data1 = db.Query<Comp_AttachmentModel>("SELECT * from [Comp_AttachmentModel] where [taskId]='" + task_id + "'" );
                return data1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
}

