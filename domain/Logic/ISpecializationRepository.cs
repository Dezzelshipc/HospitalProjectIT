using domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Logic.Interfaces
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        public Specialization? GetByName(string name);
    }
}