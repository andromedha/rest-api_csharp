using DataClasses;
using DataClasses.Models;
using Repository.Contracts;
using Repository.Sql.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository.Sql
{
    public class SqlRepository : IRepository
    {
        private readonly DatabaseContext _context;

        public SqlRepository(DatabaseContext context)
        {
            _context = context;
        }

        public T Get<T>(int id) where T : AbstractModel
        {
            _context.Database.BeginTransaction();
            try
            {
                return _context.Persons.Where(x => x.Id == id).FirstOrDefault() as T;
            }
            catch(Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }
            finally
            {
                _context.Database.CommitTransaction();
            } 
        }

        public IEnumerable<T> GetList<T>() where T : AbstractModel
        {
            _context.Database.BeginTransaction();
            try
            {
                return _context.Persons as IEnumerable<T>;
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }
            finally
            {
                _context.Database.CommitTransaction();
            }

            
        }

        public T Update<T>(T model) where T : AbstractModel
        {
            _context.Database.BeginTransaction();
            try
            {
                var result = _context.Persons.Add(model as Person);
                _context.SaveChanges();
                return result.Entity as T;
            }
            catch (Exception)
            {
                _context.Database.RollbackTransaction();
                throw;
            }
            finally
            {
                _context.Database.CommitTransaction();
            }  
        }
    }
}
