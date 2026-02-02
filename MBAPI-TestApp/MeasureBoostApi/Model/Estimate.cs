namespace MeasureBoostApi.Model
{
    public class Estimate
    {
        public Project Project { get; set; } = new Project();
        public Customer Customer { get; set; } = new Customer();        
        public List<EstimateLine> Lines { get; set; } = [];
        public List<Attachment> Attachments { get; set; } = [];
    }

    public class EstimateLine {

        public EstimateLineProduct Product { get; set; } = new EstimateLineProduct();

        public double Quantity { get; set; }
        public string SaleUnits { get; set; } = "";
        public double UnitPrice { get; set; }
        public double UnitCost { get; set; }
        public double LineTotal { get; set; }
        public double TaxAmount { get; set; }
        public bool IsFixedTotal { get; set; } = false;

        public bool Hidden { get; set; }
        public string Notes { get; set; } = "";
        public string InternalNotes { get; set; } = "";

    }

    public class EstimateLineProduct {
        public string Id { get; set; } = "";
        public string ProductType { get; set; } = "";
        public string StyleName { get; set; } = "";
        public string StyleNumber { get; set; } = "";
        public string ColorName { get; set; } = "";
        public string ColorNumber { get; set; } = "";
        public string Supplier { get; set; } = "";
    }

    public class Project {
        public string Id { get; set; } = "";
        public string Title { get; set; } = "";
        public string Status { get; set; } = "";
        public string QuoteNumber { get; set; } = "";
    }

    public class Attachment {
        public string Title { get; set; } = "";
        public string DownloadUrl { get; set; } = "";
        public int LineNumber { get; set; } = 0;
        public string AttachmentType { get; set; } = "";
    }
}
