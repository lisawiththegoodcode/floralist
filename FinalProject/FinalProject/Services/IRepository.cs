using FinalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Services
{
    public interface IRepository : IDisposable
    {
        IQueryable<Image> Images { get; }
    }
}
