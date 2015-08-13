using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{

    public interface IReceiptBuilder
    {
        string BuildReceipt(IList<Line> lines, decimal taxRate, string company);
        string BuildHtmlReceipt(IList<Line> lines, decimal taxRate, string company);
    }

    public class ReceiptBuilder : IReceiptBuilder
    {
        private string OrderHeader = "Order Receipt for {0}";
        public string BuildReceipt(IList<Line> lines, decimal taxRate, string company)
        {
            var totalAmount = 0m;
            string header = string.Format(OrderHeader, company);
            var result = new StringBuilder(string.Format("{0}{1}", header, Environment.NewLine));
            foreach (var line in lines)
            {
                var thisAmount = line.Cost;
                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Brand, line.Model, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * taxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));

            return result.ToString();
        }

        public string BuildHtmlReceipt(IList<Line> lines, decimal taxRate, string company)
        {

            var totalAmount = 0m;
            string header = string.Format(OrderHeader, company);
            var result = new StringBuilder(string.Format("<html><body><h1>{0}</h1>", header));
            if (lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in lines)
                {
                    var thisAmount = line.Cost;

                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Brand, line.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }

            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * taxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            return result.ToString();
        }
    }
}
