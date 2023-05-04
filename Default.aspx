<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TODOProject._Default" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    

     

    <!-- Main -->
      <div id="main">
                <noscript>This site just doesn't work, period, without JavaScript</noscript>
                <asp:HiddenField ID="hfEditID" runat="server" />
                <asp:TextBox ID="hfSelectedColor" runat="server" Visible="false"></asp:TextBox>
                <ul id="list" class="ui-sortable drag_drop_grid">
                    <asp:Repeater ID="rptList" runat="server" OnItemCommand="rptList_ItemCommand">
                        <ItemTemplate>
                            <li color="1" class="colorBlue" rel="1" id='<%# Eval("ID") %>' data-value='<%# Eval("TaskOrder") %>'>
                                <span 
                                    id="2listitem" 
                                    title="Double-click to edit..." 
                                    style='<%# "opacity: 1; background-color: " + DataBinder.Eval(Container.DataItem, "TaskColor") + ";" %>'>
                                    <%# Eval("TaskName") %>
                                </span>

                                <div class="draggertab tab"></div>

                               

                                <asp:TextBox
                                    ID="txtCardColor" 
                                    runat="server" 
                                    class="colortab tab cardColor" 
                                    style="border: none; height: 10px !important; position: absolute; left: 34px; width: 20px; background-position: -31px 0; cursor: pointer;"                       
                                    AutoPostBack="true"
                                    OnTextChanged="txtCardColor_TextChanged"
                                    />
                                
                               <cc1:ColorPickerExtender
                                    ID="txtCardColor_ColorPickerExtender"
                                    TargetControlID="txtCardColor"
                                    Enabled="True"
                                    runat="server"></cc1:ColorPickerExtender>

                                <div id="btnDelete" class="deletetab tab"></div>



                                
                                <div class="tab" id="btnCancelDelete" style="position: absolute; background-position: -82px 0; cursor: pointer; width: 44px; display: none; right: -64px;"></div>
                                <asp:linkbutton ID="lnkConfirmDelete" commandname="Confirm" runat="server" text="Update"  CommandArgument='<%# Eval("ID") %>'>
                                    <div class="donetab tab" style="display: none;"></div>
                                </asp:linkbutton>
                                                                
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
                <br />
                
                <div id="divNotification" runat="server"></div>
                <div id="add-new">
                    <asp:TextBox ID="txtTaskName" runat="server" style="width: 532px; float: left; margin: 0 10px 0 69px;"></asp:TextBox>
                    <asp:HiddenField ID="hfIsDone" runat="server" />
                    <asp:LinkButton ID="lnkSave" runat="server" CssClass="button" style="padding: 10px 12px;" OnClick="lnkSave_Click">Add</asp:LinkButton>
                    <asp:LinkButton ID="lnkUpdate" runat="server" CssClass="button" style="padding: 10px 12px;" OnClick="lnkUpdate_Click">Update</asp:LinkButton>
                    <asp:LinkButton ID="lnkCancelEdit" runat="server" CssClass="button" style="padding: 10px 12px;" OnClick="lnkCancelEdit_Click">Cacnel</asp:LinkButton>
                </div>

                <div class="clear"></div>
            </div>
    <!-- END -->
   
  
</asp:Content>





