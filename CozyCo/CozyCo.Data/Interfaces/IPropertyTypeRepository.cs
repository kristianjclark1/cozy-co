using CozyCo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CozyCo.Data.Interfaces
{
    public interface IPropertyTypeRepository
    {
        //Read
        PropertyType GetById(int id);
        ICollection<PropertyType> GetAll();
    }
}
