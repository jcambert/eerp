using ePing.Api.models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ePing.Api.services
{
    public class EfService
    {
        public EfService()
        {

        }
        static Dictionary<Type,IEnumerable< PropertyInfo>> _cache = new Dictionary<Type, IEnumerable< PropertyInfo>>();
        public IEnumerable< PropertyInfo> GetKey<TModel>() where TModel : Trackable
        {
            var @type = typeof(TModel);
            if (_cache.ContainsKey(@type))
                return _cache[@type];
            var props=typeof(TModel).GetProperties().Where(p=>p.GetCustomAttribute<KeyAttribute>()!=null);

            if (props == null) throw new NullReferenceException("A Trackable object must have a KeyAttribute at least");
            _cache[@type] = props;
            return props;
        }

        public object[] GetKeyValues<TModel>(TModel entity)where TModel : Trackable
        {
            List<object> result = new List<object>();
            foreach (var prop in GetKey<TModel>())
            {
                result.Add(prop.GetGetMethod().Invoke(entity,null));
            }
            return result.ToArray();
        }
    }
}
