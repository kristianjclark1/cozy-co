using CozyCo.Data.Interfaces;
using CozyCo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CozyCo.Service.Services
{
    public interface IPropertyTypeService
    {
        //Read
        PropertyType GetById(int id);
        ICollection<PropertyType> GetAll();
    }

    public class PropertyTypeService : IPropertyTypeService
    {
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public PropertyTypeService(IPropertyTypeRepository propertyTypeRepository) 
        {
            _propertyTypeRepository = propertyTypeRepository;
        }

        public ICollection<PropertyType> GetAll()
        {
            return _propertyTypeRepository.GetAll();
        }

        public PropertyType GetById(int id)
        {
            return _propertyTypeRepository.GetById(id);
        }
    }
}
