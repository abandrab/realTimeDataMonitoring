using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Layer.Helpers
{
    public class PropertyCopier<T>
    {
        public static void Copy(T source, T destination)
        {
            PropertyDescriptorCollection sourceProps = TypeDescriptor.GetProperties(source);
            PropertyDescriptorCollection destProps = TypeDescriptor.GetProperties(destination);

            foreach (PropertyDescriptor prop in sourceProps)
            {
                PropertyDescriptor destProp = destProps[prop.Name];
                if (destProp != null)
                {
                    var type = destProp.GetType();

                        var destVal = destProp.GetValue(destination);
                        var sourceVal = prop.GetValue(source);
                    if (destVal != null && sourceVal != null)
                    {
                        if (!destVal.Equals(sourceVal))
                        {
                            destProp.SetValue(destination, prop.GetValue(source));
                        }
                    }
                    else
                    {
                        destProp.SetValue(destination, prop.GetValue(source));
                    }
                }
            }
        }
        public void CopyProps()
        {

        }
    }
}