namespace MeasureBoostApi.Model
{
    public class Product
    {
        public string Id { get; set; } = "";
         
        public string ProductType { get; set; } = "";
            // RollCarpet, RollVinyl, RollWallpaper
            // TileCeramic, TileLaminate, TileWood, TileOther
            // PlankLaminate, PlankWood
            // ItemService, ItemMisc
        
        // If false, Measurement fields are understood to be metres
        public bool MeasurementsAreInches { get; set; } = false;

        public string StyleName { get; set; } = "";
        public string StyleNumber { get; set; } = "";
        public string Supplier { get; set; } = "";

        public double LabelWidthMeasurement { get; set; } = 0;
        public double UsableWidthMeasurement { get; set; } = 0;
        public double PatternWidthMeasurement { get; set; } = 0;
        public double PatternLengthMeasurement { get; set; } = 0;
        public double PatternDropPercent { get; set; } = 0;

        public int MaximumTSeams { get; set; } = 0;
        public bool CutSquare { get; set; } = false;
        public double CutGapMeasurement { get; set; } = 0;
        public int CutGapMethod { get; set; } = 0;                       //  0=Guillotine, 1=Length, 2=All sides
        public bool AllowTileRotation { get; set; } = false;

        public decimal ExtraWasteAmount { get; set; }                    // Percentage 0.1 = 10%
        public int ExtraWasteType { get; set; }                          //  0=Percentage, 1=Manual, 2=Length
        public bool UseAsAddonOnly { get; set; }

        public decimal UnitPrice { get; set; }
        public decimal UnitCost { get; set; }

        public string SaleUnits { get; set; } = "EA";
        public bool IsTaxable { get; set; }
        public double QuantityPerBox { get; set; }

        public List<ProductColorOption> ProductColorOptions { get; set; } = new List<ProductColorOption>();
    }

    public class ProductColorOption {
        public int Id { get; set; }
        public string ColorName { get; set; } = "";
        public string ColorNumber { get; set; } = "";
        public string SKU { get; set; } = "";
    }

}
