using CozyCo.Data.Context;
using CozyCo.Data.Interfaces;
using CozyCo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CozyCo.Data.Implementation.SqlServer
{
    public class SqlServerPropertyRepository : IPropertyRepository
    {
        public Property GetById(int id)
        {
            using (var context = new Context.CozyCoDbContext())
            {
                // SQL -> Database look for table Properties
                //if not found returns null value = rather than an exception
                var property = context.Properties.SingleOrDefault(p => p.Id == id);
                return property;
            }
        }
        public ICollection<Property> GetAllPropertiesByUserId(string userId)
        {
            using (var context = new  CozyCoDbContext())
            {
                //DbSet != ICollection
                return context.Properties
                    .Where(p => p.AppUserId == userId)
                    .ToList();
            }
        }
        public Property Create(Property newProperty)
        {
            using (var context = new CozyCoDbContext())
            {
                context.Properties.Add(newProperty);
                context.SaveChanges();

                return newProperty; //newProperty.Id will be populated with new DB value
            }
        }

        public Property Update(Property updatedProperty)
        {
            using (var context = new CozyCoDbContext())
            {
                //find the old entity
                var oldProperty = GetById(updatedProperty.Id);

                //update each entity properties / get;set;
                context.Entry(oldProperty).CurrentValues.SetValues(updatedProperty);

                //save changes
                context.Properties.Update(oldProperty);
                context.SaveChanges();

                return oldProperty; //To ensure that the save happened
            }

            
        }

        public bool Delete(int id)
        {
            using (var context = new CozyCoDbContext())
            {
                //Find what we're going to delete
                var propertyToBeDeleted = GetById(id);

                //delete
                context.Properties.Remove(propertyToBeDeleted);

                //save changes
                context.SaveChanges();

                //check if entity/model exists
                if(GetById(id) == null)
                {
                    return true;
                }

                return false;
            }

        }

       
    }
}
