using ChinookSystem.BLL;
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
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement", "You must have a playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist showing");

                }
                else
                {
                    //was anything actually selected
                    CheckBox songSelected = null;//reference pointer to a control
                    int songselectedCount = 0; //count number of selected songs
                    int trackid = 0;            //Track Id of song to move
                    int tracknumber = 0;        //track number of song to move
                    //traverse the song list
                    //only one song may be selected for movement
                    for (int index = 0; index < PlayList.Rows.Count; index++)
                    {
                        //point to a checkbox on the gridviewrow
                        songSelected = PlayList.Rows[index].FindControl("Selected") as CheckBox;
                        //Selecte???
                        if (songSelected.Checked)
                        {
                            songselectedCount++;
                            trackid = int.Parse((PlayList.Rows[index].FindControl("TrackId") as Label).Text);
                            tracknumber = int.Parse((PlayList.Rows[index].FindControl("TrackNumber") as Label).Text);
                        }
                        //did you select a row and only a single row
                        if (songselectedCount != 1)
                        {
                            MessageUserControl.ShowInfo("Track Movement", "Song is at the top of list. ");

                        }
                        else
                        {
                            //is this the last row
                            if (tracknumber == PlayList.Rows.Count)
                            {
                                MessageUserControl.ShowInfo("Track Movement", "Song is at the bottom of list. No need to move track");
                            }
                            else
                            {
                                //Move Track
                                MoveTrack(trackid, tracknumber, "down");
                            }
                        }
                    }
                }

            }
        }

        protected void MoveUp_Click(object sender, EventArgs e)
        {
            //code to go here
            if (string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Track Movement", "You must have a playlist name");
            }
            else
            {
                if (PlayList.Rows.Count == 0)
                {
                    MessageUserControl.ShowInfo("Track Movement", "You must have a playlist showing");

                }
                else
                {
                    //was anything actually selected
                    CheckBox songSelected = null;//reference pointer to a control
                    int songselectedCount = 0; //count number of selected songs
                    int trackid = 0;            //Track Id of song to move
                    int tracknumber = 0;        //track number of song to move
                    //traverse the song list
                    //only one song may be selected for movement
                    for (int index=0; index<PlayList.Rows.Count; index++)
                    {
                        //point to a checkbox on the gridviewrow
                        songSelected = PlayList.Rows[index].FindControl("Selected") as CheckBox;
                        //Selecte???
                        if (songSelected.Checked)
                        {
                            songselectedCount++;
                            trackid = int.Parse((PlayList.Rows[index].FindControl("TrackId")as Label).Text );
                            tracknumber = int.Parse((PlayList.Rows[index].FindControl("TrackNumber") as Label).Text);
                        }
                        //did you select a row and only a single row
                        if(songselectedCount != 1)
                        {
                            MessageUserControl.ShowInfo("Track Movement", "Song is at the top of list. ");

                        }
                        else
                        {
                            //is this the first row
                            if(tracknumber == 1)
                            {
                                MessageUserControl.ShowInfo("Track Movement", "Song is at the top of list. No need to move track");

                            }
                            else
                            {
                                //Move Track
                                MoveTrack(trackid, tracknumber, "up");
                            }
                        }
                    }
                }

            }
        }

        protected void MoveTrack(int trackid, int tracknumber, string direction)
        {
            //call BLL to move track
            string username = "HansenB";
            MessageUserControl.TryRun(() =>
                {
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    sysmgr.MoveTrack(username, PlaylistName.Text, trackid, tracknumber, direction);
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    PlayList.DataSource = info;
                    //bind resutls to control
                    PlayList.DataBind();
                }, "Move Track", "Track Has been moved");
        }


        protected void DeleteTrack_Click(object sender, EventArgs e)
        {
            //code to go here

        }

        protected void TracksSelectionList_ItemCommand(object sender,
            ListViewCommandEventArgs e)
        {
            string username = "HansenB";
            //validation of incoming data
            if(string.IsNullOrEmpty(PlaylistName.Text))
            {
                MessageUserControl.ShowInfo("Missing Data", "Enter the playlist name");
            }
            else
            {
                //REMINDER: Message User control will do the error handling
                MessageUserControl.TryRun(() =>
                {
                    //Coding block for logic to be run under the error handling
                    //control or the MessageUserControl
                    //a standard add to teh database
                    //connect to controller
                    PlaylistTracksController sysmgr = new PlaylistTracksController();
                    //issue call to the controller method
                    sysmgr.Add_TrackToPLaylist(PlaylistName.Text, username, int.Parse(e.CommandArgument.ToString()));
                    //refresh the playlist
                    List<UserPlaylistTrack> info = sysmgr.List_TracksForPlaylist(PlaylistName.Text, username);
                    //assign the results to the control
                    PlayList.DataSource = info;
                    //bind resutls to control
                    PlayList.DataBind();
                }, "Add track to PlayList", "Track has been added to playlist");
            }
        }

    }
}