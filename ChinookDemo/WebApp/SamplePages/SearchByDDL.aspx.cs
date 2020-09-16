using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

#region Additional namespaces
using ChinookSystem.BLL;
using ChinookSystem.ViewModels;
#endregion
namespace WebApp.SamplePages
{
    public partial class SearchByDDL : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            MessageLabel.Text = "";
            if(!IsPostBack)
            {
                BindArtistList();
            }
        }
        protected void BindArtistList ()
        {
            ArtistController sysmgr = new ArtistController();
            List<SelectionList> info = sysmgr.Artist_List();

            //reminder on how to .sort in your code behind
            //info.Sort((x, y) => x.DisplayText.CompareTo(y.DisplayText));

            //setup for DDL
            ArtistList.DataSource = info;                                              ///grab list of artist
            ArtistList.DataTextField = nameof(SelectionList.DisplayText);               //assign Display text
            ArtistList.DataValueField = nameof(SelectionList.ValueId);                  //assign value relation
            ArtistList.DataBind();

            //setup the prompt
            ListItem prompt = new ListItem();
            prompt.Value = "0";
            prompt.Text = "Select an Artist";
            ArtistList.Items.Insert(0, prompt);

            //Alternatively----Doing it in one line
           // ArtistList.Items.Insert(0, new ListItem("select an artist", "0"));
        }

        protected void SearchAlbums_Click(object sender, EventArgs e)
        {
            if (ArtistList.SelectedIndex==0)
            {
                MessageLabel.Text = "Select an artist for search";
            }
            else
            {
                AlbumController sysmgr = new AlbumController();
                List<AlbumArtist> info = sysmgr.Album_FindByArtist(int.Parse(ArtistList.SelectedValue));
                AlbumArtistList.DataSource = info;
                AlbumArtistList.DataBind();
            }
        }

        protected void AlbumArtistList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //access the value from the gridview
            GridViewRow agvrow = AlbumArtistList.Rows[AlbumArtistList.SelectedIndex];
            //data from gridview contro comes in as a STRING
            MessageLabel.Text = (agvrow.FindControl("AlbumId") as Label).Text;
        }
    }
}