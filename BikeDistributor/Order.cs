using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BikeDistributor
{
    public class Order
    {
        private readonly IList<Line> _lines = new List<Line>();

        public string Company { get; private set; }
        public decimal TaxRate { get; private set; }

        public Order(string company, decimal taxRate)
        {
            Company = company;
            TaxRate = taxRate;
        }

        public void AddLine(Line line)
        {
            _lines.Add(line);
        }

        public string Receipt()
        {
            var totalAmount = 0m;
            var result = new StringBuilder(string.Format("Order Receipt for {0}{1}", Company, Environment.NewLine));
            foreach (var line in _lines)
            {
                var thisAmount = CalculateCost(line.Quantity, line.Price);
                result.AppendLine(string.Format("\t{0} x {1} {2} = {3}", line.Quantity, line.Brand, line.Model, thisAmount.ToString("C")));
                totalAmount += thisAmount;
            }
            result.AppendLine(string.Format("Sub-Total: {0}", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.AppendLine(string.Format("Tax: {0}", tax.ToString("C")));
            result.Append(string.Format("Total: {0}", (totalAmount + tax).ToString("C")));
            return result.ToString();
        }

        public string HtmlReceipt()
        {
            var totalAmount = 0m;
            var result = new StringBuilder(string.Format("<html><body><h1>Order Receipt for {0}</h1>", Company));
            if (_lines.Any())
            {
                result.Append("<ul>");
                foreach (var line in _lines)
                {
                    var thisAmount = CalculateCost(line.Quantity, line.Price);

                    result.Append(string.Format("<li>{0} x {1} {2} = {3}</li>", line.Quantity, line.Brand, line.Model, thisAmount.ToString("C")));
                    totalAmount += thisAmount;
                }
                result.Append("</ul>");
            }

            result.Append(string.Format("<h3>Sub-Total: {0}</h3>", totalAmount.ToString("C")));
            var tax = totalAmount * TaxRate;
            result.Append(string.Format("<h3>Tax: {0}</h3>", tax.ToString("C")));
            result.Append(string.Format("<h2>Total: {0}</h2>", (totalAmount + tax).ToString("C")));
            result.Append("</body></html>");
            return result.ToString();
        }

        public decimal CalculateCost(int quantity, decimal price)
        {
            var discount = 1m;

            if (price >= 5000)
            {
                if (quantity >= 5)
                {
                    discount = .8m;
                }
            }
            else if (price >= 2000)
            {
                if (quantity >= 10)
                {
                    discount = .8m;
                }
            }
            else if (price >= 1000)
            {
                if (quantity >= 20)
                {
                    discount = .9m;
                }
            }

            return quantity * price * discount;
        }

    }
}