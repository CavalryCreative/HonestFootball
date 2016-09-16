using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HonestFootball.Models;

namespace HonestFootball.Interfaces
{
    public interface IWebService
    {
        Task<Comment[]> GetComments(string commentId);
    }
}
