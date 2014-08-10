using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AggregationInterfaces.Schema;
using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces.CustomCounting;

namespace WorkbenchSample
{
    public class CustomDataFilter : CustomDataCounterBase
    {

        public CustomDataFilter(FieldKeyFactory fieldKeyFactory)
            : base(InstanceIndexer.Namespace, "Feedback", new Type[] { typeof(FeatureMessage) }, new[]
        {
            new CustomDataCounterKey("Happiness", false),
            new CustomDataCounterKey("Color", false)
        }, fieldKeyFactory,true)
        {
        }
    }

    public class CustomUserDataFilter : CustomDataCounterBase
    {

        public CustomUserDataFilter(FieldKeyFactory fieldKeyFactory)
            : base(InstanceIndexer.Namespace, "User", new Type[] { typeof(ApplicationLifeCycle) }, new[]
        {
            new CustomDataCounterKey("Department", false),
            
        }, fieldKeyFactory, true)
        {
        }
    }

}
