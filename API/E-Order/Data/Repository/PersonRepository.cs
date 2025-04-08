using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using e_order.Data;
using e_order.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace e_order.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(DataContext dataContext) : base(dataContext)
        {
        }
    }
}