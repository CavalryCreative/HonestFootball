using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HonestFootball.Interfaces;
using HonestFootball.Models;

namespace HonestFootball.Android.Core
{
    public class WebService : IWebService
    {
        public Task<Comment[]> GetComments(string commentId)
        {
            throw new NotImplementedException();
        }

        public Task<IList<Team>> GetStandings()
        {
            throw new NotImplementedException();
        }
    }
}