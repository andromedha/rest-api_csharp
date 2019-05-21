using DataClasses.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Contracts
{
    public interface IRepository
    {
        T Get<T>(int id) where T : AbstractModel;

        IEnumerable<T> GetList<T>() where T : AbstractModel;

        T Update<T> (T model) where T : AbstractModel;
    }
}
