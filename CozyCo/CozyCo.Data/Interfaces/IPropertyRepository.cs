using CozyCo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CozyCo.Data.Interfaces
{
    public interface IPropertyRepository
    {
        //Read
        Property GetById(int id);
        ICollection<Property> GetAllPropertiesByUserId(string userId);

        //Create
        Property Create(Property newProperty);

        //Update
        Property Update(Property updatedProperty);

        //Delete
        bool Delete(int id);
    }
}
