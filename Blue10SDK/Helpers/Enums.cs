using System.Text.Json.Serialization;

namespace Blue10SDK
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EB10Endpoint
    {
        administrationactions,
        companies,
        costcenters,
        costunits,
        documentactions,
        documentoriginals,
        glaccounts,
        logisticsdocumentactions,
        me,
        paymentterms,
        projects,
        purchaseinvoices,
        purchaseorders,
        vatcodes,
        vatscenarios,
        vendors,
        warehouses
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EAdministrationAction
    {
        update_all_master_data,
        update_vendors
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EDocumentAction
    {
        create_purchase_invoice,
        get_payment_due_date,
        post_block_purchase_invoice,
        post_purchase_invoice,
        get_purchase_invoice_lines,
        block_purchase_invoice_for_payment,
        unblock_purchase_invoice_for_payment,
        match_purchase_order
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ELogisticsDocumentAction
    {
        create_logistics_purchase_invoice,
        get_match_result,
        export_logistics_purchase_invoice
    }
}
