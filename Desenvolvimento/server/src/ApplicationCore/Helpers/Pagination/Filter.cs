using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApplicationCore.Helpers.Pagination
{
    public class Filter
    {
        [DataMember]
        public GroupOperation groupOp { get; set; }
        [DataMember]
        public List<Rule> rules { get; set; }
        [DataMember]
        public List<Filter> groups { get; set; }
    }
}
