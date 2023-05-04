using MySqlConnector;
using System;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using TODOProject.EventArgs;
using TODOProject.Modal;
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

        private  TaskPresenter _taskPresenter;
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
            TaskPresenter taskPresenter = new TaskPresenter(this);
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
            { exp = exception.Message; }
            catch (ArgumentException exception)
            { exp = exception.Message; }
            catch (Exception exception)
            {exp=exception.Message;
            }
            notification("Error", exp);
        }

        public void notification()
        {
            string exp = string.Empty;
            try
            {
                divNotification.InnerHtml = string.Empty;
            }
            catch (MySqlException exception)
            { exp = exception.Message; }
            catch (ArgumentException exception)
            { exp = exception.Message; }
            catch (Exception exception)
            {
                exp = exception.Message;
            }
            notification("Error", exp);
        }
        public void notification(string type, string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                if (type == "Error")
                {
                    divNotification.InnerHtml = "<div class=\"alert alert-danger\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == "Success")
                {
                    divNotification.InnerHtml = "<div class=\"alert alert-success\">" + msg + "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">×</span></button></div>";
                }
                else if (type == string.Empty)
                {
                    divNotification.InnerHtml = string.Empty;
                }
            }
        }

        #region Events

        protected void lnkSave_Click(object sender, System.EventArgs e)
        {
            string exp = string.Empty;
            try
            {
                if (txtTaskName.Text == string.Empty)
                {
                    notification("Error", "Please enter to do description");
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
            { exp = exception.Message; }
            catch (ArgumentException exception)
            { exp = exception.Message; }
            catch (Exception exception)
            {
                exp = exception.Message;
            }
            notification("Error", exp);
        }

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
                { exp = exception.Message; }
                catch (ArgumentException exception)
                { exp = exception.Message; }
                catch (Exception exception)
                {
                    exp = exception.Message;
                }
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
            { exp = exception.Message; }
            catch (ArgumentException exception)
            { exp = exception.Message; }
            catch (Exception exception)
            {
                exp = exception.Message;
            }
            notification("Error", exp);
        }

        protected void lnkCancelEdit_Click(object sender, System.EventArgs e)
        {
            string exp = string.Empty;
            try
            {
                //lnkCancelEdit.Style.Remove("display");
                lnkCancelEdit.Style.Add("display", "none");

                hfEditID.Value = string.Empty;
            }
            catch (MySqlException exception)
            { exp = exception.Message; }
            catch (ArgumentException exception)
            { exp = exception.Message; }
            catch (Exception exception)
            {
                exp = exception.Message;
            }
            notification("Error", exp);
        }
        [WebMethod]
        public static string UpdatePosition(int ItemID, int Position)
        {
            
            ModelsData modelsData = new ModelsData();
            modelsData.SaveQuery(new TaskEventArgs { ID = ItemID, TaskOrder = Position });
            

            return "success";
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
            { exp = exception.Message; }
            catch (ArgumentException exception)
            { exp = exception.Message; }
            catch (Exception exception)
            {
                exp = exception.Message;
            }
            notification("Error", exp);
        }

        #endregion
    }
}