using System.Collections.Generic;

namespace BikeDistributor
{
    public enum ReceiptTemplateType
    {
        Basic  = 1,
        Html = 2
    }

    public class ReceiptTemplate
    {
        public bool AppendsNewLine { get; set; }
        public string LineItemFormat { get; set; }
        public string Template { get; set; }
    }

    public class TemplateDictionary
    {
        public static Dictionary<ReceiptTemplateType, ReceiptTemplate> Templates = new Dictionary<ReceiptTemplateType, ReceiptTemplate>{
            { ReceiptTemplateType.Basic, new ReceiptTemplate()
                {
                    AppendsNewLine = true,
                    LineItemFormat = "{0} x {1} {2} = {3:C}",
                    Template = @"Order Receipt for {0}\n\t{1}\nSub-Total: {2:C}\nTax: {3:C}\nTotal: {4:C}"
                }
            },
            { ReceiptTemplateType.Html, new ReceiptTemplate {
                AppendsNewLine = false,
                LineItemFormat = "{0} x {1} {2} = {3:C}",
                Template = @"<html><body><h1>Order Receipt for {0}</h1><ul><li>{1}</li></ul><h3>Sub-Total: {2:C}</h3><h3>Tax: {3:C}</h3><h2>Total: {4:C}</h2></body></html>" }
            } };
    }
}
