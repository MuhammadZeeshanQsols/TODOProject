using MySqlConnector;
using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TODOProject.EventArgs;
using TODOProject.Modal;
using TODOProject.Modal.Interfaces;
using TODOProject.Presentation;
using TODOProject.View;

namespace TODOProject
{
    public partial class _Default : Page, ITaskView
    {
        public event EventHandler<TaskEventArgs> LoadHandler;
        public event EventHandler<TaskEventArgs> SaveHandler;
        public event EventHandler<TaskEventArgs> DeleteHandler;
        public event EventHandler<TaskEventArgs> ChangeTaskNameHandler;
        public event EventHandler<TaskEventArgs> ChangeTaskColorHandler;
        public event EventHandler<TaskEventArgs> ChangeOrder;

        private  TaskPresenter _taskPresenter;
      private readonly  IdbRepository dbRepository;
        public _Default(IdbRepository dbRepository)
        {
            dbRepository = dbRepository;
        }
        public void AttachPresenter(TaskPresenter taskPresenter)
        {
            if (taskPresenter == null)
            {
                throw new ArgumentNullException("presenter may not be null");
            }
            this._taskPresenter = taskPresenter;
        }
        
        protected void Page_Load(object sender, System.EventArgs e)
        {
            TaskPresenter taskPresenter = new TaskPresenter(this, dbRepository);
            AttachPresenter(taskPresenter);
            if (!IsPostBack)
            { 
                BindData();
                lnkUpdate.Style.Add("display", "none");
                lnkCancelEdit.Style.Add("display", "none");
            }

        }
        public void BindData()
        {
            string exp = string.Empty;
            try
            {
                if (LoadHandler != null)
                {
                    TaskEventArgs args = new TaskEventArgs();
                    LoadHandler(this, args);
                    rptList.DataSource = args.TodoList;
                    rptList.DataBind();
                }
            }
            catch (MySqlException exception)
            { exp=GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.SqlException, exception); }
            catch (ArgumentException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.ArgumentExceptions,exception); }
            catch (Exception exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.Exception, exception);}
            notification("Error", exp);
        }

       

        #region Events

        public void lnkSave_Click(object sender, System.EventArgs e)
        {
            string exp = string.Empty;
            try
            {
                if (txtTaskName.Text == string.Empty)
                {
                    GenrcModel.notification("Error", "Please enter to do description");
                    txtTaskName.Focus();
                }
                else
                {
                    if (SaveHandler != null)
                    {
                        TaskEventArgs args = new TaskEventArgs();
                        args.TaskName = txtTaskName.Text;
                        SaveHandler(this, args);
                         BindData();
                         txtTaskName.Text = string.Empty;
                          txtTaskName.Focus();
                        
                    }
                }
            }
            catch (MySqlException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.SqlException, exception); }
            catch (ArgumentException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.ArgumentExceptions, exception); }
            catch (Exception exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.Exception, exception); }
            notification("Error", exp);
        }
        public void notification(string type, string msg) => divNotification.InnerHtml = GenrcModel.notification(type, msg);
        protected void rptList_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            string exp = string.Empty;
            if (e.CommandName == "Confirm")
            {
                try
                {
                    if (DeleteHandler != null)
                    {
                        TaskEventArgs args = new TaskEventArgs();
                        args.ID =(hfEditID.Value!=null? Convert.ToInt32(hfEditID.Value):0);
                        DeleteHandler(this, args);
                         BindData();
                    }
                }
                catch (MySqlException exception)
                { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.SqlException, exception); }
                catch (ArgumentException exception)
                { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.ArgumentExceptions, exception); }
                catch (Exception exception)
                { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.Exception, exception); }
                notification("Error", exp);
            }
        }

        protected void lnkUpdate_Click(object sender, System.EventArgs e)
        {
            string exp = string.Empty;
            try
            {
                if (ChangeTaskNameHandler != null)
                {
                    TaskEventArgs args = new TaskEventArgs();
                    args.ID = (hfEditID.Value != null ? Convert.ToInt32(hfEditID.Value) : 0);
                    args.TaskName = txtTaskName.Text;
                    ChangeTaskNameHandler(this, args);
                    BindData();
                    txtTaskName.Text = string.Empty;
                    hfEditID.Value = string.Empty;
                    hfSelectedColor.Text = string.Empty;
                }
                else
                {
                    notification("Error", "Cannot update task, due to: Edit Handler is null");
                }
            }
            catch (MySqlException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.SqlException, exception); }
            catch (ArgumentException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.ArgumentExceptions, exception); }
            catch (Exception exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.Exception, exception); }
            notification("Error", exp);
        }

        protected void lnkCancelEdit_Click(object sender, System.EventArgs e)
        {
            string exp = string.Empty;
            try
            {
                
                lnkCancelEdit.Style.Add("display", "none");
                hfEditID.Value = string.Empty;
            }
            catch (MySqlException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.SqlException, exception); }
            catch (ArgumentException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.ArgumentExceptions, exception); }
            catch (Exception exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.Exception, exception); }
            notification("Error", exp);
        }
        [WebMethod]
        public  string UpdatePosition(int ItemID, int Position)
        {
            if (ChangeOrder != null)
            {
                ChangeOrder(this, new TaskEventArgs { ID = ItemID, TaskOrder = Position });
               // modelsData.SaveQuery(new TaskEventArgs { ID = ItemID, TaskOrder = Position });
            } return "success";
        }
        protected void txtCardColor_TextChanged(object sender, System.EventArgs e)
        {
            string exp = string.Empty;
            try
            {
                if (hfEditID.Value.Trim() == string.Empty)
                {
                    notification("Error", "Please select color first");
                }
                else
                {
                    if (ChangeTaskColorHandler != null)
                    {
                        TextBox txtColor = (TextBox)sender;
                        if (txtColor.Text == string.Empty)
                        {
                            notification("Error", "Please select color");
                        }
                        else
                        {
                           TaskEventArgs args = new TaskEventArgs();
                            args.ID = (hfEditID.Value != null ? Convert.ToInt32(hfEditID.Value) : 0);
                            args.TaskColor = "#" + txtColor.Text;
                            ChangeTaskColorHandler(this, args);
                            BindData();
                            txtColor.Text = string.Empty;
                            hfEditID.Value = string.Empty;
                        }
                    }
                }
            }
            catch (MySqlException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.SqlException, exception); }
            catch (ArgumentException exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.ArgumentExceptions, exception); }
            catch (Exception exception)
            { exp = GenrcModel.Exceptiontype(TaskEventArgs.ExceptionType.Exception, exception); }
            notification("Error", exp);
        }

        #endregion
    }
}