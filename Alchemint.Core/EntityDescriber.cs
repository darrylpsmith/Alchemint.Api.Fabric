using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Alchemint.Core
{
    public class EntityDescriber
    {

        private object _entity = null;

        public EntityDescriber (object Entity) {
            _entity = Entity;
        }

        public List<EntityProperty> PrimaryKeys ()
        {
            return GetPropertiesOfAttributeType(
                new Type[] {typeof(PrimaryKeyAttribute) }
                );
        }

        public bool PrimaryKeyProvidedOnEntity ()
        {
            bool primaryKeyIdFieldValueSupplied = false;
            if (IdField() != null)
            {
                if (IdField().Value != null)
                {
                    primaryKeyIdFieldValueSupplied = true;
                }
            }
            return primaryKeyIdFieldValueSupplied;
        }

        public bool UniqueKeyProvidedOnEntity()
        {
            bool uniqueKeyIdFieldValueSupplied = false;
            if (UniqueKeyFields() != null)
            {
                foreach(var key in UniqueKeyFields())
                {
                    if (key.Value == null)
                    {
                        uniqueKeyIdFieldValueSupplied = false;
                        break;
                    }
                }

            }
            return uniqueKeyIdFieldValueSupplied;
        }

        public EntityProperty IdField()
        {
            var props =  GetPropertiesOfAttributeType(
                new Type[] { typeof(PrimaryKeyAttribute) }
                );
            if (props.Count == 0)
            {
                return null;
            }
            else if (props.Count == 1)
            {
                return props.First();
            }
            else
            {
                throw new Exception("CODE LOGIC EXCEPTION: More than one Primary Key field specified");
            }
        }

        public List<EntityProperty> UniqueKeyFields()
        {
            var props = GetPropertiesOfAttributeType(
                new Type[] { typeof(UniqueKeyAttribute) }
                );
            if (props.Count == 0)
            {
                return null;
            }
            else if (props.Count == 1)
            {
                return props;
            }
            else
            {
                throw new Exception("CODE LOGIC EXCEPTION: More than one Primary Key field specified");
            }
        }
        public List<EntityProperty> UniqueKeys()
        {
            return GetPropertiesOfAttributeType(
                new Type[] {typeof(UniqueKeyAttribute) }
                );
        }

        public List<EntityProperty> PrimaryAndUniqueKeys()
        {
            return GetPropertiesOfAttributeType(
                new Type[] { typeof(PrimaryKeyAttribute), typeof(UniqueKeyAttribute) }
                );
        }

        public List<EntityProperty> AllPropertyValues()
        {
            return GetAllProperties();
        }
        private List<EntityProperty> GetPropertiesOfAttributeType (Type[] types = null)
        {

            List<EntityProperty> keys = new List<EntityProperty>();

            var type = _entity.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                foreach (var tp  in types)
                {
                    var attributes = property.GetCustomAttributes(false);

                    if (attributes.Where(a => a.GetType() == tp).Count() > 0)
                    {
                        var value = (object)type.GetProperty(property.Name).GetValue(_entity, null);
                        var propType = type.GetProperty(property.Name).PropertyType;
                        keys.Add(new EntityProperty { Name = property.Name, Value = value, Type = propType });
                    }
                }

            }

            return keys;
        }

        private List<EntityProperty> GetAllProperties()
        {

            List<EntityProperty> keys = new List<EntityProperty>();

            var type = _entity.GetType();
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                var value = (object)type.GetProperty(property.Name).GetValue(_entity, null);
                var propType = type.GetProperty(property.Name).PropertyType;
                bool isPartOfPrimaryKey = false;
                bool isPartOfUniqueKey = false;

                var attributes = property.GetCustomAttributes(false);

                if (attributes.Where(a => a.GetType() == typeof(PrimaryKeyAttribute)).Count() > 0)
                    isPartOfPrimaryKey = true;

                if (attributes.Where(a => a.GetType() == typeof(UniqueKeyAttribute)).Count() > 0)
                    isPartOfUniqueKey = true;

                keys.Add(new EntityProperty { Name = property.Name, Value = value, Type = propType, IsPartOfPrimaryKey = isPartOfPrimaryKey, IsPartOfUniqueKey = isPartOfUniqueKey });

            }

            return keys;

        }

    }
}
