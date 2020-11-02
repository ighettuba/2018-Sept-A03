﻿using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Web.UI.WebControls;
using System.Linq;

#region Additonal Namespaces
//using WebApp.Security;
#endregion

namespace WebApp.SamplePages
{
    public partial class ManagePlaylist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TracksSelectionList.DataSource = null;
        }

        #region  Error Handling
        protected void SelectCheckForException(object sender,
                                       ObjectDataSourceStatusEventArgs e)
        {
            MessageUserControl.HandleDataBoundException(e);
        }
        protected void InsertCheckForException(object sender,
                                              ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been added.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void UpdateCheckForException(object sender,
                                               ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been updated.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }
        protected void DeleteCheckForException(object sender,
                                                ObjectDataSourceStatusEventArgs e)
        {
            if (e.Exception == null)
            {
                MessageUserControl.ShowInfo("Success", "Album has been removed.");
            }
            else
            {
                MessageUserControl.HandleDataBoundException(e);
            }
        }

        #endregion


        protected void ArtistFetch_Click(object sender, EventArgs e)
        {
            TracksBy.Text = "Artist";
            SearchArg.Text = ArtistName.Text;
            //validate that data exists if not put out a message
            if (string.IsNullOrEmpty(ArtistName.Text))
            {
                MessageUserControl.ShowInfo("Search entry error",
                    "Enter a artist name or partial artist name. The press your button.");
                SearchArg.Text = "xcfdrte";
            }


            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();



        }

        protected void MediaTypeFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "MediaType";
            //the DDL does not have a prompt line therefore
            //  no selection test required
            //REMEMBER: SelectedValue returns contents as a string
            SearchArg.Text = MediaTypeDDL.SelectedValue;
          

            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();
        }

        protected void GenreFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "Genre";
            //the DDL does not have a prompt line therefore
            //  no selection test required
            //REMEMBER: SelectedValue returns contents as a string
            SearchArg.Text = GenreDDL.SelectedValue;


            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();
        }

        protected void AlbumFetch_Click(object sender, EventArgs e)
        {

            TracksBy.Text = "Album";
            SearchArg.Text = AlbumTitle.Text;
            //validate that data exists if not put out a message
            if (string.IsNullOrEmpty(AlbumTitle.Text))
            {
                MessageUserControl.ShowInfo("Search entry error",
                    "Enter a album title or partial album title. The press your button.");
                SearchArg.Text = "xcfdrte";
            }


            //to force the listview to rebind (to execute again)
            //NOTE there is NO DataSource assignment as that is
            //     accomplished using the ODS and a DataSourceID parameter
            //     on the ListView control
            TracksSelectionList.DataBind();

        }

        protected void PlayListFetch_Click(object sender, EventArgs e)
        {
            //security is yet to be implemented
            //this page needs to know the username of the currently logged user
            //temporarily we will hard code the username
            var username = "HansenB";

            //validate that a string exists in the playlist name
            if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter the playlist name");
            }
            else
            {
                //how do we do error handling using MEsseegeUser control if the
                //  code executing is NOT part of the ODS
                //you could use a try/ catch
                //We wish to use MessegeUserControl
                //If we examine the sourcecode for MessegeUserControl, you will find
                //  embedded within the code, the Try/Catch
                //The Syntax
                //  MessegeUserControl.TryRun(  ()  => {your code block});          OR
                //  MessegeUserControl.TryRun(  ()  => {your code block},"Success Title","Success Message");
                MessageUserControl.TryRun(() =>
                {
                    //Standard look up coding block

                    //connect to controller
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //issue controller call
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    //assign the results to the control
                    PlayList.DataSource = info;
                    //bind resutls to control
                    PlayList.DataBind();
                }, "Playlist", "View the current songs on the Playlist");               
            }

        }

        protected void MoveDown_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track

        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void TracksSelectionList_ItemCommand(object sender,
            ListViewCommandEventArgs e)
        {
            //code to go here

        }

    }
}