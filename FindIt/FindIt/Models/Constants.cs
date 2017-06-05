using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FindIt.Models
{
    public class Constants
    {
        public const string AdministratorRole = "Administrator";

        public static Guid LessThan50Id = Guid.Parse("33491c0f-57ba-431b-a7e1-f0c1dcecd1e8");
        public static Guid LessThan60Id = Guid.Parse("fbb1b609-39f8-476e-a9f5-df3658c50878");
        public static Guid LessThan80Id = Guid.Parse("0927e465-cd54-45dc-8a41-e2a14f2aa5db");
        public static Guid LessThan90Id = Guid.Parse("e6d063ac-b16c-4e69-8901-adc549e09b0f");
        public static Guid MoreThan90Id = Guid.Parse("92935a98-ee71-4274-bd9f-aa4dd6039f02");
    }
}