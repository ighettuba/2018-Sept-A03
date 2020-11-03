using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#region Additional Namespaces
using ChinookSystem.ViewModels;
using ChinookSystem.DAL;
using System.ComponentModel;
using ChinookSystem.Entities;
using DMIT2018Common.UserControls;
using System.Diagnostics;
#endregion

namespace ChinookSystem.BLL
{
    public class PlaylistTracksController
    {
        public List<UserPlaylistTrack> List_TracksForPlaylist(
            string playlistname, string username)
        {
            using (var context = new ChinookSystemContext())
            {
                var results = from x in context.PlaylistTracks
                              where x.Playlist.Name.Equals(playlistname)
                              && x.Playlist.UserName.Equals(username)

                              orderby x.TrackNumber
                              select new UserPlaylistTrack
                              {
                                  TrackID = x.TrackId,
                                  TrackNumber = x.TrackNumber,
                                  TrackName = x.Track.Name,
                                  Milliseconds = x.Track.Milliseconds,
                                  UnitPrice = x.Track.UnitPrice
                              };
                return results.ToList();
            }
        }//eom
        public void Add_TrackToPLaylist(string playlistname, string username, int trackid)
        {
            using (var context = new ChinookSystemContext())
            {
                //code within this using will be done as a transaction which 
                //  means there will be ONLY ONE .SaveChanges() within this code.
                //If the .SaveChanges() is NOT executed successfully, all work
                //  within this method will be rolled back AUTOMATICALLY!!!!!!

                //trx
                //Query: PlayList to see if list name exists


                int trackNumber = 0;
                PlaylistTrack newtrack = null;
                List<string> errors = new List<string>(); //for use by BusinessRuleException
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname) 
                                        && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                //if not
                if (exists == null)
                {
                    //  Create an instance of the PlayList
                    //    exists = new Playlist()
                    //    exists.Name = playlistname,
                    //    exists.UserName = username
                    exists = new Playlist()
                    {
                        //  Load instance with data
                        //pkey is an identity int key
                        Name = playlistname,
                        UserName = username
                    };                    
                    //  Stage the instance for adding
                    context.Playlists.Add(exists);
                    //  set the tracknumber to 1
                    trackNumber = 1;
                }
                //if yes
                else
                { 
                    //  check to see if the track already exists on the playlist

                    //exist has the record instance of the playlist
                    //does the track already exist on the playlist
                    newtrack = (from x in context.PlaylistTracks
                                where x.Playlist.Name.Equals(playlistname)
                                    && x.Playlist.UserName.Equals(username)
                                    && x.TrackId == trackid
                                select x).FirstOrDefault();
                    if (newtrack == null)
                    {
                        //track not on playlist 
                    //      no:  Determine the current max tracknumber and increment++
                        trackNumber = (from x in context.PlaylistTracks
                                       where x.Playlist.Name.Equals(playlistname)
                                            && x.Playlist.UserName.Equals(username)
                                       select x.TrackNumber).Max();
                        trackNumber++;
                    }
                    else
                    {
                        //  if found
                        //  yes: throw an error (stop processing trx) Business Rule
                        // track on  playlist
                        //Business rull does not allow for duplicates tracks on playlist

                        //throw an exception
                        //throw new Exception("Track already on playlists. Duplicates not allowed.");

                        //use a BusinessRuleException class to throw the error
                        //this class takes in a List<string> representin all errors to be handled
                        errors.Add("Track already on playlists. Duplicates not allowed.");
                    }
                }

                //create/load/add a PlaylistTrack 
                newtrack = new PlaylistTrack();
                //load instance Data
                newtrack.TrackId = trackid;
                newtrack.TrackNumber = trackNumber;

                //scenario 1. This is a New playlist
                //      the exists instance is a new instance that is  YET
                //          to be placed on the SQL database
                //  THEREFORE it DOES NOT yet have a primary key value!!!!!!!!
                //  the current value of the playlistId on the exist instance 
                //      is the Default system value for an integer(0)
                //scenario 2. This is an existing playlist
                //      the exists instance has the playlist ID value
              

                //the solution to both these scenarios is to use 
                //  navigational properties during the ACTUAL .Add command
                //the entityframework will, on your behalf, ensure that the 
                //  adding of records to the database will be done in the 
                //  appropriate order and the missing compound primary key
                //  value(playlistId) to the child record new track
                exists.PlaylistTracks.Add(newtrack);            //Staged Data

                //Handle creation of playlist track record
                //all validation has been passed??????????
                if (errors.Count() > 0)
                {
                    //NO, at least one error found
                    throw new BusinessRuleException("Adding a Track", errors);
                }
                else
                {
                    //good to go
                    //commit the Staged work to the database
                    context.SaveChanges();

                }
                


            }// this ensures that the sql connection closes properly
        }//eom
        public void MoveTrack(string username, string playlistname, int trackid, int tracknumber, string direction)
        {
            using (var context = new ChinookSystemContext())
            {

                //trx
                //check to see if the playlist exists
                //no:   error exception
                //yes:  
                //  Check to see if song exists
                //  no:     error exception
                //  yes:    
                //      check to see if song is at the top
                //      yes:    error exception
                //      no:     
                //          find record above (tracknumber-1
                //          change above record tracknumber modified to tracknumber + 1
                //          selected record tracnumber modified to tracknumbere - 1
                //check to see if song is at the botom
                //      yes:    error exception
                //      no:     
                //          find record above (tracknumber+1
                //          change above record tracknumber modified to tracknumber - 1
                //          selected record tracnumber modified to tracknumbere + 1
                //stage records
                //commit
                PlaylistTrack moveTrack = null;
                PlaylistTrack otherTrack = null;

                List<string> errors = new List<string>(); //for use by BusinessRuleException
                Playlist exists = (from x in context.Playlists
                                   where x.Name.Equals(playlistname)
                                        && x.UserName.Equals(username)
                                   select x).FirstOrDefault();
                //if not
                if (exists == null)
                {
                    errors.Add("Playlist does not exist");
                }
                else
                {
                    moveTrack = (from x in context.PlaylistTracks
                                 where x.Playlist.Name.Equals(playlistname)
                                      && x.Playlist.UserName.Equals(username)
                                      && x.TrackId ==trackid
                                 select x).FirstOrDefault();
                    if (moveTrack == null)
                    {
                        errors.Add("Playlist track does not exist");

                    }
                    else
                    {
                        if (direction.Equals("up"))         //Move Uo
                        {

                        }
                        else                               //Move Down
                        {

                        }
                    }
                }
                if (errors.Count > 0)
                {
                    throw new BusinessRuleException("Move Track", errors);
                }
                else
                {
                    //stage
                    //commit
                    context.SaveChanges();
                }
            }
        }//eom


        public void DeleteTracks(string username, string playlistname, List<int> trackstodelete)
        {
            using (var context = new ChinookSystemContext())
            {
               //code to go here


            }
        }//eom
    }
}
