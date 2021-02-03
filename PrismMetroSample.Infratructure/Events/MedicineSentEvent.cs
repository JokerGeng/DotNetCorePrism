using Prism.Events;
using PrismMetroSample.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismMetroSample.Infratructure.Events
{
    public class MedicineSentEvent : PubSubEvent<Medicine>
    {

    }
}
