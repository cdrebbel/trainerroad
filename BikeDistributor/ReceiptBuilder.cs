using System;
using System.Collections.Generic;
using System.Text;

namespace BikeDistributor
{ 
    public interface IReceiptBuilder
    {
        string BuildReceipt(IList<Line> lines, decimal taxRate, string company, ReceiptTemplateType template);
    }

    public class ReceiptBuilder : IReceiptBuilder
    {   
        public string BuildReceipt(IList<Line> lines, decimal taxRate, string company, ReceiptTemplateType type)
        {
            var template = TemplateDictionary.Templates[type];
            var subTotal = 0m;
            var lineItems = new StringBuilder();
            foreach (var line in lines)
            {
                var lineItem = string.Format(template.LineItemFormat, line.Quantity, line.Brand, line.Model, line.Cost);
                if (template.AppendsNewLine)
                    lineItems.AppendLine(lineItem);
                else
                    lineItems.Append(lineItem);

                subTotal += line.Cost;
            }
            var tax = subTotal * taxRate;
            var totalAmount = subTotal + tax;

            var result = new StringBuilder(string.Format(template.Template, company, lineItems, totalAmount, tax, totalAmount));

            return result.ToString();
        }
    }
}