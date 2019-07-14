using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting.Models
{
    class DocumnetTemplateModelBinding
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DescriptionHeader { get; set; }
        public string DescriptionRow { get; set; }
        public string NatureTitle { get; set; }
        public string NatureId { get; set; }
        public int DocumentTypeId { get; set; }
        public string DocumentTypeTitle { get; set; }
        public string TafsilCode { get; set; }
    }
}
