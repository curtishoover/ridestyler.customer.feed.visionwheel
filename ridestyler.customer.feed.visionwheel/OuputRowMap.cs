using CsvHelper.Configuration;
using ridestyler.core.feed.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ridestyler.customer.feed.visionwheel
{
    class OuputRowMap : ClassMap<OutputRow>
    {
        public OuputRowMap()
        {
            Map(m => m.PartNumber);
            Map(m => m.SupplierNumber);
            Map(m => m.Model);
            Map(m => m.Brand);
            Map(m => m.Discontinued);
            Map(m => m.ShipHeight);
            Map(m => m.ShipWidth);
            Map(m => m.ShipDepth);
            Map(m => m.ShipWeight);
            Map(m => m.CostTotal);
            Map(m => m.MSRP);
            Map(m => m.StockAvailable);
            Map(m => m.SourceName);

        }
    }
}
