using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace ListaVideos
{
    public class VideoFetcherController
    {
        public void FetchVideos(string channelId)
        {
            //Traer instancia de MainWindow a este contexto para poder acceder a la vista
            MainWindow win = (MainWindow)System.Windows.Application.Current.MainWindow;

            //Crear servicio de YouTube
            var youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyC8uvUVBUb7CL9HbpeGdbdQUuHfr3h-v1Q",
            });

            //Solicitarle al servicio los contenidos del canal
            var channelListRequest = youtube.Channels.List("contentDetails");
            channelListRequest.Id = channelId;

            var channelListResponse = channelListRequest.Execute();

            foreach (var channel in channelListResponse.Items)
            {
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;

                var playlistItemsListRequest = youtube.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = uploadsListId;
                playlistItemsListRequest.MaxResults = 50;

                var playlistItemsListResponse = playlistItemsListRequest.Execute();

            //Insertar los títulos de los tracks en la Lista
                foreach (var playlistItem in playlistItemsListResponse.Items)
                {
                    win.listBox.Items.Add(playlistItem.Snippet.Title);
                }
            }


        }
    }
}
