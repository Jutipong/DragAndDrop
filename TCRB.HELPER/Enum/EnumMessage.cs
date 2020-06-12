using System.ComponentModel;

namespace TCRB.HELPER
{
    public enum EnumMessage
    {
        [Description("Data not found.")]
        DATANOTFOUND,
        [Description("Data already Exists!.")]
        DUPLICATE,
        [Description("PayinFull")]
        PAYINFULL,
        [Description("Standard")]
        STANDARD,
        [Description("Deposit")]
        DEPOSIT,
        [Description("WithDraw")]
        WITHDRAW,
        [Description("Percent")]
        PERCENT,
        [Description("Amount")]
        AMOUNT,
        [Description("FixAmount")]
        FIXAMOUNT,
        [Description("Y")]
        YES,
        [Description("N")]
        NO,
        [Description("INVALID PARAMETER")]
        INVALIDPARAMETER,
        [Description("OTHER")]
        OTHER
    }

}
