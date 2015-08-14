using System.Collections.Generic;

namespace BikeDistributor
{
    public class Order
    {
        private readonly IList<Line> _lines = new List<Line>();

        public string Company { get; private set; }
        public decimal TaxRate { get; private set; }
        private IReceiptBuilder _receiptBuilder;

        public Order(string company, decimal taxRate) : this(company, taxRate, new ReceiptBuilder())
        {

        }

        public Order(string company, decimal taxRate, IReceiptBuilder receiptBuilder)
        {
            Company = company;
            TaxRate = taxRate;
            _receiptBuilder = receiptBuilder;
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            return _receiptBuilder.BuildReceipt(_lines, TaxRate, Company, ReceiptTemplateType.Basic);
        }

        public string HtmlReceipt()
        {
            return _receiptBuilder.BuildReceipt(_lines, TaxRate, Company, ReceiptTemplateType.Html);
        }
    }
}