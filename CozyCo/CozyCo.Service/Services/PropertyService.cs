using CozyCo.Data.Interfaces;
using CozyCo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CozyCo.Service.Services
{
    public interface IPropertyService
    {
        
        //Read
        Property GetById(int id);
        ICollection<Property> GetAllProperties();

        //Create
        Property Create(Property newProperty);

        //Update
        Property Update(Property updatedProperty);

        //Delete
        bool Delete(int id);

    }

    public class PropertyService : IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;//->null

        //Added a dependency to the contructor
        public PropertyService(IPropertyRepository propertyRepository)
        {
            _propertyRepository = propertyRepository;//not null
        }

        public ICollection<Property>GetAllProperties()
        {
            return _propertyRepository.GetAllProperties();
        }

        public Property Create(Property newProperty)
        {
            //Add more logic to verify a new property
            //before creating
           return _propertyRepository.Create(newProperty); //Create() is from Repository
        }

        public bool Delete(int id)
        {
            return _propertyRepository.Delete(id);
        }

        public Property GetById(int id)
        {
            return _propertyRepository.GetById(id);
        }

        public Property Update(Property updatedProperty)
        {
            return _propertyRepository.Update(updatedProperty);
        }
    }
}
