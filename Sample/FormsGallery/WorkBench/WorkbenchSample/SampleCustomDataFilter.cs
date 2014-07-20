using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AggregationInterfaces.Schema;
using PreEmptive.Components.Messaging.Schema.Public.V2;
using PreEmptive.Workbench.Interfaces;
using PreEmptive.Workbench.Interfaces.CustomCounting;
using PreEmptive.Workbench.Interfaces.Indexing;


namespace WorkbenchSample
{
    public class SampleCustomDataFilter : CustomDataCounterBase
    {
        public const string Namespace = "PreEmptive.Sample";
        public SampleCustomDataFilter(FieldKeyFactory fieldKeyFactory)
            : base(Namespace, "VideoGame", new Type[] {typeof (SessionLifeCycle)}, new[]
            {
                new CustomDataCounterKey("Character", false),
                new CustomDataCounterKey("Level", false),
                new CustomDataCounterKey("EnemiesDefeated", false),
                new CustomDataCounterKey("Inventory", true)
            }, fieldKeyFactory,
            false) //Include as pivots
        {

        }
    }
}
