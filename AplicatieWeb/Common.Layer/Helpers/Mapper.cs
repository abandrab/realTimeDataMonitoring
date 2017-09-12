using Domain.Model.Models.MongoDB;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Common.Layer.Helpers
{
    public static class Mapper
    {
            public static List<Data> Map (List<Data> datas)
            {
                List<Data> entities = new List<Data>();
                foreach(var entry in datas)
                {
                    //entry.ConvertTimezone();
                    if (entry.NodeType.Equals("ECG"))
                    {
                        entry.Package = ToStatic<ECG>((JObject)entry.Package);
                        entities.Add(entry);
                    }
                    else if (entry.NodeType.Equals("SpO2"))
                    {
                        entry.Package = ToStatic<SpO2>((JObject)entry.Package);
                        entities.Add(entry);
                    }
                    else if (entry.NodeType.Equals("TAC"))
                    {
                        entry.Package = ToStatic<TAcc>((JObject)entry.Package);
                        entities.Add(entry);
                    }
                }
                return entities;
            }
            public static T ToStatic<T>(JObject expando)
            {
                var entity = Activator.CreateInstance<T>();
                var properties = expando.ToObject<Dictionary<string, object>>();
                if (properties == null)
                    return entity;
                foreach (var entry in properties)
                {
                    var propertyInfo = entity.GetType().GetProperty(entry.Key);
                    if (propertyInfo != null && entry.Value != null)
                    {
                        if (entry.Value.GetType() == typeof(JArray))
                        {
                            propertyInfo.SetValue(entity, ((JArray)entry.Value).ToObject<List<int>>(), null);
                        }
                        else if(entry.Value.GetType() == typeof(Int64))
                        {
                            propertyInfo.SetValue(entity, Convert.ToInt32(entry.Value), null);
                        }
                        else
                        {
                            propertyInfo.SetValue(entity, entry.Value, null);
                        }
                    }
                }
                return entity;
            }
    }
}
