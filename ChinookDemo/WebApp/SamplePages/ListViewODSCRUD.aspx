﻿<%@ Page Title="Listview ODS CRUD" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListViewODSCRUD.aspx.cs" Inherits="WebApp.SamplePages.ListViewODSCRUD" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Single Control ODS CRUD : LitView</h1>
    <blockquote>
        This sample will use the asp: ListView control. <br />
        This sample will use ObectDataSource for the control.<br />
        This sample will use minimal code behind.<br />
        This sample will demonstrate the use of the course supplied error handling control<br />
        This sample will demonstrate validation on a ListView CRUD
    </blockquote>
    <div class="row">
        <%-- using the MessageUserControl to handle erros on a web page --%>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        <asp:ValidationSummary ID="ValidationSummaryInsert" runat="server" 
            HeaderText="Following are concerns with your data: "
            ValidationGroup="igroup"/>
        <asp:ValidationSummary ID="ValidationSummaryEdit" runat="server"
            HeaderText="Following are concerns with your data: "
            ValidationGroup="egroup"/>
    </div>
    <div class="row">
        <div class="offset-1">
<%--             remember to use the attribute DataKeyNames to get the delete to work--%>
            <asp:ListView ID="AlbumList" runat="server" 
                DataSourceID="AlbumListODS" 
                InsertItemPosition="FirstItem"
                DataKeyNames="AlbumId">

                <AlternatingItemTemplate>
                    <tr style="background-color: #FFFFFF; color: #284775;">
                       
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="30px" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                        <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayText" 
                            DataValueField="ValueId"
                            selectedValue='<%# Bind("ArtistId") %>'
                            Width ="300px">                        
                            
                        </asp:DropDownList></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" Width="30px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" Width="30px"/></td>
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" 
                                OnClientClick="Return confirm(Are you sure you wish to remove?)"/>
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <asp:RequiredFieldValidator ID="RequiredTitleTextBoxE" runat="server"
                        ErrorMessage="Title is required" 
                        Display="None"
                        ControlToValidate="TitleTextBoxE" 
                        ValidationGroup="egroup">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegExTitleTextBoxE" runat="server" 
                        ErrorMessage="Title is limited to 160 characters" 
                        Display="None" 
                        ControlToValidate="TitleTextBoxE" 
                        ValidationGroup="egroup" 
                        ValidationExpression="^.{1,160}$">
                    </asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator ID="RequiredReleasedYearTextboxE" runat="server"
                        ErrorMessage="Year is required" 
                        Display="None"
                        ControlToValidate="ReleasedYearTextboxE" 
                        ValidationGroup="egroup">
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeReleasedYearTextboxE" runat="server"  MinimumValue="1950" MaximumValue="<%#DateTime.Today.Year %>"
                        ErrorMessage="Release year must be between 1950 and now"  ControlToValidate="ReleasedYearTextboxE" 
                        ValidationGroup="egroup"></asp:RangeValidator>
                    <tr style="background-color: #999999;">
                       
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" Width="30px" Enabled ="false"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxE" /></td>
                        <td>
                                <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayText" 
                            DataValueField="ValueId"
                            selectedValue='<%# Bind("ArtistId") %>'
                            Width ="300px">                        
                            
                        </asp:DropDownList></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleasedYearTextboxE" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxE" /></td>
                        <td>
                            <asp:Button runat="server" CommandName="Update" Text="Update" ID="UpdateButton" ValidationGroup="egroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Cancel" ID="CancelButton" />
                        </td>
                    </tr>
                </EditItemTemplate>
                <EmptyDataTemplate>
                    <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                        <tr>
                            <td>No data was returned.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
                <InsertItemTemplate>
                    <asp:RequiredFieldValidator ID="RequiredTitleTextBoxI" runat="server"
                        ErrorMessage="Title is required" 
                        Display="None"
                        ControlToValidate="TitleTextBoxI" 
                        ValidationGroup="igroup">
                    </asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegExTitleTextBoxI" runat="server" 
                        ErrorMessage="Title is limited to 160 characters" 
                        Display="None" 
                        ControlToValidate="TitleTextBoxI" 
                        ValidationGroup="igroup" 
                        ValidationExpression="^.{1,160}$">
                    </asp:RegularExpressionValidator>
                         <asp:RequiredFieldValidator ID="RequiredReleasedYearTextboxI" runat="server"
                        ErrorMessage="Year is required" 
                        Display="None"
                        ControlToValidate="ReleaseYearTextboxI" 
                        ValidationGroup="igroup">
                    </asp:RequiredFieldValidator>
                    <asp:RangeValidator ID="RangeReleasedYearTextboxE" runat="server" MinimumValue="1950" MaximumValue="<%#DateTime.Today.Year %>"
                        ErrorMessage="Release year must be between 1950 and now" 
                        Display="None" 
                        ControlToValidate="ReleaseYearTextboxI" 
                        ValidationGroup="igroup" ></asp:RangeValidator>
                    <tr style="">
                       
                        <td>
                            <asp:TextBox Text='<%# Bind("AlbumId") %>' runat="server" ID="AlbumIdTextBox" Width="30px" Enabled ="false"/></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("Title") %>' runat="server" ID="TitleTextBoxI" /></td>
                        <td>
                         <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayText" 
                            DataValueField="ValueId"
                             selectedValue='<%# Bind("ArtistId") %>'
                            Width ="300px">                        
                            
                        </asp:DropDownList></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseYear") %>' runat="server" ID="ReleaseYearTextBoxI" /></td>
                        <td>
                            <asp:TextBox Text='<%# Bind("ReleaseLabel") %>' runat="server" ID="ReleaseLabelTextBoxI" /></td> 
                        <td>
                            <asp:Button runat="server" CommandName="Insert" Text="Insert" ID="InsertButton" ValidationGroup="igroup"/>
                            <asp:Button runat="server" CommandName="Cancel" Text="Clear" ID="CancelButton" />
                        </td>
                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: #E0FFFF; color: #333333;">
                        
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="30px" Enabled ="false"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                         <asp:DropDownList ID="ArtistList" runat="server" 
                            DataSourceID="ArtistListODS" 
                            DataTextField="DisplayText" 
                            DataValueField="ValueId"
                            selectedValue='<%# Bind("ArtistId") %>'
                            Width ="300px">                        
                            
                        </asp:DropDownList></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" 
                                OnClientClick="Return confirm(Are you sure you wish to remove?)"/>
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;" border="1">
                                    <tr runat="server" style="background-color: #E0FFFF; color: #333333;">
                                       
                                        <th runat="server">Id</th>
                                        <th runat="server">Title</th>
                                        <th runat="server">ArtistId</th>
                                        <th runat="server">Year</th>
                                        <th runat="server">Label</th>
                                        <th runat="server"></th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button"
                                            ShowFirstPageButton="True"
                                            ShowNextPageButton="False"
                                            ShowPreviousPageButton="False"
                                            ButtonCssClass="datapagerStyle"></asp:NextPreviousPagerField>
                                        <asp:NumericPagerField></asp:NumericPagerField>
                                        <asp:NextPreviousPagerField ButtonType="Button"
                                            ShowLastPageButton="True"
                                            ShowNextPageButton="False"
                                            ShowPreviousPageButton="False"
                                            ButtonCssClass="datapagerStyle"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr style="background-color: #E2DED6; font-weight: bold; color: #333333;">
                        <td>
                            <asp:Button runat="server" CommandName="Delete" Text="Delete" ID="DeleteButton" />
                            <asp:Button runat="server" CommandName="Edit" Text="Edit" ID="EditButton" />
                        </td>
                        <td>
                            <asp:Label Text='<%# Eval("AlbumId") %>' runat="server" ID="AlbumIdLabel" Width="30px"/></td>
                        <td>
                            <asp:Label Text='<%# Eval("Title") %>' runat="server" ID="TitleLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ArtistId") %>' runat="server" ID="ArtistIdLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseYear") %>' runat="server" ID="ReleaseYearLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ReleaseLabel") %>' runat="server" ID="ReleaseLabelLabel" /></td>
                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
          
        </div>
    </div>
    <asp:ObjectDataSource ID="AlbumListODS" runat="server" 
        DataObjectTypeName="ChinookSystem.ViewModels.AlbumList" 
        DeleteMethod="Album_Delete" 
        InsertMethod="Album_Add" 
        SelectMethod="Album_List"  
        UpdateMethod="Album_Update"
        OldValuesParameterFormatString="original_{0}"        
        TypeName="ChinookSystem.BLL.AlbumController" 
         OnDeleted="DeleteCheckForException"
         OnInserted="InsertCheckForException"
         OnSelected="SelectCheckForException"
         OnUpdated="UpdateCheckForException">

        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="artistid" Type="Int32"></asp:Parameter>

        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ArtistListODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Artist_List" 
        TypeName="ChinookSystem.BLL.ArtistController"
         OnSelected="SelectCheckForException">

            </asp:ObjectDataSource>
</asp:Content>
